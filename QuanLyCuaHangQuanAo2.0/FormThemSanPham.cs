using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyCuaHangQuanAo2._0.DTO;
using QuanLyCuaHangQuanAo2._0.DAO;
namespace QuanLyCuaHangQuanAo2._0
{
    public partial class FormThemSanPham : Form
    {
        public FormThemSanPham()
        {
            InitializeComponent();
        }
        public string TenSP;
        public string Loai;
        public string size;
        public string GiaBan;
        public string giaNhap;
        public string soluongton;
        public List<Product> list;
        private void FormThemSanPham_Load(object sender, EventArgs e)
        {
            list = ProductDAO.Instance.GetAllProducts();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox4.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên sản phẩm và giá bán!");
                    return;
                }
                int giaB = int.Parse(textBox4.Text);
                int giaN = string.IsNullOrWhiteSpace(textBox5.Text) ? 0 : int.Parse(textBox5.Text);
                int soLuong = string.IsNullOrWhiteSpace(textBox6.Text) ? 0 : int.Parse(textBox6.Text);

                if (giaN >= giaB && giaN > 0)
                {
                    MessageBox.Show("Cảnh báo: Giá nhập đang lớn hơn hoặc bằng giá bán!");

                }
                TenSP = textBox1.Text.Trim();
                Loai = textBox2.Text.Trim();
                size = textBox3.Text.Trim(); 
                GiaBan = giaB.ToString();
                giaNhap = giaN.ToString();
                soluongton = soLuong.ToString();
                if (list.Count > 0)
                {
                    foreach (Product p in list)
                    {
                        if (p.Product_name == TenSP && p.Category_name==Loai &&p.Product_size==size)
                        {
                            MessageBox.Show("Không được nhập trùng sản phẩm ! ");
                            return;
                        }
                    }
                }
                this.Tag = "1";
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (FormatException)
            {
                MessageBox.Show("Giá tiền và số lượng phải nhập bằng số!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Tag = "0";
            this.Close();
        }
    }
}
