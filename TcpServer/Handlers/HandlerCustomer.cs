using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcpServer.ServerHandler;

namespace TcpServer.Handlers
{
    public class HandlerCustomer
    {
        private readonly DatabaseHelper _db;

        public HandlerCustomer(DatabaseHelper db)
        {
            _db = db;
        }
        private object CreateNewSession(string customerId, string computerId, decimal balance, decimal pricePerSecond, string username)
        {
            string sqlMax = "SELECT TOP 1 SessionId FROM Sessions ORDER BY CAST(SUBSTRING(SessionId, 2, 10) AS INT) DESC";
            var dtMax = _db.ExecuteQuery(sqlMax);

            string newSessionId;
            if (dtMax.Rows.Count == 0 || dtMax.Rows[0]["SessionId"].ToString() == "S")
            {
                newSessionId = "S001";
            }
            else
            {
                string lastId = dtMax.Rows[0]["SessionId"].ToString();
                int number;
                if (int.TryParse(lastId.Substring(1), out number))
                {
                    newSessionId = "S" + (number + 1).ToString("D3");
                }
                else
                {
                    newSessionId = "S001";
                }
            }

            int initialTimeLeft = (int)Math.Floor(balance / pricePerSecond);

            string sqlInsert = @"INSERT INTO Sessions(SessionId, CustomerId, ComputerId, StartTime, TotalCost)
                                 VALUES(@sessionId, @customerId, @computerId, GETDATE(), 0)";
            _db.ExecuteNonQuery(sqlInsert,
                new SqlParameter("@sessionId", newSessionId),
                new SqlParameter("@customerId", customerId),
                new SqlParameter("@computerId", computerId));

            string sql = @"UPDATE Computers SET Status = 'IN_USE' WHERE ComputerId = @cid";
            _db.ExecuteNonQuery(sql, new SqlParameter("@cid", computerId));

            return new
            {
                status = "success",
                sessionId = newSessionId,
                computerName = computerId,
                timeUsed = 0,
                moneyUsed = 0,
                moneyLeft = balance,
                timeLeft = initialTimeLeft,
                customerId = customerId,
                userName = username
            };
        }

