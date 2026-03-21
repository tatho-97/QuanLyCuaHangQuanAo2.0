using System;
using System.Collections.Generic;
using System.Data.SQLite;
using QuanLyCuaHangQuanAo2._0.DTO;

namespace QuanLyCuaHangQuanAo2._0.DAO
{
    public class ImportDAO
    {
        // --- Áp dụng Singleton Pattern giống OrderDAO ---
        private static ImportDAO instance;

        public static ImportDAO Instance
        {
            get { if (instance == null) instance = new ImportDAO(); return instance; }
        }

        private ImportDAO() { }
        // ------------------------------------------------


        public int InsertImport(Import_orders obj)
        {
            int importId = -1;
            string query = @"INSERT INTO import_orders 
                            (employee_id, import_date, total_amount) 
                            VALUES (@empId, @date, @amount);
                            SELECT last_insert_rowid();";

            using (SQLiteConnection conn = DataProvider.GetConnection())
            {
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@empId", obj.Employee_id);
                    cmd.Parameters.AddWithValue("@date", obj.Import_date.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@amount", obj.Total_amount);

                    conn.Open();
                    // Dùng ExecuteScalar sẽ tối ưu hơn ExecuteReader khi chỉ cần lấy 1 giá trị (ID)
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        importId = Convert.ToInt32(result);
                    }
                }
            }
            return importId;
        }


        public List<Import_orders> GetAllImports()
        {
            List<Import_orders> list = new List<Import_orders>();

            // JOIN với bảng employees để lấy tên nhân viên
            string query = @"SELECT i.*, e.full_name as EmployeeName 
                             FROM import_orders i
                             JOIN employees e ON i.employee_id = e.employee_id";

            using (SQLiteConnection conn = DataProvider.GetConnection())
            {
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    conn.Open();
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Import_orders importOrder = new Import_orders
                            {
                                Import_id = Convert.ToInt32(reader["import_id"]),
                                Employee_id = Convert.ToInt32(reader["employee_id"]),
                                Import_date = Convert.ToDateTime(reader["import_date"]),
                                Total_amount = Convert.ToInt32(reader["total_amount"]),

                                NhanVien = new Employee { Full_name = reader["EmployeeName"].ToString() }
                            };
                            list.Add(importOrder);
                        }
                    }
                }
            }
            return list;
        }
    }
}