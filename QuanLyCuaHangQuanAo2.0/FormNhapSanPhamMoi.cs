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
    public partial class FormNhapSanPhamMoi : Form
    {
        public FormNhapSanPhamMoi()
        {
            InitializeComponent();
        }
        public string TenSP;
        public string Loai;
        public string size;
        public string GiaBan;
        public string giaNhap;
        public string soluongton;
        private void button2_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBox5.Text) >= Convert.ToInt32(textBox4.Text))
            {
                MessageBox.Show("Giá nhập phải < giá bán");
                textBox4.Text = GiaBan;
                textBox5.Text = giaNhap;
                return;
            }
            else
            {
                TenSP = textBox1.Text;
                Loai = textBox2.Text;
                size = textBox3.Text;
                GiaBan = textBox4.Text;
                giaNhap = textBox5.Text;
                soluongton = textBox6.Text;
                this.Tag = "1";
                MessageBox.Show("Nhập thành công!");
                this.Hide();
            }
        }
    }
}
