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
        private void MoUC(UserControl uc, object sender, EventArgs e)
        {
            StaffForm a = (StaffForm)this.FindForm();
            a.chonUC(uc,sender,e);
        }
        private void btnSanPham_Click(object sender, EventArgs e)
        {
            MoUC(new StaffUC_SanPham(), sender, e);
        }

        private void btnTaoHoaDon_Click(object sender, EventArgs e)
        {

            MoUC(new UC_TaoHoaDon(), sender, e);
        }

        private void btnThongKeSP_Click(object sender, EventArgs e)
        {
            MoUC(new UC_ThongKeDoanhThu(), sender, e);
        }

        private void btnThongKeHD_Click(object sender, EventArgs e)
        {
            MoUC(new UC_ThongKeHoaDon(), sender, e);
        }
    }
}
