using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_QuanLyKhoHang_Nhom20.DTO
{
    public abstract class User
    {
        [DisplayName("Họ tên")]
        public string Full_name { get; set; }
        [DisplayName("Giới tính")]
        public string Gender { get; set; }
        [DisplayName("Số điện thoại")]
        public string Phone_number { get; set; }
    }
}
