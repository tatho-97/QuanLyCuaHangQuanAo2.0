using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCuaHangQuanAo2._0.DTO
{
    public class ImportDetail
    {
        public int Import_detail_id { get; set; }
        public int Import_id { get; set; }
        public int Product_id { get; set; }
        public int Quantity { get; set; }
        public int Import_price { get; set; }
    }
}