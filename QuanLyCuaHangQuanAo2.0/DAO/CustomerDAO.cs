using BTL_QuanLyKhoHang_Nhom20.DTO;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiTapLon.DAO
{
    internal class CustomerDAO
    {
        private static CustomerDAO instance = new CustomerDAO();

        public static CustomerDAO Instance { get { return instance; } }
        public List<Customer> GetAllCategories()
        {
            List<Customer> list = new List<Customer>();
            string query = "SELECT customer_id, full_name, gender, phone_number, address FROM customers";
            using (SQLiteConnection conn = DataProvider.GetConnection())
            {
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    conn.Open();
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Customer Customer = new Customer
                            {
                                Customer_id = Convert.ToInt32(reader["customer_id"]),
                                Full_name = reader["full_name"].ToString(),
                                Gender = reader["gender"].ToString(),
                                Phone_number = reader["phone_number"].ToString(),
                                Address = reader["address"].ToString()
                            };

                            list.Add(Customer);
                        }
                    }
                }
            }
            return list;
        }
    }
}
