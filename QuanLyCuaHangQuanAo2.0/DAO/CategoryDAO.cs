using BTL_QuanLyKhoHang_Nhom20.DTO;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaiTapLon.DAO
{
    internal class CategoryDAO
    {
        private static CategoryDAO instance = new CategoryDAO();
        public static CategoryDAO Instance { get { return instance; } }

        public List<Category> GetAllCategories()
        {
            List<Category> list = new List<Category>();
            string query = "SELECT category_id, category_name FROM categories";
            using (SQLiteConnection conn = DataProvider.GetConnection())
            {
                using(SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    conn.Open();
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Category category = new Category 
                            { 
                                Category_id = Convert.ToInt32(reader["category_id"]),
                                Category_name = reader["category_name"].ToString()
                            };

                            list.Add(category);
                        }
                    }
                }
            }
            return list;
        }
    }
}
