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
    public partial class FormNhapSoLuongChon : Form
    {

        public FormNhapSoLuongChon()
        {
            InitializeComponent();
        }
        public int soluongton;
        public string tenSP;
        public string soluongchon = "";

        private void button1_Click(object sender, EventArgs e)
        {
            int a
;           try
            {
               a = Convert.ToInt32(textBox1.Text);
            }
            catch
            {
                MessageBox.Show("Nhập sai định dạng");
                textBox1.Clear();
                textBox1.Focus();
                return;
            }
            if(a>soluongton)
            {
                MessageBox.Show("Nhập quá số lượng");
                textBox1.Clear();
                textBox1.Focus();
                return;
            }
            if(a<=0)
            {
                MessageBox.Show("Chọn >0");
                textBox1.Clear();
                textBox1.Focus();
                return;
            }
            else
            {
                soluongchon = a.ToString();
                this.Hide();
            }
        }

        private void NhapSoLuongChon_Load(object sender, EventArgs e)
        {
            label1.Text = label1.Text + tenSP;
            label2.Text = label2.Text + soluongton.ToString();
        }

        private void Thoat(object sender, EventArgs e)
        {
            soluongchon = "";
            this.Hide();
        }
    }
}
