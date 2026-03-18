using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_QuanLyKhoHang_Nhom20.DTO
{
    internal class Category
    {
        [DisplayName("ID Nhà cung cấp")]
        public int Category_id { get; set; }
        [DisplayName("Tên nhà cung cấp")]
        public string Category_name { get; set; }
    }
}
