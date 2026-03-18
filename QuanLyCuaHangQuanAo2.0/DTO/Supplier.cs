using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_QuanLyKhoHang_Nhom20.DTO
{
    internal class Supplier
    {
        [DisplayName("Mã nhà cung cấp")]
        public int Supplier_id { get; set; }
        [DisplayName("Tên nhà cung cấp")]
        public string Supplier_name { get; set; }
        [DisplayName("Số điện thoại")]
        public string Phone_number { get; set; }
        [DisplayName("Địa chỉ")]
        public string Address { get; set; }
    }
}
