using System;
using System.Collections.Generic;
using System.Windows.Forms;
using QuanLyCuaHangQuanAo2._0.BUS;
using QuanLyCuaHangQuanAo2._0.DTO;

namespace QuanLyCuaHangQuanAo2._0
{

    public partial class ManagerUC_NhapSP : UserControl
    {
        public ManagerUC_NhapSP()
        {
            InitializeComponent();
        }
        Dictionary<string, Product_selected> product_co_san = new Dictionary<string, Product_selected>();
        Dictionary<string, Product> product_moi = new Dictionary<string, Product>();
        private void ManagerUC_NhapSP_Load(object sender, EventArgs e)
        {
            ManagerForm a = (ManagerForm)this.FindForm();
        }

        private void NhapSanPhamMoi(object sender, EventArgs e)
        {

        }
        private void ShowSP()
        {
            dataGridView1.Rows.Clear();
            foreach (Product_selected pd in product_co_san.Values)
            {
                dataGridView1.Rows.Add(pd.MaSP, pd.TenSP, pd.Loai, pd.Size, pd.GiaBan, pd.GiaNhap, pd.SoLuongChon);
            }
            foreach (Product pd in product_moi.Values)
            {
                dataGridView1.Rows.Add(pd.Product_id, pd.Product_name, pd.Category_name, pd.Product_size, pd.Product_sellingPrice, pd.Product_importPrice, pd.Product_stockQuantity);
            }
        }
        private void NhapSanPhamSan(object sender, EventArgs e)
        {
            FormSanPhamCoSan f = new FormSanPhamCoSan();
            f.ShowDialog();
            if(f.list.Count>0)
            {
                foreach (Product_selected pd in f.list)
                {
                    if(!product_co_san.ContainsKey(pd.MaSP))
                    {
                        product_co_san[pd.MaSP] = pd;
                    }
                    else
                    {
                        product_co_san[pd.MaSP].SoLuongChon=
                            Convert.ToInt32(product_co_san[pd.MaSP].SoLuongChon)
                            +
                            Convert.ToInt32(pd.SoLuongChon);
                    }
                }
            }
            ShowSP();
        }
    }
}
