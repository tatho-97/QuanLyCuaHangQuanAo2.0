using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_QuanLyKhoHang_Nhom20.DTO
{
    internal class Role
    {
        [Browsable(false)]
        public int Role_id { get; set; }
        public int Role_name { get; set; }
    }
}
