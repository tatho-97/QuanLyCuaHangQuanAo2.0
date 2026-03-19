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
    public partial class StaffUC_Trangchu : UserControl
    {
        public StaffUC_Trangchu()
        {
            InitializeComponent();
        }
        private StaffForm LayFormCha()
        {
            return (StaffForm)this.FindForm();
        }
        private void MoUC(UserControl uc, object sender, EventArgs e)
        {
            StaffForm a = LayFormCha();
            a.chonUC(uc,sender,e);
        }
        private void btnSanPham_Click(object sender, EventArgs e)
        {
            StaffForm a = LayFormCha();
            if(a.ucSanPham == null)
            {
                a.ucSanPham = new StaffUC_SanPham();
            }
            MoUC(a.ucSanPham, sender, e);
        }

        private void btnTaoHoaDon_Click(object sender, EventArgs e)
        {
            StaffForm a = LayFormCha();
            if(a.ucTaoHoaDon == null)
            {
                a.ucTaoHoaDon = new UC_TaoHoaDon();
            }
            MoUC(a.ucTaoHoaDon, sender, e);
        }

        private void btnThongKeSP_Click(object sender, EventArgs e)
        {
            StaffForm a = LayFormCha();
            if(a.ucThongKeDT==null)
            {
                a.ucThongKeDT = new UC_ThongKeDoanhThu();
            }
            MoUC(a.ucThongKeDT, sender, e);
        }

        private void btnThongKeHD_Click(object sender, EventArgs e)
        {
            StaffForm a = LayFormCha();
            if(a.ucThongKeHD==null)
            {
                a.ucThongKeHD = new UC_ThongKeHoaDon();
            }
            MoUC(a.ucThongKeHD, sender, e);
        }
    }
}
