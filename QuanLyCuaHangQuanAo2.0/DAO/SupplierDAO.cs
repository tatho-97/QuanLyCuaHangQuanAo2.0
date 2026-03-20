using System;
using System.Collections.Generic;
using System.Data.SQLite;
using QuanLyCuaHangQuanAo2._0.DTO; 

namespace QuanLyCuaHangQuanAo2._0.DAO 
{
    internal class SupplierDAO
    {
        private static SupplierDAO instance;

        public static SupplierDAO Instance
        {
            get { if (instance == null) instance = new SupplierDAO(); return instance; }
        }

        private SupplierDAO() { }

        public List<Supplier> GetAllSuppliers()
        {
            List<Supplier> list = new List<Supplier>();
            string query = "SELECT supplier_id, supplier_name, phone_number, address FROM suppliers";

            using (SQLiteConnection conn = DataProvider.GetConnection())
            {
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    conn.Open();
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Supplier s = new Supplier
                            {
                                Supplier_id = Convert.ToInt32(reader["supplier_id"]),
                                Supplier_name = reader["supplier_name"].ToString(),
                                Phone_number = reader["phone_number"]?.ToString(),
                                Is_deleted =Convert.ToBoolean( reader["is_deleted"])
                            };
                            list.Add(s);
                        }
                    }
                }
            }
            return list;
        }
    }
}