using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCuaHangQuanAo2._0.DTO
{
    public class OrderDetail
    {
        public int Order_id { get; set; }
        public int Product_id { get; set; }
        public Product SanPham { get; set; }
        public int Quantity { get; set; }
        public double Unit_price { get; set; }
    }
}