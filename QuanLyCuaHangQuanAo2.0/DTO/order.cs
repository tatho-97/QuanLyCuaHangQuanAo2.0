using System;

namespace QuanLyCuaHangQuanAo2._0.DTO
{
    public class Order
    {
        public int Order_id { get; set; }
        public int Customer_id { get; set; }
        public int Employee_id { get; set; }
        public Employee NhanVien { get; set; }
        public Customer KhachHang { get; set; }

        public DateTime Order_date { get; set; }
        public int Total_quantity { get; set; }
        public long Total_amount { get; set; }
        public string Status { get; set; }
    }
}