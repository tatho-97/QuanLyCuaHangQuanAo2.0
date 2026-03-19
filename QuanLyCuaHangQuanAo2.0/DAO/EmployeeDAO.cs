using BTL_QuanLyKhoHang_Nhom20;
using BTL_QuanLyKhoHang_Nhom20.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiTapLon.DAO
{
    internal class EmployeeDAO
    {
        private static EmployeeDAO instance = new EmployeeDAO();
        public static EmployeeDAO Instance { get { return instance; } }
        public List<Employee> GetAllEmployee()
        {
            List<Employee> list = new List<Employee>();
            string query = @"SELECT * FROM employees ";

            using (SQLiteConnection conn = DataProvider.GetConnection())
            {
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    conn.Open();
                    
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read()) // Đọc từng dòng trong database
                        {
                            Employee e = new Employee();
                            e.Employee_id = Convert.ToInt32(reader["Employee_id"]);
                            e.Full_name = reader["Full_name"].ToString();
                            e.Gender = reader["Gender"].ToString();
                            e.Is_deleted = Convert.ToBoolean( reader["Is_deleted"]);
                            e.Password_hash = reader["Password_hash"].ToString();
                            e.Phone_number = reader["Phone_number"].ToString();
                            e.Role_id = Convert.ToInt32(reader["Role_id"]);
                            e.Username = reader["Username"].ToString();
                            list.Add(e);
                        }
                    }
                }
                return list;
            }
        }
    }
}
