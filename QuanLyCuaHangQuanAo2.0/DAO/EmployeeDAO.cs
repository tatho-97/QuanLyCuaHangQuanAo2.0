using System;
using System.Collections.Generic;
using System.Data.SQLite;
using QuanLyCuaHangQuanAo2._0.DTO; 

namespace QuanLyCuaHangQuanAo2._0.DAO 
{
    internal class EmployeeDAO
    {
        private static EmployeeDAO instance = new EmployeeDAO();
        public static EmployeeDAO Instance { get { return instance; } }

        private EmployeeDAO() { }
        public List<Employee> SearchEmployee(string searchType, string keyword)
        {
            List<Employee> list = new List<Employee>();
            string query = @"SELECT employee_id, role_id, full_name, username, 
                            password_hash, phone_number, is_deleted 
                     FROM employees 
                     WHERE is_deleted = 0 AND ";

            switch (searchType)
            {
                case "ID":
                    query += "employee_id = @key";
                    break;
                case "Họ tên":
                    query += "full_name LIKE @key";
                    keyword = "%" + keyword + "%";
                    break;
                case "Username":
                    query += "username LIKE @key";
                    keyword = "%" + keyword + "%";
                    break;
                case "Số điện thoại":
                    query += "phone_number LIKE @key";
                    keyword = "%" + keyword + "%";
                    break;
                default:
                    return GetAllEmployee();
            }

            using (SQLiteConnection conn = DataProvider.GetConnection())
            {
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@key", keyword);
                    conn.Open();
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Employee e = new Employee
                            {
                                Employee_id = Convert.ToInt32(reader["employee_id"]),
                                Full_name = reader["full_name"].ToString(),
                                Is_deleted = Convert.ToInt32(reader["is_deleted"]) == 1,
                                Password_hash = reader["password_hash"].ToString(),
                                Phone_number = reader["phone_number"]?.ToString(),
                                Role_id = Convert.ToInt32(reader["role_id"]),
                                Username = reader["username"].ToString()
                            };
                            list.Add(e);
                        }
                    }
                }
            }
            return list;
        }
        public List<Employee> GetAllEmployee()
        {
            List<Employee> list = new List<Employee>();
            string query = @"SELECT employee_id, role_id, full_name, username, 
                             password_hash, phone_number, is_deleted 
                             FROM employees 
                             WHERE is_deleted = 0";

            using (SQLiteConnection conn = DataProvider.GetConnection())
            {
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    conn.Open();
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Employee e = new Employee();
                            e.Employee_id = Convert.ToInt32(reader["employee_id"]);
                            e.Full_name = reader["full_name"].ToString();
                            e.Is_deleted = Convert.ToInt32(reader["is_deleted"]) == 1;
                            e.Password_hash = reader["password_hash"].ToString();
                            e.Phone_number = reader["phone_number"]?.ToString();
                            e.Role_id = Convert.ToInt32(reader["role_id"]);
                            e.Username = reader["username"].ToString();

                            list.Add(e);
                        }
                    }
                }
            }
            return list;
        }
    }
}