using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuaHangQuanAo2._0
{
    public partial class UC_TaoHoaDon : UserControl
    {
        public UC_TaoHoaDon()
        {
            InitializeComponent();
        }
        Form currentForm;
        private void UC_TaoHoaDon_Load(object sender, EventArgs e)
        {
            currentForm = this.FindForm();
            if (currentForm is ManagerForm)
            {
                ManagerForm f = (ManagerForm)currentForm;
                int tongtien = 0;
                foreach (Product_selected pd in f.product_Selecteds)
                {
                    dataGridView1.Rows.Add(pd.MaSP, pd.TenSP, pd.Loai, pd.Size, pd.GiaBan, pd.SoLuongTon, pd.SoLuongChon, pd.ThanhTien);
                    tongtien = tongtien += Convert.ToInt32(pd.ThanhTien);
                }
                textBox3.Text = tongtien.ToString();
            }
            else if (currentForm is StaffForm)
            {
                StaffForm f = (StaffForm)currentForm;
                int tongtien = 0;
                foreach (Product_selected pd in f.product_Selecteds)
                {
                    dataGridView1.Rows.Add(pd.MaSP, pd.TenSP, pd.Loai, pd.Size, pd.GiaBan, pd.SoLuongTon, pd.SoLuongChon, pd.ThanhTien);
                    tongtien = tongtien += Convert.ToInt32(pd.ThanhTien);
                }
                textBox3.Text = tongtien.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            currentForm = this.FindForm();
            if (currentForm is ManagerForm)
            {
                ManagerForm f = (ManagerForm)currentForm;
                f.ucSanPham = new ManagerUC_SanPham();
                MessageBox.Show("Sản phẩm chọn đã được xóa!");
            }
            else if (currentForm is StaffForm)
            {
                StaffForm f = (StaffForm)currentForm;
                f.ucSanPham = new StaffUC_SanPham();
                MessageBox.Show("Sản phẩm chọn đã được xóa!");
            }
            dataGridView1.Rows.Clear();
            textBox4.Text = "";
            textBox5.Text = "";
        }
    }
}
