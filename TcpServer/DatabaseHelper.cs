using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace TcpServer
{
    public class DatabaseHelper
    {
        private readonly string connectionString;

        public DatabaseHelper(string connStr)
        {
            connectionString = connStr;
            EnsureDatabaseExists();
        }
        public string ConnectionString => connectionString;

        private void EnsureDatabaseExists()
        {
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    Console.WriteLine("Database connected successfully!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database not found. Creating database... Error: {ex.Message}");

                string scriptPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Database\QuanLyQuanNet.sql");
                if (!File.Exists(scriptPath))
                {
                    throw new FileNotFoundException("SQL script file not found!", scriptPath);
                }

                string sql = File.ReadAllText(scriptPath);

                using (var conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Integrated Security=True"))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }

                Console.WriteLine("Database created successfully!");
            }
        }

        public DataTable ExecuteQuery(string query, params SqlParameter[] prms)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                if (prms != null) cmd.Parameters.AddRange(prms);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public int ExecuteNonQuery(string query, params SqlParameter[] prms)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                if (prms != null) cmd.Parameters.AddRange(prms);
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }
        public object ExecuteScalar(string query, params SqlParameter[] prms)
        {
            object rs = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                if (prms != null)
                {
                    cmd.Parameters.AddRange(prms);
                }
                conn.Open();
                rs = cmd.ExecuteScalar();
                conn.Close();
            }
            return rs;
        }
    }
}