        public object HandleStartSession(dynamic data, ServerHandler.ServerHandler server)
        {
            try
            {
                string customerId = data.customerId;
                string computerIdNew = data.computerId;

                // Lấy thông tin khách hàng và giá máy tính
                string sqlCustomer = @"SELECT Balance
                                       FROM Customers
                                       WHERE CustomerId=@customerId";
                var dtCustomer = _db.ExecuteQuery(sqlCustomer,
                    new SqlParameter("@customerId", customerId));

                string sqlComputer = @"SELECT PricePerHour 
                                       FROM Computers
                                       WHERE ComputerId = @computerIdNew";
                var dtComputer = _db.ExecuteQuery(sqlComputer,
                    new SqlParameter("@computerIdNew", computerIdNew));

                string sqlUsername = @"SELECT Username
                                       FROM Users
                                       WHERE UserId=@customerId";
                var dtUsername = _db.ExecuteQuery(sqlUsername,
                    new SqlParameter("@customerId", customerId));

                if (dtCustomer.Rows.Count == 0 || dtComputer.Rows.Count == 0 || dtUsername.Rows.Count == 0)
                    return new { status = "error", message = $"Customer or computer not found. Insert {computerIdNew} (Tên máy) vào database để tránh lỗi này. Thêm chức năng cài app vào máy sau." };

                string username = dtUsername.Rows[0]["Username"].ToString();
                decimal balance = Convert.ToDecimal(dtCustomer.Rows[0]["Balance"]);
                decimal pricePerHour = Convert.ToDecimal(dtComputer.Rows[0]["PricePerHour"]);
                decimal pricePerSecond = pricePerHour / 3600m;

                //  Kiểm tra session đang mở 
                string sqlSession = @"SELECT s.SessionId, s.StartTime, s.EndTime, s.ComputerId, s.TotalCost
                                     FROM Sessions s
                                     WHERE s.CustomerId=@customerId AND s.EndTime IS NULL";

                var dtSession = _db.ExecuteQuery(sqlSession, new SqlParameter("@customerId", customerId));

                if (dtSession.Rows.Count > 0)
                {
                    // Nếu đã có session 
                    string sessionId = dtSession.Rows[0]["SessionId"].ToString();
                    DateTime startTime = Convert.ToDateTime(dtSession.Rows[0]["StartTime"]);
                    string computerId = dtSession.Rows[0]["ComputerId"].ToString();

                    int timeUsed = (int)(DateTime.Now - startTime).TotalSeconds;
                    decimal moneyUsed = timeUsed * pricePerSecond;
                    decimal moneyLeft = balance - moneyUsed;

                    int timeLeft = (int)Math.Floor(moneyLeft / pricePerSecond);
                    if (timeLeft < 0) timeLeft = 0;

                    if (moneyLeft <= 0)
                    {
                        HandleEndSession(new { sessionId = sessionId }, server);

                        return new
                        {
                            status = "error",
                            message = "Phiên cũ đã hết tiền và đã kết thúc! Vui lòng nạp thêm tiền."
                        };
                    }
                    else
                    {
                        return new
                        {
                            status = "success",
                            sessionId = sessionId,
                            computerName = computerId,
                            timeUsed = timeUsed,
                            moneyUsed = moneyUsed,
                            moneyLeft = moneyLeft,
                            timeLeft = timeLeft,
                            customerId = customerId,
                            userName = username
                        };
                    }
                }

                return CreateNewSession(customerId, computerIdNew, balance, pricePerSecond, username);
            }
            catch (Exception ex)
            {
                return new { status = "error", message = ex.Message };
            }
        }
        public object HandleUpdateSession(dynamic data)
        {
            try
            {
                string sessionId = data.sessionId;

                string sql = @"
             SELECT s.StartTime, s.TotalCost, s.EndTime, s.ComputerId, c.PricePerHour, cu.Balance, cu.CustomerId
             FROM Sessions s
             JOIN Computers c ON s.ComputerId = c.ComputerId
             JOIN Customers cu ON s.CustomerId = cu.CustomerId
             WHERE s.SessionId=@sessionId";

                var dt = _db.ExecuteQuery(sql, new SqlParameter("@sessionId", sessionId));

                if (dt.Rows.Count == 0)
                    return new { status = "error", message = "Session not found" };

                DateTime startTime = Convert.ToDateTime(dt.Rows[0]["StartTime"]);
                decimal totalCost = Convert.ToDecimal(dt.Rows[0]["TotalCost"]);
                decimal pricePerHour = Convert.ToDecimal(dt.Rows[0]["PricePerHour"]);
                decimal balance = Convert.ToDecimal(dt.Rows[0]["Balance"]);
                string computerId = dt.Rows[0]["ComputerId"].ToString();
                DateTime? endTime = dt.Rows[0]["EndTime"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dt.Rows[0]["EndTime"]);
                string customerId = dt.Rows[0]["CustomerId"].ToString(); // Lấy CustomerId

                decimal pricePerSecond = pricePerHour / 3600m;

                if (endTime != null)
                {
                    return new
                    {
                        status = "ended",
                        timeUsed = (int)(endTime.Value - startTime).TotalSeconds,
                        moneyUsed = totalCost,
                        moneyLeft = balance - totalCost,
                        timeLeft = 0,
                        computerName = computerId
                    };
                }

                // Tính thời gian đã dùng
                int timeUsed = (int)(DateTime.Now - startTime).TotalSeconds;
                decimal moneyUsed = timeUsed * pricePerSecond;

                bool autoEnd = false;
                if (moneyUsed >= balance)
                {
                    moneyUsed = balance;
                    endTime = DateTime.Now;
                    autoEnd = true;

                    string sqlUpdateBalance = @"UPDATE Customers SET Balance=0 WHERE CustomerId=@customerId";
                    _db.ExecuteNonQuery(sqlUpdateBalance, new SqlParameter("@customerId", customerId));
                }

                decimal moneyLeft = balance - moneyUsed;
                int timeLeft = (int)Math.Floor(moneyLeft / pricePerSecond);
                if (timeLeft < 0) timeLeft = 0;

                string sqlUpdate = @"UPDATE Sessions SET TotalCost=@moneyUsed, EndTime=@endTime WHERE SessionId=@sessionId";
                _db.ExecuteNonQuery(sqlUpdate,
                    new SqlParameter("@moneyUsed", moneyUsed),
                    new SqlParameter("@endTime", (object)endTime ?? DBNull.Value),
                    new SqlParameter("@sessionId", sessionId));

                return new
                {
                    status = autoEnd ? "ended" : "success",
                    timeUsed = timeUsed,
                    moneyUsed = moneyUsed,
                    moneyLeft = moneyLeft,
                    timeLeft = timeLeft,
                    computerName = computerId
                };
            }
            catch (Exception ex)
            {
                return new { status = "error", message = ex.Message };
            }
        }

