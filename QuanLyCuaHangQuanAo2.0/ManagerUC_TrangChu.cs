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
    public partial class ManagerUC_TrangChu : UserControl
    {
        public ManagerUC_TrangChu()
        {
            InitializeComponent();
        }
        private ManagerForm LayFormCha()
        {
            return (ManagerForm)this.FindForm();
        }

        private void MoUC(UserControl uc, object sender, EventArgs e)
        {
            ManagerForm fCha = LayFormCha();
            if (fCha != null && uc != null)
            {
                fCha.chonUC(uc, sender, e);
            }
        }

        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            ManagerForm fCha = LayFormCha();
            if (fCha.ucTrangChu == null)
            {
                fCha.ucTrangChu = new ManagerUC_TrangChu();
            }
            MoUC(fCha.ucTrangChu, sender, e);
        }

        private void btnSanPham_Click(object sender, EventArgs e)
        {
            ManagerForm fCha = LayFormCha();
            if (fCha.ucSanPham == null)
            {
                fCha.ucSanPham = new ManagerUC_SanPham();
            }
            MoUC(fCha.ucSanPham, sender, e);
        }

        private void btnTaoHoaDon_Click(object sender, EventArgs e)
        {
            ManagerForm fCha = LayFormCha();
            if (fCha.ucTaoHoaDon == null)
            {
                fCha.ucTaoHoaDon = new UC_TaoHoaDon();
            }
            MoUC(fCha.ucTaoHoaDon, sender, e);
        }

        private void btnThongKeSP_Click(object sender, EventArgs e)
        {
            ManagerForm fCha = LayFormCha();
            if (fCha.ucThongKeDT == null)
            {
                fCha.ucThongKeDT = new UC_ThongKeDoanhThu();
            }
            MoUC(fCha.ucThongKeDT, sender, e);
        }

        private void btnThongKeHD_Click(object sender, EventArgs e)
        {
            ManagerForm fCha = LayFormCha();
            if (fCha.ucThongKeHD == null)
            {
                fCha.ucThongKeHD = new UC_ThongKeHoaDon();
            }
            MoUC(fCha.ucThongKeHD, sender, e);
        }

        private void btnQuanLyNV_Click(object sender, EventArgs e)
        {
            ManagerForm fCha = LayFormCha();
            if (fCha.ucQuanLyNV == null)
            {
                fCha.ucQuanLyNV = new ManagerUC_QuanLyNV();
            }
            MoUC(fCha.ucQuanLyNV, sender, e);
        }

        private void btnNhapSP_Click(object sender, EventArgs e)
        {
            ManagerForm fCha = LayFormCha();
            if (fCha.ucNhapSP == null)
            {
                fCha.ucNhapSP = new ManagerUC_NhapSP();
            }
            MoUC(fCha.ucNhapSP, sender, e);
        }
        private void ManagerUC_TrangChu_Load(object sender, EventArgs e)
        {

        }
    }
}