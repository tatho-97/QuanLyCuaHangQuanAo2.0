using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace QuanLyCuaHangQuanAo2._0.DTO
{
    public class Product
    {
        public bool Is_selected { get; set; } //
        public int Product_id { get; set; } //
        public string Product_name { get; set; } //
        public string Category_name { get; set; } //
        public string Product_size { get; set; } //
        public int Product_sellingPrice { get; set; } //
        public int Product_importPrice { get; set; } //
        public int Product_stockQuantity { get; set; } //
        public bool Is_deleted { get; set; } = false; //
        public DateTime? Deleted_at { get; set; } //
    }
}