        public object HandleEndSession(dynamic data, ServerHandler.ServerHandler server)
        {
            try
            {
                string sessionId = data.sessionId;

                string sqlGet = @"SELECT s.StartTime, s.ComputerId, s.TotalCost, c.PricePerHour, cu.CustomerId, cu.Balance
                                   FROM Sessions s
                                   JOIN Computers c ON s.ComputerId = c.ComputerId
                                   JOIN Customers cu ON s.CustomerId = cu.CustomerId
                                   WHERE s.SessionId=@sessionId AND s.EndTime IS NULL";

                var dt = _db.ExecuteQuery(sqlGet, new SqlParameter("@sessionId", sessionId));

                if (dt.Rows.Count == 0)
                    return new { status = "error", message = "Session not found" };

                DateTime startTime = Convert.ToDateTime(dt.Rows[0]["StartTime"]);
                decimal balance = Convert.ToDecimal(dt.Rows[0]["Balance"]);
                string customerId = dt.Rows[0]["CustomerId"].ToString();
                decimal pricePerHour = Convert.ToDecimal(dt.Rows[0]["PricePerHour"]);
                string computerId = dt.Rows[0]["ComputerId"].ToString();


                int timeUsed = (int)(DateTime.Now - startTime).TotalSeconds;
                decimal pricePerSecond = pricePerHour / 3600m;
                decimal finalCost = timeUsed * pricePerSecond;

                if (finalCost > balance)
                {
                    finalCost = balance;
                }

                string sqlUpdate = @"UPDATE Sessions
                                     SET EndTime=GETDATE(),
                                         TotalCost=@finalCost
                                     WHERE SessionId=@sessionId";

                _db.ExecuteNonQuery(sqlUpdate,
                    new SqlParameter("@finalCost", finalCost),
                    new SqlParameter("@sessionId", sessionId));

                decimal newBalance = balance - finalCost;
                string sqlUpdateBalance = @"UPDATE Customers SET Balance=@newBalance WHERE CustomerId=@customerId";
                _db.ExecuteNonQuery(sqlUpdateBalance,
                    new SqlParameter("@newBalance", newBalance),
                    new SqlParameter("@customerId", customerId));

                string sql = @"UPDATE Computers SET Status = 'AVAILABLE' WHERE ComputerId = @cid";
                _db.ExecuteNonQuery(sql, new SqlParameter("@cid", computerId));
                server.notifyToStaff(new { type = "generic", data = new { title = "Máy đóng", content = $"Máy {computerId} đã đóng", addInfo = "" } });
                return new
                {
                    status = "success",
                    message = "Session ended",
                    timeUsed = timeUsed,
                    totalCost = finalCost,
                    newBalance = newBalance
                };
            }
            catch (Exception ex)
            {
                return new { status = "error", message = ex.Message };
            }
        }
    }
}
