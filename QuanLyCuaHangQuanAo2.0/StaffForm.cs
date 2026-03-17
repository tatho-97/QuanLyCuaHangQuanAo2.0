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
    public partial class StaffForm : Form
    {
        public StaffForm()
        {
            InitializeComponent();
        }
        Button before;
        public void chonUC(UserControl uc, object sender, EventArgs e)
        {
            panelBody.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            panelBody.Controls.Add(uc);

            Button check = sender as Button;
            if (check == null) return;

            // reset button cũ
            if (before != null)
            {
                if (before.Text == "Trang chủ")
                {
                    btnTrangChu.BackColor = Color.FromArgb(166, 124, 82);
                    btnTrangChu.ForeColor = Color.Black;
                }
                if (before.Text == "Sản phẩm")
                {
                    btnSanPham.BackColor = Color.FromArgb(166, 124, 82);
                    btnSanPham.ForeColor = Color.Black;
                }
                if (before.Text == "Tạo hóa đơn")
                {
                    btnTaoHoaDon.BackColor = Color.FromArgb(166, 124, 82);
                    btnTaoHoaDon.ForeColor = Color.Black;
                }
                if (before.Text == "Thống kê hóa đơn")
                {
                    btnThongKeHD.BackColor = Color.FromArgb(166, 124, 82);
                    btnThongKeHD.ForeColor = Color.Black;
                }
                if (before.Text == "Thống kê doanh thu")
                {
                    btnThongKeDT.BackColor = Color.FromArgb(166, 124, 82);
                    btnThongKeDT.ForeColor = Color.Black;
                }
            }

            // đổi màu button mới
            if (check.Text == "Trang chủ")
            {
                btnTrangChu.BackColor = Color.FromArgb(107, 79, 58);
                btnTrangChu.ForeColor = Color.White;
            }
            if (check.Text == "Sản phẩm")
            {
                btnSanPham.BackColor = Color.FromArgb(107, 79, 58);
                btnSanPham.ForeColor = Color.White;
            }
            if (check.Text == "Tạo hóa đơn")
            {
                btnTaoHoaDon.BackColor = Color.FromArgb(107, 79, 58);
                btnTaoHoaDon.ForeColor = Color.White;
            }
            if (check.Text == "Thống kê hóa đơn")
            {
                btnThongKeHD.BackColor = Color.FromArgb(107, 79, 58);
                btnThongKeHD.ForeColor = Color.White;
            }
            if (check.Text == "Thống kê doanh thu")
            {
                btnThongKeDT.BackColor = Color.FromArgb(107, 79, 58);
                btnThongKeDT.ForeColor = Color.White;
            }
            before = check;
        }
        private void StaffForm_Load(object sender, EventArgs e)
        {
            panelBody.Controls.Clear();
            StaffUC_Trangchu home = new StaffUC_Trangchu();
            home.Dock = DockStyle.Fill;
            before = btnTrangChu;
            panelBody.Controls.Add(home);
        }

        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            chonUC(new StaffUC_Trangchu(),sender, e);
        }

        private void btnSanPham_Click(object sender, EventArgs e)
        {
            chonUC(new StaffUC_SanPham(),sender,e);
        }

        private void btnTaoHoaDon_Click(object sender, EventArgs e)
        {
            chonUC(new UC_TaoHoaDon (), sender, e);
        }
        private void btnThongKeHD_Click(object sender, EventArgs e)
        {
            chonUC(new UC_ThongKeHoaDon(), sender, e);
        }

        private void btnThongKeDT_Click(object sender, EventArgs e)
        {
            chonUC(new UC_ThongKeDoanhThu(), sender, e);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            LoginForm a = new LoginForm();
            this.Hide();
            a.ShowDialog();
            this.Close();
        }
    }
}
