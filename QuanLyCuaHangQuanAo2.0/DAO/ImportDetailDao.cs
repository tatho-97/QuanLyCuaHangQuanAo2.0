using System;
using System.Data.SQLite;
using QuanLyCuaHangQuanAo2._0.DTO;

namespace QuanLyCuaHangQuanAo2._0.DAO
{
    public class ImportDetailDAO
    {
        private static ImportDetailDAO instance;

        public static ImportDetailDAO Instance
        {
            get { if (instance == null) instance = new ImportDetailDAO(); return instance; }
        }

        private ImportDetailDAO() { }

        public bool InsertImportDetail(ImportDetail detail)
        {
            string queryInsert = @"INSERT INTO import_details (import_id, product_id, quantity, import_price) 
                                   VALUES (@importId, @productId, @quantity, @price)";

            using (SQLiteConnection conn = DataProvider.GetConnection())
            {
                using (SQLiteCommand cmdInsert = new SQLiteCommand(queryInsert, conn))
                {
                    cmdInsert.Parameters.AddWithValue("@importId", detail.Import_id);
                    cmdInsert.Parameters.AddWithValue("@productId", detail.Product_id);
                    cmdInsert.Parameters.AddWithValue("@quantity", detail.Quantity);
                    cmdInsert.Parameters.AddWithValue("@price", detail.Import_price);

                    conn.Open();
                    return cmdInsert.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}