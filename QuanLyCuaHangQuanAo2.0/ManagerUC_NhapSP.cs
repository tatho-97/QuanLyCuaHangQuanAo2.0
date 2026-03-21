using System;
using System.Collections.Generic;
using System.Windows.Forms;
using QuanLyCuaHangQuanAo2._0.BUS;
using QuanLyCuaHangQuanAo2._0.DTO;
using QuanLyCuaHangQuanAo2._0.DAO;
namespace QuanLyCuaHangQuanAo2._0
{

    public partial class ManagerUC_NhapSP : UserControl
    {
        public ManagerUC_NhapSP()
        {
            InitializeComponent();
        }

        Dictionary<string, Product> product_co_san = new Dictionary<string, Product>();
        Dictionary<string, Product> product_moi = new Dictionary<string, Product>();
        private void ManagerUC_NhapSP_Load(object sender, EventArgs e)
        {
        }
        public int id = 0;
        private void NhapSanPhamMoi(object sender, EventArgs e)
        {
            FormNhapSanPhamMoi n = new FormNhapSanPhamMoi();
            n.ShowDialog();
            if (n.Tag?.ToString() == "1" && n.p != null)
            {
                bool check = false;
                foreach (Product p in product_moi.Values)
                {
                    if (p.Product_name == n.p.Product_name &&
                        p.Category_name == n.p.Category_name &&
                        p.Product_size == n.p.Product_size)
                    {
                        p.Product_stockQuantity += n.p.Product_stockQuantity;
                        p.Product_importPrice = n.p.Product_importPrice;
                        p.Product_sellingPrice = n.p.Product_sellingPrice;
                        check = true;
                        break;
                    }
                }
                if (!check)
                {
                    id++;
                    n.p.Product_id = id;
                    product_moi[id.ToString()] = n.p;
                }
            }
            ShowSP();
        }
        private void ShowSP()
        {
            dataGridView1.Rows.Clear();
            int tongtien = 0;
            foreach (Product pd in product_co_san.Values)
            {
                dataGridView1.Rows.Add("Sẵn", pd.Product_id, pd.Product_name, pd.Category_name, pd.Product_size,
                                    pd.Product_sellingPrice, pd.Product_importPrice, pd.Product_stockQuantity);
                tongtien += pd.Product_importPrice * pd.Product_stockQuantity;
            }
            foreach (Product pd in product_moi.Values)
            {
                dataGridView1.Rows.Add("Mới",pd.Product_id, pd.Product_name, pd.Category_name, pd.Product_size, 
                                    pd.Product_sellingPrice, pd.Product_importPrice, pd.Product_stockQuantity);
                tongtien += pd.Product_importPrice * pd.Product_stockQuantity;
            }
            textBox1.Text = tongtien.ToString();
        }
        private void NhapSanPhamSan(object sender, EventArgs e)
        {
            FormSanPhamCoSan f = new FormSanPhamCoSan();
            f.ShowDialog();
            if(f.list.Count>0)
            {
                foreach (Product pd in f.list)
                {
                    int key = pd.Product_id;
                    if (!product_co_san.ContainsKey(key.ToString()))
                    {
                        product_co_san[key.ToString()] = pd;
                    }
                    else
                    {
                        // su dung stock quantity- so luong ton de luu so luong nhap
                        product_co_san[key.ToString()].Product_stockQuantity = pd.Product_stockQuantity + product_co_san[key.ToString()].Product_stockQuantity;
                        // trung trong dict thi cap nhat value product soluongchon(stockquantity) cong them 
                    }
                }
            }
            ShowSP();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if(dataGridView1.Rows.Count<=0)
            {
                return;
            }
            DataGridViewRow row = dataGridView1.CurrentRow;
            string key = row.Cells[1].Value.ToString();
            if (row.Cells[0].Value.ToString() == "Mới")
            {
                int tien = product_moi[key].Product_importPrice * product_moi[key].Product_stockQuantity;
                textBox1.Text = (Convert.ToInt32(textBox1.Text) - tien).ToString();
                product_moi.Remove(key);
            }
            if (row.Cells[0].Value.ToString() == "Sẵn")
            {
                int tien = product_moi[key].Product_importPrice * product_moi[key].Product_stockQuantity;
                textBox1.Text = (Convert.ToInt32(textBox1.Text) - tien).ToString();
                product_co_san.Remove(key);
            }
            dataGridView1.Rows.Remove(row);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows.Clear();
                product_co_san.Clear();
                product_moi.Clear();
                textBox1.Text = "0"; // Nên reset lại cả textbox tổng tiền
                MessageBox.Show("Hủy nhập thành công!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (product_co_san.Count > 0 || product_moi.Count > 0)
            {
                ManagerForm a = (ManagerForm)this.FindForm();

                // tao hoa don nhap hang
                Import_orders imp = new Import_orders();
                imp.Employee_id = a.id;
                imp.Import_date = DateTime.Now;
                imp.NhanVien = a.emp;
                imp.Total_amount = Convert.ToInt32((textBox1.Text));
                    MessageBox.Show("Tạo hóa đơn nhập hàng thành công!");
                    bool check = true;
                    List<Product> list = ProductBUS.Instance.GetAllProducts();
                    List<ImportDetail> listIMP = new List<ImportDetail>();

                    foreach (Product p in list)
                    {
                        if (product_co_san.ContainsKey(p.Product_id.ToString()))
                        {
                            p.Product_stockQuantity = p.Product_stockQuantity + product_co_san[p.Product_id.ToString()].Product_stockQuantity;
                            check = ProductBUS.Instance.UpdateProduct(p);
                            if (!check)
                            {
                                MessageBox.Show("Xảy ra lỗi không thể thêm vào cơ sở dữ liệu!");
                                break;
                            }
                            if (check)
                            {
                                ImportDetail imp_detail = new ImportDetail();
                                imp_detail.Import_id = imp.Import_id;
                                imp_detail.Import_price = p.Product_importPrice;
                                imp_detail.Product_id = p.Product_id;
                                imp_detail.Quantity = product_co_san[p.Product_id.ToString()].Product_stockQuantity;
                                listIMP.Add(imp_detail);
                            }
                        }
                    }
                    if (check)
                    {
                        foreach (Product p in product_moi.Values)
                        {
                            int id = ProductBUS.Instance.InsertProduct(p);
                            if (id==-1)
                            {
                                MessageBox.Show("Xảy ra lỗi không thể thêm vào cơ sở dữ liệu !");
                                break;
                            }
                            else
                            {
                                ImportDetail imp_detail = new ImportDetail();
                                imp_detail.Import_id = imp.Import_id;
                                imp_detail.Import_price = p.Product_importPrice;
                                imp_detail.Product_id = id;
                                imp_detail.Quantity = p.Product_stockQuantity;
                                listIMP.Add(imp_detail);
                            }
                        }
                        if (!check)
                        {
                            MessageBox.Show("Lỗi bất định!");
                        }
                        else
                        {
                            ImportBUS.Instance.ProcessFullImport(imp, listIMP);
                            button5_Click(sender, e);
                            a.ucSanPham = new ManagerUC_SanPham();
                        }
                    }
            }
            else
            {
                MessageBox.Show("Chưa có sản phẩm nào!");
            }
        }
    }
}
