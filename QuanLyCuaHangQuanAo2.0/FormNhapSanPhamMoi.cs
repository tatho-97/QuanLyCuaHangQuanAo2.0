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
namespace QuanLyCuaHangQuanAo2._0
{
    public partial class FormNhapSanPhamMoi : Form
    {
        public FormNhapSanPhamMoi()
        {
            InitializeComponent();
        }
        public Product p = new Product();
        private bool CoChuaSo(string text)
        {
            foreach (char c in text)
            {
                if (char.IsDigit(c)) // Nếu ký tự c là một con số (0-9)
                {
                    return true; // Có chứa số
                }
            }
            return false; // Không chứa số nào cả
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text) ||
                string.IsNullOrWhiteSpace(textBox5.Text) ||
                string.IsNullOrWhiteSpace(textBox6.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin, không được để trống!");
                return;
            }
            if (CoChuaSo(textBox1.Text))
            {
                MessageBox.Show("Tên sản phẩm không được chứa chữ số!");
                textBox1.Focus();
                return;
            }
            if (CoChuaSo(textBox2.Text))
            {
                MessageBox.Show("Loại sản phẩm không được chứa chữ số!");
                textBox2.Focus();
                return;
            }
            try
            {
                int gBan = int.Parse(textBox4.Text);
                int gNhap = int.Parse(textBox5.Text);
                int sLuong = int.Parse(textBox6.Text);
                if (gNhap >= gBan)
                {
                    MessageBox.Show("Giá nhập phải nhỏ hơn giá bán!");
                    return;
                }
                if(gNhap<1000)
                {
                    MessageBox.Show("Giá nhập phải >=1000");
                    return;
                }
                if (gBan < 1000)
                {
                    MessageBox.Show("Giá Bán phải >=1000");
                    return;
                }
                if (sLuong <= 0)
                {
                    MessageBox.Show("Số lượng nhập phải >=1");
                    return;
                }
                p.Product_name = textBox1.Text.Trim();
                p.Category_name = textBox2.Text.Trim();
                p.Product_size = textBox3.Text.Trim();
                p.Product_sellingPrice = gBan;
                p.Product_importPrice = gNhap;
                p.Product_stockQuantity = sLuong;
                p.Is_deleted = false;
                p.Is_selected = false;
                this.Tag = "1";
                MessageBox.Show("Nhập thành công!");
                this.Hide();
            }
            catch (FormatException)
            {
                MessageBox.Show("Giá bán, giá nhập và số lượng phải là con số!");
            }
        }
        private void FormNhapSanPhamMoi_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
            this.Tag = "0";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Tag = "0";
            this.Hide();
        }
    }
}
