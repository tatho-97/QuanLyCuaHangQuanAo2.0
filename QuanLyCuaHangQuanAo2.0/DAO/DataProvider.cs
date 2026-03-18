using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace BaiTapLon.DAO
{
    public class DataProvider
    {
        private static string connectionString = "Data Source=QuanLyCuaHangQuanAo.db;Version=3;";

        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(connectionString);
        }
    }
}
