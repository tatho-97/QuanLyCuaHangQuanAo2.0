using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCuaHangQuanAo2._0
{
    public class Product_selected
    {
        // Các thuộc tính kiểu Chuỗi (String)
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public string Loai { get; set; }
        public string Size { get; set; }

        public int GiaBan { get; set; }
        public int GiaNhap { get; set; }
        public int SoLuongTon { get; set; }
        public int SoLuongChon { get; set; }

        public int ThanhTien { get; set; }

        public Product_selected() { }
    }
}
