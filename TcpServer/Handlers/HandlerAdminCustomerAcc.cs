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
                                    Employees.EmployeeCode AS [Mã nhân viên],
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
            try
            {
                string userId = Guid.NewGuid().ToString("N").Substring(0, 8);
                string empCode = (string)data.maNV;
                string fullName = (string)data.hoTen;
                string gender = (string)data.gioiTinh;
                DateTime birthDate = DateTime.Parse((string)data.ngaySinh);
                string phone = (string)data.soDienThoai;
                DateTime hiredDate = DateTime.Parse((string)data.ngayVaoLam);
                int workDays = (int)data.soNgayLam;
                decimal salaryBase = (decimal)data.luongCoBan;
                decimal salaryMonth = (decimal)data.luongThang;

                using (SqlConnection conn = new SqlConnection(db.ConnectionString))
                {
                    conn.Open();
                    SqlTransaction tran = conn.BeginTransaction();

                    try
                    {
                        // Thêm vào Users
                        string insertUser = @"INSERT INTO Users 
                            (UserId, Username, [Password], FullName, Phone, Email, [Role], Active)
                            VALUES (@id, @username, @pwd, @fullname, @phone, @mail, 'EMPLOYEE', 1)";
                        new SqlCommand(insertUser, conn, tran)
                        {
                            Parameters =
                            {
                                new SqlParameter("@id", userId),
                                new SqlParameter("@username", empCode),
                                new SqlParameter("@pwd", "123"), // mật khẩu mặc định
                                new SqlParameter("@fullname", fullName),
                                new SqlParameter("@phone", phone),
                                new SqlParameter("@mail", DBNull.Value)
                            }
                        }.ExecuteNonQuery();

                        // Thêm vào Employees
                        string insertEmp = @"INSERT INTO Employees 
                            (EmployeeId, EmployeeCode, Gender, BirthDate, HiredDate, WorkDays, SalaryBase, SalaryMonth)
                            VALUES (@id, @code, @gender, @birth, @hired, @days, @base, @month)";
                        new SqlCommand(insertEmp, conn, tran)
                        {
                            Parameters =
                            {
                                new SqlParameter("@id", userId),
                                new SqlParameter("@code", empCode),
                                new SqlParameter("@gender", gender),
                                new SqlParameter("@birth", birthDate),
                                new SqlParameter("@hired", hiredDate),
                                new SqlParameter("@days", workDays),
                                new SqlParameter("@base", salaryBase),
                                new SqlParameter("@month", salaryMonth)
                            }
                        }.ExecuteNonQuery();

                        tran.Commit();
                        return new { status = "success", message = "Thêm nhân viên thành công!" };
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        return new { status = "error", message = "Lỗi thêm nhân viên: " + ex.Message };
                    }
                }
            }
            catch (Exception ex)
            {
                return new { status = "error", message = ex.Message };
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