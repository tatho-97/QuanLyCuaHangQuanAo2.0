using System;
using System.Collections.Generic;
using System.Data.SQLite;
using QuanLyCuaHangQuanAo2._0.DTO; 

namespace QuanLyCuaHangQuanAo2._0.DAO 
{
    internal class CustomerDAO
    {
        private static CustomerDAO instance = new CustomerDAO();
        public static CustomerDAO Instance { get { return instance; } }

        private CustomerDAO() { } // 

        public List<Customer> GetAllCustomers()
        {
            List<Customer> list = new List<Customer>();


            string query = "SELECT customer_id, full_name, phone_number, address FROM customers";

            using (SQLiteConnection conn = DataProvider.GetConnection())
            {
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    conn.Open();
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Customer customer = new Customer
                            {
                                Customer_id = Convert.ToInt32(reader["customer_id"]),
                                Full_name = reader["full_name"].ToString(),
                                // Gender = reader["gender"].ToString(), // Bỏ nếu DB không có cột này
                                Phone_number = reader["phone_number"]?.ToString(),
                                Address = reader["address"]?.ToString()
                            };

                            list.Add(customer);
                        }
                    }
                }
            }
            return list;
        }
        public List<Customer> SearchCustomer(string ten,string sdt)
        {
            List<Customer> list = new List<Customer>();
            string query = @"SELECT customer_id, full_name, phone_number, address 
                    FROM customers 
                    WHERE (full_name LIKE @key1 and phone_number LIKE @key2)";

            using (SQLiteConnection conn = DataProvider.GetConnection())
            {
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@key1", "%" + ten + "%");
                    cmd.Parameters.AddWithValue("@key2", "%" + sdt + "%");


                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Customer
                            {
                                Customer_id = Convert.ToInt32(reader["customer_id"]),
                                Full_name = reader["full_name"].ToString(),
                                Phone_number = reader["phone_number"]?.ToString(),
                                Address = reader["address"]?.ToString()
                            }) ;
                        }
                    }
                }
            }
            return list;
        }
        public bool InsertCustomer(string ten,string sdt)
        {
            string query = @"INSERT INTO customers 
                            (full_name,phone_number,address) 
                            VALUES 
                            (@name, @phone, @add)";

            using (SQLiteConnection conn = DataProvider.GetConnection())
            {
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", ten);
                    cmd.Parameters.AddWithValue("@phone", sdt);
                    cmd.Parameters.AddWithValue("@add", "");
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}