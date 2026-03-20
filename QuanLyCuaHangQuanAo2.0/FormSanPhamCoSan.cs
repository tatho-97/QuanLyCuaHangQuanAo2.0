using System;
using System.Collections.Generic;
using System.Windows.Forms;
using QuanLyCuaHangQuanAo2._0.BUS;
using QuanLyCuaHangQuanAo2._0.DTO;
namespace QuanLyCuaHangQuanAo2._0
{
    public partial class FormSanPhamCoSan : Form
    {
        public FormSanPhamCoSan()
        {
            InitializeComponent();
        }
        public List<Product_selected> list =new List<Product_selected>();
        private void FormSanPhamCoSan_Load(object sender, EventArgs e)
        {
            
            dataGridView1.Rows.Clear();
            List<Product> data = ProductBUS.Instance.GetAllProducts();
            foreach (Product p in data)
            {
                if (!p.Is_deleted)
                    dataGridView1.Rows.Add( p.Product_id, p.Product_name, p.Category_name, p.Product_size, p.Product_sellingPrice, p.Product_importPrice, p.Product_stockQuantity, "0");
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            var cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
            try
            {
               int val = Convert.ToInt32(cell.Value);
                if (val > 0 )
                {
                    MessageBox.Show("Hợp lệ: Đây là số nguyên dương.");
                }
                else
                {
                    MessageBox.Show("Số lượng ko hợp lệ!");
                }
            }
            catch (Exception )
            {
                MessageBox.Show("Lỗi: Dữ liệu nhập vào không phải là số.");
            }
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dataGridView1.IsCurrentCellDirty)
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (Convert.ToInt32(row.Cells[6].Value)>0)
                {
                    Product_selected pd = new Product_selected();
                    pd.MaSP = row.Cells[0].Value.ToString();
                    pd.TenSP = row.Cells[1].Value.ToString();
                    pd.Loai = row.Cells[2].Value.ToString();
                    pd.Size = row.Cells[3].Value.ToString();
                    pd.GiaBan = Convert.ToInt32(row.Cells[4].Value);
                    pd.GiaNhap = Convert.ToInt32(row.Cells[5].Value);
                    pd.SoLuongChon = Convert.ToInt32(row.Cells[6].Value);
                    list.Add(pd);
                }
                this.Hide();
            }
            if(list.Count>0)
            {
                MessageBox.Show("Thêm thành công");
                this.Hide();
            }
            else
            {
                MessageBox.Show("Bạn chưa thêm sản phẩm nào");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            list.Clear();
            this.Hide();
        }
    }
}
