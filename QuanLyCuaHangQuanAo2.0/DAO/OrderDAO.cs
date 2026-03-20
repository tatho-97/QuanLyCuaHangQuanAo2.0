using System;
using System.Collections.Generic;
using System.Data.SQLite;
using QuanLyCuaHangQuanAo2._0.DTO;

namespace QuanLyCuaHangQuanAo2._0.DAO
{
    public class OrderDAO
    {
        private static OrderDAO instance;

        public static OrderDAO Instance
        {
            get { if (instance == null) instance = new OrderDAO(); return instance; }
        }

        private OrderDAO() { }

        public int InsertOrder(Order obj)
        {
            int orderId = -1;
            string query = @"INSERT INTO orders 
                    (customer_id, employee_id, order_date, total_quantity, total_amount, status) 
                    VALUES (@cusId, @empId, @date, @qty, @amount, @status);
                    SELECT last_insert_rowid();";

            using (SQLiteConnection conn = DataProvider.GetConnection())
            {
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@cusId", obj.Customer_id);
                    cmd.Parameters.AddWithValue("@empId", obj.Employee_id);
                    cmd.Parameters.AddWithValue("@date", obj.Order_date.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@qty", obj.Total_quantity);
                    cmd.Parameters.AddWithValue("@amount", obj.Total_amount);
                    cmd.Parameters.AddWithValue("@status", obj.Status);

                    conn.Open();
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            orderId = Convert.ToInt32(reader[0]);
                        }
                    }
                }
            }
            return orderId;
        }

        public List<Order> GetAllOrders()
        {
            List<Order> list = new List<Order>();
            string query = @"SELECT o.*, c.full_name as CustomerName, e.full_name as EmployeeName 
                            FROM orders o
                            JOIN customers c ON o.customer_id = c.customer_id
                            JOIN employees e ON o.employee_id = e.employee_id";

            using (SQLiteConnection conn = DataProvider.GetConnection())
            {
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    conn.Open();
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Order order = new Order
                            {
                                Order_id = Convert.ToInt32(reader["order_id"]),
                                Customer_id = Convert.ToInt32(reader["customer_id"]),
                                Employee_id = Convert.ToInt32(reader["employee_id"]),
                                Order_date = Convert.ToDateTime(reader["order_date"]),
                                Total_quantity = Convert.ToInt32(reader["total_quantity"]),
                                Total_amount = Convert.ToInt64(reader["total_amount"]),
                                Status = reader["status"].ToString(),
                                KhachHang = new Customer { Full_name = reader["CustomerName"].ToString() },
                                NhanVien = new Employee { Full_name = reader["EmployeeName"].ToString() }
                            };
                            list.Add(order);
                        }
                    }
                }
            }
            return list;
        }

    }
}