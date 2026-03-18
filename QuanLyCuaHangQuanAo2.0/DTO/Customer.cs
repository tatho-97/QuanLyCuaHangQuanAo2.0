using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_QuanLyKhoHang_Nhom20.DTO
{
    internal class Customer : User
    {
        [DisplayName("Mã số")]
        public int Customer_id { get; set; }
        [DisplayName("Địa chỉ")]
        public string Address { get; set; }
    }
}
