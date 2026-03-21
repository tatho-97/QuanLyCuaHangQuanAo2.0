using System;
using System.Collections.Generic;
using System.Windows.Forms;
using QuanLyCuaHangQuanAo2._0.BUS;
using QuanLyCuaHangQuanAo2._0.DTO;

namespace QuanLyCuaHangQuanAo2._0
{
    public partial class LoginForm : Form
    {
        List<Employee> list;
        public LoginForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            list = EmployeeBUS.Instance.GetAllEmployee();
            if (list.Count<=0)
            {
                MessageBox.Show("Lỗi không kết nối được cơ sở dữ liệu");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                textBox2.UseSystemPasswordChar = false;
            else
                textBox2.UseSystemPasswordChar = true;

        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == ""|| textBox2.Text == "")
            {
                MessageBox.Show("Tên đăng nhập và mật khẩu không được để trống!");
                if (textBox2.Text == "")
                    textBox2.Focus();
                if (textBox1.Text == "")
                    textBox1.Focus();
            }
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                Employee Emp = null;
                foreach (Employee emp in list)
                {
                    if (emp.Username == textBox1.Text && emp.Password_hash == textBox2.Text&& emp.Is_deleted==false)
                    {
                        Emp = emp;
                        break;
                    }
                }
                if (Emp != null)
                {
                    if (Emp.Role_id == 1)
                    {
                        MessageBox.Show("Đăng nhập thành công. Xin chào quản lí " + Emp.Full_name);
                        ManagerForm quanly = new ManagerForm();
                        quanly.id = Emp.Employee_id;
                        quanly.ten = Emp.Full_name;
                        quanly.emp = Emp;
                        this.Hide();
                        quanly.ShowDialog();
                        this.Close();
                    }
                    if (Emp.Role_id == 2)
                    {
                        MessageBox.Show("Đăng nhập thành công. Xin chào nhân viên " + Emp.Full_name);
                        StaffForm nhanvien = new StaffForm();
                        nhanvien.id = Emp.Employee_id;
                        nhanvien.ten = Emp.Full_name;
                        this.Hide();
                        nhanvien.ShowDialog();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Sai tài khoản hoặc mật khẩu!");
                }
            }
        }
    }
}
