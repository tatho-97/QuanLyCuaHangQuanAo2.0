using BTL_QuanLyKhoHang_Nhom20;
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
    internal class ProductDAO
    {
        private static ProductDAO instance = new ProductDAO();

        public static ProductDAO Instance { get { return instance; } }
        public List<Product> GetAllProducts()
        {
            List<Product> list = new List<Product>();
            string query = @"SELECT p.product_id, p.product_name, c.category_name, 
                            p.size, p.selling_price, p.stock_quantity, p.is_selected
                     FROM products p
                     INNER JOIN categories c ON p.category_id = c.category_id
                     WHERE p.is_deleted = 0";

            using (SQLiteConnection conn = DataProvider.GetConnection())
            {
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    conn.Open();
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read()) // Đọc từng dòng trong database
                        {
                            Product p = new Product
                            {
                                Is_selected = Convert.ToBoolean(reader["is_selected"]),
                                Product_id = Convert.ToInt32(reader["product_id"]),
                                Product_name = reader["product_name"].ToString(),
                                Category_name = reader["category_name"].ToString(),
                                Product_size = reader["size"].ToString(),
                                Product_sellingPrice = Convert.ToSingle(reader["selling_price"]),
                                Product_stockQuantity = Convert.ToInt32(reader["stock_quantity"])
                            };

                            list.Add(p);
                        }
                    }
                }
            }
            return list;
        }

        public bool InsertProduct(Product product)
        {
            string query = @"INSERT INTO products 
                            (is_selected, product_name, category_name, product_size, 
                            product_sellingPrice, product_importPrice, product_stockQuantity, 
                            is_deleted, create_at) 
                            VALUES 
                            (0, @name, @category, @size, 
                            @sell, @import, @stock, 
                            0, CURRENT_TIMESTAMP))";
            using (SQLiteConnection conn = DataProvider.GetConnection())
            {
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", product.Product_name);
                    cmd.Parameters.AddWithValue("@category", product.Category_name);
                    cmd.Parameters.AddWithValue("@size", product.Product_size);
                    cmd.Parameters.AddWithValue("@sell", product.Product_sellingPrice);
                    cmd.Parameters.AddWithValue("@import", product.Product_importPrice);
                    cmd.Parameters.AddWithValue("@stock", product.Product_stockQuantity);

                    conn.Open();
                    int result = cmd.ExecuteNonQuery();

                    return result > 0;
                }
            }
        }

        public bool UpdateProduct(Product product)
        {
            string query = "UPDATE products SET " +
                           "product_name = @name, " +
                           "product_size = @size, " +
                           "product_sellingPrice = @sell, " +
                           "product_importPrice = @import, " +
                           "product_stockQuantity = @stock " +
                           "WHERE product_id = @id";
            using (SQLiteConnection conn = DataProvider.GetConnection())
            {
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", product.Product_id);
                    cmd.Parameters.AddWithValue("@pic", product.Is_deleted);
                    cmd.Parameters.AddWithValue("@name", product.Product_name);
                    cmd.Parameters.AddWithValue("@category", product.Category_name);
                    cmd.Parameters.AddWithValue("@size", product.Product_size);
                    cmd.Parameters.AddWithValue("@sell", product.Product_sellingPrice);
                    cmd.Parameters.AddWithValue("@import", product.Product_importPrice);
                    cmd.Parameters.AddWithValue("@stock", product.Product_stockQuantity);
                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
        }

        public bool DeleteProduct(int id)
        {
            string query = "DELETE FROM products WHERE product_id = @id";

            using (SQLiteConnection conn = DataProvider.GetConnection())
            {
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
        }

        public List<Product> SearchProducts(string searchType, string keyword)
        {
            List<Product> list = new List<Product>();
            string query = @"SELECT p.product_id, p.product_name, c.category_name, 
                            p.size, p.selling_price, p.stock_quantity, p.is_selected
                     FROM products p
                     INNER JOIN categories c ON p.category_id = c.category_id
                     WHERE p.is_deleted = 0 AND ";

            switch (searchType)
            {
                case "ID":
                    query += "p.product_id = @key";
                    break;
                case "Tên sản phẩm":
                    query += "p.product_name LIKE @key";
                    keyword = "%" + keyword + "%";
                    break;
                case "Loại":
                    query += "c.category_name LIKE @key";
                    keyword = "%" + keyword + "%";
                    break;
                case "Giá tiền":
                    query += "p.selling_price <= @key";
                    break;
                default:
                    return GetAllProducts(); 
            }

            using (SQLiteConnection conn = DataProvider.GetConnection())
            {
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@key", keyword);

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Product p = new Product
                            {
                                Product_id = Convert.ToInt32(reader["product_id"]),
                                Product_name = reader["product_name"].ToString(),
                                Category_name = reader["category_name"].ToString(),
                                Product_size = reader["size"].ToString(),
                                Product_sellingPrice = Convert.ToSingle(reader["selling_price"]),
                                Product_stockQuantity = Convert.ToInt32(reader["stock_quantity"]),
                                Is_selected = Convert.ToBoolean(reader["is_selected"])
                            };
                            list.Add(p);
                        }
                    }
                }
            }
            return list;
        }
    }
}
