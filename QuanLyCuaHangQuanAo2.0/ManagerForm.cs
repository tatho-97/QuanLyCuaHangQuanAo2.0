
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using QuanLyCuaHangQuanAo2._0.BUS;
using QuanLyCuaHangQuanAo2._0.DTO;
namespace QuanLyCuaHangQuanAo2._0
{
    public partial class ManagerForm : Form
    {
        public ManagerForm()
        {
            InitializeComponent();
        }
        public List<Product_selected> product_Selecteds = new List<Product_selected>();
        Button before;
        public ManagerUC_TrangChu ucTrangChu;
        public ManagerUC_SanPham ucSanPham;
        public UC_TaoHoaDon ucTaoHoaDon;
        public UC_ThongKeHoaDon ucThongKeHD;
        public UC_ThongKeDoanhThu ucThongKeDT;
        public ManagerUC_QuanLyNV ucQuanLyNV;
        public ManagerUC_NhapSP ucNhapSP;
        public string ten;
        public int id;
        public Button taohoaDon()
        {
            return btnTaoHoaDon;
        }
        public Employee emp;
        public void chonUC(UserControl uc, object sender, EventArgs e)
        {
            uc.Dock = DockStyle.Fill;
            panelBody.Controls.Clear();
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
                if (before.Text == "Quản lý nhân viên")
                {
                    btnQuanLyNV.BackColor = Color.FromArgb(166, 124, 82);
                    btnQuanLyNV.ForeColor = Color.Black;
                }
                if (before.Text == "Nhập sản phẩm")
                {
                    btnNhapSP.BackColor = Color.FromArgb(166, 124, 82);
                    btnNhapSP.ForeColor = Color.Black;
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
            if (check.Text == "Quản lý nhân viên")
            {
                btnQuanLyNV.BackColor = Color.FromArgb(107, 79, 58);
                btnQuanLyNV.ForeColor = Color.White;
            }
            if (check.Text == "Nhập sản phẩm")
            {
                btnNhapSP.BackColor = Color.FromArgb(107, 79, 58);
                btnNhapSP.ForeColor = Color.White;
            }
            before = check;
        }
        private void ManagerForm_Load(object sender, EventArgs e)
        {
            panelBody.Controls.Clear();
            ManagerUC_TrangChu home = new ManagerUC_TrangChu();
            home.Dock = DockStyle.Fill;
            before = btnTrangChu;
            panelBody.Controls.Add(home);
            textBox1.Text = ten;
            textBox2.Text = id.ToString();
        }

        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            if (ucTrangChu == null) 
                ucTrangChu = new ManagerUC_TrangChu(); 
            chonUC(ucTrangChu, sender, e);
        }

        private void btnSanPham_Click(object sender, EventArgs e)
        {
            if (ucSanPham == null)
                ucSanPham = new ManagerUC_SanPham(); 
            chonUC(ucSanPham, sender, e);
        }

        private void btnTaoHoaDon_Click(object sender, EventArgs e)
        {
            chonUC(new UC_TaoHoaDon(), sender, e);
        }
        private void btnThongKeHD_Click(object sender, EventArgs e)
        {
            chonUC(new UC_ThongKeHoaDon(), sender, e);
        }

        private void btnThongKeDT_Click(object sender, EventArgs e)
        {
            chonUC(new UC_ThongKeDoanhThu(), sender, e);
        }


        private void btnQuanLyNV_Click(object sender, EventArgs e)
        {
            chonUC(new ManagerUC_QuanLyNV(), sender, e);
        }

        private void btnNhapSP_Click(object sender, EventArgs e)
        {
            chonUC(new ManagerUC_NhapSP(), sender, e);
        }

        private void btnThoat_Click_1(object sender, EventArgs e)
        {
            LoginForm a = new LoginForm();
            this.Hide();
            a.ShowDialog();
            this.Close();
        }
    }
}
