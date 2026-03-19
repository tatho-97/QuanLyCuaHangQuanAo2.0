using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL_QuanLyKhoHang_Nhom20
{
    public class Product
    {
        [DisplayName("Thêm thanh toán")]
        public bool Is_selected { get; set; }
        [DisplayName("Mã sản phẩm")]
        public int Product_id { get; set; }
        [DisplayName("Tên sản phẩm")]
        public string Product_name { get; set; }
        [Browsable(false)]
        public int Category_id { get; set; }
        [DisplayName("Loại")]
        public string Category_name { get; set; }
        [DisplayName("Size")]
        public string Product_size { get; set; }
        [DisplayName("Giá bán")]
        public float Product_sellingPrice { get; set; }
        [Browsable(false)]
        public float Product_importPrice { get; set; }
        [DisplayName("Số lượng tồn")]
        public int Product_stockQuantity { get; set; }
        [Browsable(false)]
        public bool Is_deleted { get; set; }
        [Browsable(false)]
        public DateTime? Deleted_at { get; set; }
    }
}
