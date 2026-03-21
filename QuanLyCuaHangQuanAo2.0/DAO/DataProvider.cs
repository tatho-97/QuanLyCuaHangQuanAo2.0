using System.Data.SQLite;

namespace QuanLyCuaHangQuanAo2._0.DAO 
{
    public class DataProvider
    {
        private static string connectionString = "Data Source=QuanLyCuaHangQuanAo.db;Version=3;";

        public static SQLiteConnection GetConnection()
        {
            string connectionString = @"Data Source=.\QuanLyCuaHangQuanAo.db;Version=3;";
            return new SQLiteConnection(connectionString);
        }
    }
}