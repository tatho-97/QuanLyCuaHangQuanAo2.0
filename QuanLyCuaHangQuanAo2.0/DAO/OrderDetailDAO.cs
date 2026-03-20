using System;
using System.Data.SQLite;
using QuanLyCuaHangQuanAo2._0.DTO;

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
    }
}