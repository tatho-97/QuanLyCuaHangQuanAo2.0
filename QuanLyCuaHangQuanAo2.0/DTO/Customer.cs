using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace QuanLyCuaHangQuanAo2._0.DTO
{ 
    public class Customer : User
    {
        [DisplayName("Mã số")]
        public int Customer_id { get; set; }

        [DisplayName("Địa chỉ")]
        public string Address { get; set; }
    }
}
