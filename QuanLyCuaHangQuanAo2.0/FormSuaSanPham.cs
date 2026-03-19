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
    public partial class FormSuaSanPham : Form
    {

        public FormSuaSanPham()
        {
            InitializeComponent();
        }
        public string TenSP;
        public string Loai;
        public string size;
        public string GiaBan;
        public string giaNhap; 
        public string soluongton;

        private void textBox4_Leave(object sender, EventArgs e)
        {
            int a;
            try
            {
                a = Convert.ToInt32(textBox4.Text);
                if (a < 1000)
                {
                    MessageBox.Show("Giá bán >=1000!");
                    textBox4.Text = GiaBan;
                    textBox4.Focus();
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Nhập sai định dạng");
                textBox4.Focus();
                return;
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_Leave(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            int a;
            try
            {
                a = Convert.ToInt32(textBox5.Text);
                if (a < 1000)
                {
                    MessageBox.Show("Giá bán >=1000!");
                    textBox5.Text = giaNhap;
                    textBox5.Focus();
                    return;
                }
                else
                {
                    giaNhap = textBox5.Text;
                }
            }
            catch
            {
                MessageBox.Show("Nhập sai định dạng");
                textBox5.Focus();
                return;
            }
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            int a;
            try
            {
                a = Convert.ToInt32(textBox6.Text);
                if (a < 0)
                {
                    MessageBox.Show("Số lượng tồn >0!");
                    textBox6.Text = soluongton;
                    textBox6.Focus();
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Nhập sai định dạng");
                textBox6.Focus();
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(Convert.ToInt32(textBox5.Text)>= Convert.ToInt32(textBox4.Text))
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
                MessageBox.Show("Sửa thành công!");
                this.Hide();
            }
        }

        private void FormSuaSanPham_Load(object sender, EventArgs e)
        {
            this.Tag = "0";
            textBox1.Text = TenSP;
            textBox2.Text = Loai;
            textBox3.Text = size;
            textBox4.Text = GiaBan;
            textBox5.Text = giaNhap;
            textBox6.Text = soluongton;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Tag = "0";
            this.Hide();
        }
    }
}
