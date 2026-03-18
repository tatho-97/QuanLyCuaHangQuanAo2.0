using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_QuanLyKhoHang_Nhom20.DTO
{
    internal class Employee : User
    {
        [DisplayName("Mã số")]
        public int Employee_id {  get; set; }
        [Browsable(false)]
        public int Role_id { get; set; }
        [DisplayName("Tên đăng nhập")]
        public string Username { get; set; }
        [DisplayName("Mật khẩu")]
        public string Password_hash { get; set; }
        [Browsable (false)]
        public bool Is_deleted { get; set; }
    }
}
