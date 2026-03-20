using System;
using System.Collections.Generic;
using System.Data.SQLite;
using QuanLyCuaHangQuanAo2._0.DTO; 

namespace QuanLyCuaHangQuanAo2._0.DAO
{
    internal class RoleDAO
    {
        private static RoleDAO instance;

        public static RoleDAO Instance
        {
            get { if (instance == null) instance = new RoleDAO(); return instance; }
        }

        private RoleDAO() { }

        public List<Role> GetListRole()
        {
            List<Role> list = new List<Role>();
            string query = "SELECT role_id, role_name FROM roles";

            using (SQLiteConnection conn = DataProvider.GetConnection())
            {
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    conn.Open();
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Role role = new Role();
                            role.Role_id = Convert.ToInt32(reader["role_id"]);
                            role.Role_name = reader["role_name"].ToString();

                            list.Add(role);
                        }
                    }
                }
            }
            return list;
        }
    }
}