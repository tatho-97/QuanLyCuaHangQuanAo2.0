using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyCuaHangQuanAo2._0.BUS;
using QuanLyCuaHangQuanAo2._0.DTO;
namespace QuanLyCuaHangQuanAo2._0
{
    public partial class UC_ThongKeHoaDon : UserControl
    {
        public UC_ThongKeHoaDon()
        {
            InitializeComponent();
        }
        public List<Order> hoadon;
        private void UC_ThongKeHoaDon_Load(object sender, EventArgs e)
        {
            hoadon = OrderBUS.Instance.GetAllOrders();
            foreach (Order o in hoadon)
            {
                dataGridView1.Rows.Add(o.Order_id, o.Order_date, o.NhanVien.Full_name, o.KhachHang.Full_name, o.Total_quantity, o.Total_amount, "Xem Thêm");
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6 && e.RowIndex >= 0)
            {
                var selectedId = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();

                foreach (Order o in hoadon)
                {
                    if (o.Order_id.ToString() == selectedId)
                    {
                        MessageBox.Show("Đang mở chi tiết cho hóa đơn: " + o.Order_id);
                        FormChiTietHoaDon f = new FormChiTietHoaDon();
                        f.o = o;
                        break; 
                    }
                }
            }
        }
    }
}
