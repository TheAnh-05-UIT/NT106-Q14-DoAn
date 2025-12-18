using System;
using System.Data.SqlClient;

namespace TcpServer.Handlers
{
    public class HandlerCustomerBalance
    {
        private readonly DatabaseHelper db;

        public HandlerCustomerBalance(DatabaseHelper database)
        {
            db = database;
        }

        public string AddBalance(decimal amount, string customerId)
        {
            try
            {
                string updateBalance = "UPDATE Customers SET Balance = ISNULL(Balance,0) + @amount WHERE CustomerId = @customerId";
                db.ExecuteNonQuery(updateBalance,
                    new SqlParameter("@amount", amount),
                    new SqlParameter("@customerId", customerId));

                return "Số dư khách hàng đã được cập nhật thành công.";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while updating customer balance: {ex.Message}");
                return "Không thể cập nhật số dư khách hàng.";
            }
        }
    }
}