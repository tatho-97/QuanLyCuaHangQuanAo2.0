using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCuaHangQuanAo2._0.DTO
{
    internal class Supplier
    {
        [DisplayName("Mã nhà cung cấp")]
        public int Supplier_id { get; set; }

        [DisplayName("Tên nhà cung cấp")]
        public string Supplier_name { get; set; }

        [DisplayName("Số điện thoại")]
        public string Phone_number { get; set; }

        [Browsable(false)]
        public bool Is_deleted { get; set; }
    }
}