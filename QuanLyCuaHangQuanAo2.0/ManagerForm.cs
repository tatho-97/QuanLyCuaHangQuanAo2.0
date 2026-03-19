using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BaiTapLon.BUS;
using BaiTapLon.DAO;
using BTL_QuanLyKhoHang_Nhom20;
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
        public Button taohoaDon()
        {
            return btnTaoHoaDon;
        }
        public void chonUC(UserControl uc, object sender, EventArgs e)
        {
            if (!panelBody.Controls.Contains(uc))
            {
                uc.Dock = DockStyle.Fill;
                panelBody.Controls.Add(uc);
            }
            uc.BringToFront();
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
            if (ucTaoHoaDon == null)
                ucTaoHoaDon = new UC_TaoHoaDon(); 
            chonUC(ucTaoHoaDon, sender, e);
        }
        private void btnThongKeHD_Click(object sender, EventArgs e)
        {
            if (ucThongKeHD == null)
                ucThongKeHD = new UC_ThongKeHoaDon();
            chonUC(ucThongKeHD, sender, e);
        }

        private void btnThongKeDT_Click(object sender, EventArgs e)
        {
            if (ucThongKeDT == null)
                ucThongKeDT = new UC_ThongKeDoanhThu(); 
            chonUC(ucThongKeDT, sender, e);
        }


        private void btnQuanLyNV_Click(object sender, EventArgs e)
        {
            if (ucQuanLyNV == null) 
                ucQuanLyNV = new ManagerUC_QuanLyNV();
            chonUC(ucQuanLyNV, sender, e);
        }

        private void btnNhapSP_Click(object sender, EventArgs e)
        {
            if (ucNhapSP == null)
                ucNhapSP = new ManagerUC_NhapSP(); 
            chonUC(ucNhapSP, sender, e);
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
