using System;
using System.Collections.Generic;
using System.Windows.Forms;
using QuanLyCuaHangQuanAo2._0.BUS;
using QuanLyCuaHangQuanAo2._0.DTO;

namespace QuanLyCuaHangQuanAo2._0
{
    public partial class ManagerUC_SanPham : UserControl
    {
        public ManagerUC_SanPham()
        {
            InitializeComponent();
        }

        private void ManagerUC_SanPham_Load(object sender, EventArgs e)
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
                dataGridView1.Rows.Add(false, p.Product_id, p.Product_name, p.Category_name,
                                       p.Product_size, p.Product_sellingPrice,
                                       p.Product_importPrice,
                                       p.Product_stockQuantity, "");
            }
        }

        private void btTimKiem_Click(object sender, EventArgs e)
        {
            string type = cboTimKiem.Text;
            string keyword = tbTimKiem.Text;
            List<Product> results = ProductBUS.Instance.SearchProducts(type, keyword);
            dataGridView1.Rows.Clear();
            foreach (Product p in results)
            {
                dataGridView1.Rows.Add(false, p.Product_id, p.Product_name, p.Category_name,
                                       p.Product_size, p.Product_sellingPrice, p.Product_importPrice,
                                       p.Product_stockQuantity, "");
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            ManagerForm a = (ManagerForm)this.FindForm();
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
                    int soluongton = Convert.ToInt32(row.Cells[7].Value);
                    if (soluongton <= 0)
                    {
                        MessageBox.Show("Hàng không có sẵn");
                        return;
                    }
                    FormNhapSoLuongChon slc = new FormNhapSoLuongChon();
                    string tenSP = row.Cells[2].Value.ToString();
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

        private void TaoHoaDon(object sender, EventArgs e)
        {
            ManagerForm a = (ManagerForm)this.FindForm();
            // Luôn khởi tạo mới để UC_TaoHoaDon chạy lại sự kiện Load
            a.ucTaoHoaDon = new UC_TaoHoaDon();
            a.chonUC(a.ucTaoHoaDon, a.taohoaDon(), e);
        }

        private void btnSuaSanPham_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.CurrentRow;
                FormSuaSanPham sua = new FormSuaSanPham();
                sua.TenSP = row.Cells[2].Value.ToString();
                sua.Loai = row.Cells[3].Value.ToString();
                sua.size = row.Cells[4].Value.ToString();
                sua.GiaBan = row.Cells[5].Value.ToString();
                sua.giaNhap = row.Cells[6].Value.ToString();
                sua.soluongton = row.Cells[7].Value.ToString();
                sua.ShowDialog();
                // lay thay doi
                if (sua.Tag.ToString() == "0")
                {
                    sua.Close();
                }
                if (sua.Tag.ToString() == "1")
                {
                    row.Cells[2].Value = sua.TenSP;
                    row.Cells[3].Value = sua.Loai;
                    row.Cells[4].Value = sua.size;
                    row.Cells[5].Value = sua.GiaBan;
                    row.Cells[6].Value = sua.giaNhap;
                    row.Cells[7].Value = sua.soluongton;
                    Product p = new Product();
                    p.Product_id = Convert.ToInt32(row.Cells[1].Value);
                    p.Product_name = row.Cells[2].Value.ToString();
                    p.Is_deleted = false;
                    p.Category_name = row.Cells[3].Value.ToString();
                    p.Product_size = row.Cells[4].Value.ToString();
                    p.Product_sellingPrice = Convert.ToInt32(row.Cells[5].Value);
                    p.Product_importPrice = Convert.ToInt32(row.Cells[6].Value);
                    p.Product_stockQuantity = Convert.ToInt32(row.Cells[7].Value);
                    sua.Close();

                    if (ProductBUS.Instance.UpdateProduct(p))
                    {
                        MessageBox.Show("Dữ liệu sửa thành công");
                        this.ManagerUC_SanPham_Load(sender,e);
                    }
                    else
                    {
                        MessageBox.Show("Dữ liệu sửa thất bại");
                    }
                }
            }
        }

        private void ThemSanPham_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormThemSanPham frmThem = new FormThemSanPham();
            frmThem.ShowDialog();

            if (frmThem.Tag != null && frmThem.Tag.ToString() == "1")
            {
                Product p = new Product();
                p.Product_name = frmThem.TenSP;
                p.Category_name = frmThem.Loai;
                p.Product_size = frmThem.size;
                p.Product_sellingPrice = Convert.ToInt32(frmThem.GiaBan);
                p.Product_importPrice = Convert.ToInt32(frmThem.giaNhap);
                p.Product_stockQuantity = Convert.ToInt32(frmThem.soluongton);
                p.Is_deleted = false;
                if (ProductBUS.Instance.InsertProduct(p)!=-1) 
                {
                    MessageBox.Show("Thêm sản phẩm mới thành công!");
                    ManagerUC_SanPham_Load(sender, e); 
                }
                else
                {
                    MessageBox.Show("Lỗi khi lưu vào cơ sở dữ liệu!");
                }
            }
        }
    }
}
/*
DataGridViewRow row1 = dataGridView1.Rows[e.RowIndex];
string tenSP = row1.Cells[2].Value.ToString();
slc.tenSP = tenSP;
if

int giaban = Convert.ToInt32(row1.Cells[4].Value);
string soluongChon = Convert.ToInt32(row1.Cells[6].Value);
string thanhtien = (giaban * soluongChon).ToString();
ChonThanhToan.Rows.Add(row1.Cells[1], row1.Cells[2], row1.Cells[3], row1.Cells[4], row1.Cells[5], row1.Cells[6], thanhtien);
*/
