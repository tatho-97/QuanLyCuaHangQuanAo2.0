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
    public partial class FormSuaNhanVien : Form
    {
        public FormSuaNhanVien()
        {
            InitializeComponent();
        }
        public Employee emp = new Employee();
        public List<Employee> list;
        private void FormSuaNhanVien_Load(object sender, EventArgs e)
        {
            textBox5.Text = emp.Employee_id.ToString();
            textBox1.Text = emp.Full_name;
            textBox2.Text = emp.Phone_number;
            textBox3.Text = emp.Username;
            textBox4.Text = emp.Password_hash;
            if (emp.Role_id == 1)
            {
                comboBox1.SelectedIndex = 0;
            }
            if (emp.Role_id == 2)
            {
                comboBox1.SelectedIndex = 1;
            }
            if (emp.Is_deleted)
            {
                comboBox2.SelectedIndex=1;
            }
            else
            {
                comboBox2.SelectedIndex = 0;
            }
            this.Tag = 0;
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
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
                string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin vào tất cả các trường!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Focus();
                return;
            }
            if (textBox2.Text.Length < 10 || textBox2.Text.Length > 11 || !long.TryParse(textBox2.Text, out _))
            {
                MessageBox.Show("Số điện thoại phải là chữ số và có độ dài từ 10-11 ký tự!");
                textBox2.Focus();
                return;
            }
            if (CoChuaSo(textBox1.Text))
            {
                MessageBox.Show("Tên không được chứa chữ số");
                textBox1.Focus();
                return;
            }
            foreach (Employee nhanvien in list)
            {
                if (nhanvien.Username == textBox3.Text && nhanvien.Username!=emp.Username)
                {
                    MessageBox.Show(("Đã có tài khoản sử dụng tên đăng nhập này xin vui lòng Nhập lại"));
                    textBox3.Clear();
                    textBox3.Focus();
                    return;

                }
            }
            emp.Full_name = textBox1.Text;
            emp.Phone_number = textBox2.Text;
            emp.Username = textBox3.Text;
            emp.Password_hash = textBox4.Text;
            if (comboBox1.SelectedItem.ToString() == "Quản lý")
                emp.Role_id = 1;
            if (comboBox1.SelectedItem.ToString() == "Nhân viên")
                emp.Role_id = 2;
            if (comboBox2.SelectedIndex == 0)
            {
                emp.Is_deleted = false; 
            }
            else if (comboBox2.SelectedIndex == 1)
            {
                emp.Is_deleted = true;
            }
            this.Tag = 1;
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Tag = 0;
            this.Hide();
        }
    }
}
