using QuanLyCuaHangQuanAo2._0.DTO;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace QuanLyCuaHangQuanAo2._0.DAO
{
    internal class ProductDAO
    {
        private static ProductDAO instance = new ProductDAO();
        public static ProductDAO Instance { get { return instance; } }

        private ProductDAO() { }

        public List<Product> GetAllProducts()
        {
            List<Product> list = new List<Product>();
            string query = "SELECT * FROM products WHERE is_deleted = 0";

            using (SQLiteConnection conn = DataProvider.GetConnection())
            {
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    conn.Open();
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Product
                            {
                                Product_id = Convert.ToInt32(reader["product_id"]),
                                Product_name = reader["product_name"].ToString(),
                                Category_name = reader["category_name"]?.ToString(),
                                Product_size = reader["product_size"]?.ToString(),
                                Product_sellingPrice = Convert.ToInt32(reader["product_sellingPrice"]),
                                Product_importPrice = Convert.ToInt32(reader["product_importPrice"]),
                                Product_stockQuantity = Convert.ToInt32(reader["product_stockQuantity"]),
                                Is_selected = Convert.ToInt32(reader["is_selected"]) == 1,
                                Is_deleted = Convert.ToInt32(reader["is_deleted"]) == 1
                            });
                        }
                    }
                }
            }
            return list;
        }

        public int InsertProduct(Product product)
        {
            int productId = -1;

            // Thêm SELECT last_insert_rowid() để lấy ID vừa tự động tăng
            string query = @"INSERT INTO products 
                    (product_name, category_name, product_size, 
                     product_sellingPrice, product_importPrice, product_stockQuantity, 
                     is_deleted, is_selected) 
                    VALUES 
                    (@name, @category, @size, 
                     @sell, @import, @stock, 0, 0);
                    SELECT last_insert_rowid();";//lay dong insert gan nhat

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

                    // Dùng ExecuteScalar để lấy giá trị ID trả về ở dòng cuối cùng
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        productId = Convert.ToInt32(result);
                    }
                }
            }

            return productId;
        }

        public bool UpdateProduct(Product product)
        {
            string query = @"UPDATE products SET 
                            product_name = @name, 
                            category_name = @category,
                            product_size = @size, 
                            product_sellingPrice = @sell, 
                            product_importPrice = @import, 
                            product_stockQuantity = @stock 
                            WHERE product_id = @id";

            using (SQLiteConnection conn = DataProvider.GetConnection())
            {
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", product.Product_id);
                    cmd.Parameters.AddWithValue("@name", product.Product_name);
                    cmd.Parameters.AddWithValue("@category", product.Category_name);
                    cmd.Parameters.AddWithValue("@size", product.Product_size);
                    cmd.Parameters.AddWithValue("@sell", product.Product_sellingPrice);
                    cmd.Parameters.AddWithValue("@import", product.Product_importPrice);
                    cmd.Parameters.AddWithValue("@stock", product.Product_stockQuantity);

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public List<Product> SearchProducts(string searchType, string keyword)
        {
            List<Product> list = new List<Product>();
            string query = @"SELECT product_id, product_name, category_name, 
                            product_size, product_sellingPrice, product_importPrice, 
                            product_stockQuantity, is_selected, is_deleted 
                            FROM products 
                            WHERE is_deleted = 0 AND ";

            switch (searchType)
            {
                case "ID":
                    query += "product_id = @key";
                    break;
                case "Tên sản phẩm":
                    query += "product_name LIKE @key";
                    keyword = "%" + keyword + "%";
                    break;
                case "Loại":
                    query += "category_name LIKE @key";
                    keyword = "%" + keyword + "%";
                    break;
                case "Giá tiền":
                    query += "product_sellingPrice <= @key";
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
                            list.Add(new Product
                            {
                                Product_id = Convert.ToInt32(reader["product_id"]),
                                Product_name = reader["product_name"].ToString(),
                                Category_name = reader["category_name"]?.ToString(),
                                Product_size = reader["product_size"]?.ToString(),
                                Product_sellingPrice = Convert.ToInt32(reader["product_sellingPrice"]),
                                Product_importPrice = Convert.ToInt32(reader["product_importPrice"]),
                                Product_stockQuantity = Convert.ToInt32(reader["product_stockQuantity"]),
                                Is_selected = Convert.ToInt32(reader["is_selected"]) == 1,
                                Is_deleted = Convert.ToInt32(reader["is_deleted"]) == 1
                            });
                        }
                    }
                }
            }
            return list;
        }
    }
}
