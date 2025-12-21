using System;
using System.Data;
using System.Data.SqlClient;

namespace TcpServer.Handlers
{
    public class HandlerAdminCustomerAcc
    {
        private readonly DatabaseHelper db;

        public HandlerAdminCustomerAcc(DatabaseHelper databaseHelper)
        {
            db = databaseHelper;
        }

        // GET ALL EMPLOYEES
        public object HandleGetAllEmployees()
        {
            try
            {
                string query = @"SELECT 
                                    Employees.EmployeeCode AS [Username],
                                    Users.FullName AS [Họ tên],
                                    Employees.Gender AS [Giới tính],
                                    Employees.BirthDate AS [Ngày sinh],
                                    Users.Phone AS [Số điện thoại],
                                    Employees.HiredDate AS [Ngày vào làm],
                                    Employees.WorkDays AS [Số ngày làm],
                                    Employees.SalaryBase AS [Lương cơ bản],
                                    Employees.SalaryMonth AS [Lương tháng]
                                 FROM Employees
                                 JOIN Users ON Employees.EmployeeId = Users.UserId";
                DataTable dt = db.ExecuteQuery(query);
                return new { status = "success", data = dt };
            }
            catch (Exception ex)
            {
                return new { status = "error", message = ex.Message };
            }
        }
        // ADD EMPLOYEE
        public object HandleAddEmployee(dynamic data)
        {
            using (SqlConnection conn = new SqlConnection(db.ConnectionString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();

                try
                {
                    string userId = Guid.NewGuid().ToString("N").Substring(0, 8);

                    // Lấy chuỗi
                    string empCode = (string)data.maNV;
                    string fullName = (string)data.hoTen;
                    string gender = (string)data.gioiTinh;
                    string phone = (string)data.soDienThoai;
                    string email = (string)data.email;

                    string rawPassword = (data.matKhau != null) ? (string)data.matKhau : "123";
                    // Mã hóa mật khẩu trước khi lưu
                    string hashedPassword = PasswordHelper.HashPassword(rawPassword);
                    DateTime birthDate;
                    try
                    {
                        birthDate = (data.ngaySinh == null) ? DateTime.Now : Convert.ToDateTime(data.ngaySinh);
                    }
                    catch { birthDate = DateTime.Now; }

                    DateTime hiredDate;
                    try
                    {
                        hiredDate = (data.ngayVaoLam == null) ? DateTime.Now : Convert.ToDateTime(data.ngayVaoLam);
                    }
                    catch { hiredDate = DateTime.Now; }
                    int workDays = (data.soNgayLam != null) ? Convert.ToInt32(data.soNgayLam) : 0;
                    decimal salaryBase = (data.luongCoBan != null) ? Convert.ToDecimal(data.luongCoBan) : 0;
                    decimal salaryMonth = (data.luongThang != null) ? Convert.ToDecimal(data.luongThang) : 0;


                    // Thêm vào bảng Users
                    string insertUser = @"INSERT INTO Users 
                (UserId, Username, [Password], FullName, Phone, Email, [Role], Active)
                VALUES (@id, @username, @pwd, @fullname, @phone, @mail, 'EMPLOYEE', 1)";

                    using (SqlCommand cmd = new SqlCommand(insertUser, conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@id", userId);
                        cmd.Parameters.AddWithValue("@username", empCode); // Tên đăng nhập = Mã NV

                        // LƯU MẬT KHẨU ĐÃ MÃ HÓA
                        cmd.Parameters.AddWithValue("@pwd", hashedPassword);

                        cmd.Parameters.AddWithValue("@fullname", fullName ?? DBNull.Value.ToString());
                        cmd.Parameters.AddWithValue("@phone", phone ?? DBNull.Value.ToString());
                        cmd.Parameters.AddWithValue("@mail", email ?? DBNull.Value.ToString());
                        cmd.ExecuteNonQuery();
                    }

                    // Thêm vào bảng Employees
                    string insertEmp = @"INSERT INTO Employees 
                (EmployeeId, EmployeeCode, Gender, BirthDate, HiredDate, WorkDays, SalaryBase, SalaryMonth)
                VALUES (@id, @code, @gender, @birth, @hired, @days, @base, @month)";

                    using (SqlCommand cmd = new SqlCommand(insertEmp, conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@id", userId);
                        cmd.Parameters.AddWithValue("@code", empCode);
                        cmd.Parameters.AddWithValue("@gender", gender ?? "Khác");
                        cmd.Parameters.AddWithValue("@birth", birthDate);
                        cmd.Parameters.AddWithValue("@hired", hiredDate);
                        cmd.Parameters.AddWithValue("@days", workDays);
                        cmd.Parameters.AddWithValue("@base", salaryBase);
                        cmd.Parameters.AddWithValue("@month", salaryMonth);
                        cmd.ExecuteNonQuery();
                    }

                    tran.Commit();
                    return new { status = "success", message = "Thêm nhân viên thành công!" };
                }
                catch (SqlException ex)
                {
                    tran.Rollback();
                    if (ex.Number == 2627 || ex.Number == 2601)
                    {
                        return new { status = "error", message = $"Mã nhân viên '{data.maNV}' đã tồn tại!" };
                    }
                    return new { status = "error", message = "Lỗi SQL: " + ex.Message };
                }
                catch (Exception ex)
                {
                    try { tran.Rollback(); } catch { }
                    return new { status = "error", message = "Lỗi hệ thống: " + ex.Message };
                }
            }
        }

        // UPDATE EMPLOYEE
        public object HandleUpdateEmployee(dynamic data)
        {
            try
            {
                string maNV = (string)data.maNV;
                decimal luongCB = (decimal)data.luongCoBan;
                int soNgayLam = (int)data.soNgayLam;

                string query = @"UPDATE Employees 
                                         SET SalaryBase=@LuongCB, WorkDays=@SoNgay 
                                         WHERE EmployeeCode=@MaNV";

                int rows = db.ExecuteNonQuery(query,
                    new SqlParameter("@LuongCB", luongCB),
                    new SqlParameter("@SoNgay", soNgayLam),
                    new SqlParameter("@MaNV", maNV)
                );

                if (rows > 0)
                    return new { status = "success", message = "Cập nhật thành công!" };
                else
                    return new { status = "fail", message = "Không tìm thấy nhân viên." };
            }
            catch (Exception ex)
            {
                return new { status = "error", message = ex.Message };
            }
        }

        // DELETE EMPLOYEE
        public object HandleDeleteEmployee(dynamic data)
        {
            try
            {
                string maNV = (string)data.maNV;

                string query = "DELETE FROM Employees WHERE EmployeeCode=@MaNV";
                int rows = db.ExecuteNonQuery(query, new SqlParameter("@MaNV", maNV));

                if (rows > 0)
                    return new { status = "success", message = "Xóa nhân viên thành công!" };
                else
                    return new { status = "fail", message = "Không tìm thấy nhân viên." };
            }
            catch (Exception ex)
            {
                return new { status = "error", message = ex.Message };
            }
        }
    }
}