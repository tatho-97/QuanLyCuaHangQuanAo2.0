using System;
using System.Data.SQLite;
using QuanLyCuaHangQuanAo2._0.DTO;
using System.Collections.Generic;
namespace QuanLyCuaHangQuanAo2._0.DAO
{
    public class OrderDetailDAO
    {
        private static OrderDetailDAO instance;

        public static OrderDetailDAO Instance
        {
            get { if (instance == null) instance = new OrderDetailDAO(); return instance; }
        }

        private OrderDetailDAO() { }

        public bool InsertOrderDetail(OrderDetail detail)
        {
            string queryInsert = @"INSERT INTO order_details (order_id, product_id, quantity, unit_price) 
                           VALUES (@orderId, @productId, @quantity, @price)";

            using (SQLiteConnection conn = DataProvider.GetConnection())
            {
                using (SQLiteCommand cmdInsert = new SQLiteCommand(queryInsert, conn))
                {
                    cmdInsert.Parameters.AddWithValue("@orderId", detail.Order_id);
                    cmdInsert.Parameters.AddWithValue("@productId", detail.Product_id);
                    cmdInsert.Parameters.AddWithValue("@quantity", detail.Quantity);
                    cmdInsert.Parameters.AddWithValue("@price", detail.Unit_price);

                    conn.Open();
                    return cmdInsert.ExecuteNonQuery() > 0;
                }
            }
        }
        public List<OrderDetail> GetAllOrderDetail(Order o)
        {
            List<OrderDetail> list = new List<OrderDetail>();

            // Câu lệnh truy vấn kết hợp bảng order_details và products
            string query = @"SELECT od.*, p.product_name 
                     FROM order_details od
                     JOIN products p ON od.product_id = p.product_id
                     WHERE od.order_id = @orderId";

            using (SQLiteConnection conn = DataProvider.GetConnection())
            {
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    // Truyền tham số để chống SQL Injection
                    cmd.Parameters.AddWithValue("@orderId", o.Order_id);

                    conn.Open();
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            OrderDetail detail = new OrderDetail
                            {
                                Order_id = Convert.ToInt32(reader["order_id"]),
                                Product_id = Convert.ToInt32(reader["product_id"]),
                                Quantity = Convert.ToInt32(reader["quantity"]),
                                Unit_price = Convert.ToDouble(reader["unit_price"]),

                                // Khởi tạo đối tượng SanPham để chứa tên sản phẩm
                                SanPham = new Product
                                {
                                    Product_name = reader["product_name"].ToString()
                                }
                            };
                            list.Add(detail);
                        }
                    }
                }
            }
            return list;
        }
    }
}