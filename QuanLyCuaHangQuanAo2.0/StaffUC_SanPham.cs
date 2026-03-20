using System;
using System.Collections.Generic;
using System.Windows.Forms;
using QuanLyCuaHangQuanAo2._0.BUS;
using QuanLyCuaHangQuanAo2._0.DTO;
namespace QuanLyCuaHangQuanAo2._0
{
    public partial class StaffUC_SanPham : UserControl
    {
        public StaffUC_SanPham()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void StaffUC_SanPham_Load(object sender, EventArgs e)
        {
            cboTimKiem.Items.Clear();
            cboTimKiem.Items.Add("Tên sản phẩm");
            cboTimKiem.Items.Add("ID");
            cboTimKiem.Items.Add("Loại");
            cboTimKiem.Items.Add("Giá tiền");
            cboTimKiem.SelectedIndex = 0;
            /*
            dataGridView1.DataSource = ProductBUS.Instance.GetAllProducts();
                */
            dataGridView1.Rows.Clear();
            List<Product> data = ProductBUS.Instance.GetAllProducts();
            foreach (Product p in data)
            {
                if (!p.Is_deleted)
                    dataGridView1.Rows.Add(false, p.Product_id, p.Product_name, p.Category_name, p.Product_size, p.Product_sellingPrice, p.Product_importPrice, p.Product_stockQuantity, "");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StaffForm a = (StaffForm)this.FindForm();
            // Luôn khởi tạo mới để UC_TaoHoaDon chạy lại sự kiện Load
            a.ucTaoHoaDon = new UC_TaoHoaDon();
            a.chonUC(a.ucTaoHoaDon, a.taohoaDon(), e);
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            StaffForm a = (StaffForm)this.FindForm();
            if (e.RowIndex < 0) return;

            // chỉ xử lý khi là cột checkbox (ví dụ cột 0)
            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn)
            {
                //tu suy kieu
                var cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                bool isChecked = false;
                if (cell.Value != null)
                {
                    isChecked = (bool)cell.Value;
                }
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                if (isChecked)
                {
                    FormNhapSoLuongChon slc = new FormNhapSoLuongChon();
                    string tenSP = row.Cells[2].Value.ToString();
                    int soluongton = Convert.ToInt32(row.Cells[7].Value);
                    slc.soluongton = soluongton;
                    slc.tenSP = tenSP;
                    slc.ShowDialog();
                    if (slc.soluongchon == "")
                    {
                        cell.Value = false;
                        return;
                    }
                    row.Cells[8].Value = slc.soluongchon;
                    bool exists = false;
                    foreach (Product_selected pd in a.product_Selecteds)
                    {
                        if (pd.MaSP == row.Cells[1].Value.ToString())
                        {
                            exists = true;
                            pd.SoLuongChon = Convert.ToInt32(slc.soluongchon);
                            break;
                        }
                    }
                    if (!exists)
                    {
                        Product_selected pd = new Product_selected();
                        pd.MaSP = row.Cells[1].Value.ToString();
                        pd.TenSP = row.Cells[2].Value.ToString();
                        pd.Loai = row.Cells[3].Value.ToString();
                        pd.Size = row.Cells[4].Value.ToString();
                        pd.GiaBan = Convert.ToInt32(row.Cells[5].Value);
                        pd.GiaNhap = Convert.ToInt32(row.Cells[6].Value);
                        pd.SoLuongTon = Convert.ToInt32(row.Cells[7].Value);
                        pd.SoLuongChon = Convert.ToInt32(row.Cells[8].Value);
                        pd.ThanhTien = pd.SoLuongChon * pd.GiaBan;
                        a.product_Selecteds.Add(pd);
                    }
                }
                else
                {
                    row.Cells[8].Value = "";
                    foreach (Product_selected pd in a.product_Selecteds)
                    {
                        if (pd.MaSP == row.Cells[1].Value.ToString())
                        {
                            a.product_Selecteds.Remove(pd);
                            break;
                        }
                    }
                }
            }
        }

        private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView1.IsCurrentCellDirty)
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
    }
}
