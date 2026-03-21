using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCuaHangQuanAo2._0.DTO
{
    public class Import_orders
    {
        public int Import_id { get; set; }
        public int Employee_id { get; set; }
        public DateTime Import_date { get; set; }
        public int Total_amount { get; set; }
        public Employee NhanVien { get; set; }
    }
}