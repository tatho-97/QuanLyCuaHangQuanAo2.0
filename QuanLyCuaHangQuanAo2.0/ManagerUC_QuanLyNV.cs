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
using QuanLyCuaHangQuanAo2._0.BUS;
namespace QuanLyCuaHangQuanAo2._0
{
    public partial class ManagerUC_QuanLyNV : UserControl
    {
        public ManagerUC_QuanLyNV()
        {
            InitializeComponent();
        }
        List<Employee> list;
        private void LoadDataToGrid()
        {
            dataGridView1.Rows.Clear();

            if (list == null)
            {
                list = EmployeeBUS.Instance.GetAllEmployee();
            }

            foreach (Employee emp in list)
            {
                string role = "";
                string trangthai = "";

                if (emp.Role_id == 1)
                {
                    role = "Quản lý";
                }
                if (emp.Role_id == 2)
                {
                    role = "Nhân viên";
                }

                if (emp.Is_deleted)
                {
                    trangthai = "Khóa";
                }
                else
                {
                    trangthai = "Mở";
                }
                dataGridView1.Rows.Add(
                  emp.Employee_id,
                  role,
                  emp.Full_name,
                  emp.Phone_number,
                  emp.Username,
                  emp.Password_hash,
                  trangthai
                );
            }
        }
        private void ManagerUC_QuanLyNV_Load(object sender, EventArgs e)
        {
            LoadDataToGrid();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormThemNhanvien f = new FormThemNhanvien();
            f.list = list;
            f.ShowDialog();
            if (f.Tag.ToString() == "1")
            {
                f.Close();
                Employee emp = f.emp;
                int id = EmployeeBUS.Instance.CreateEmployee(emp);
                emp.Employee_id = id;
                if (id != -1)
                {
                    MessageBox.Show("Tạo tài khoản thành công");
                    list.Add(emp);
                }
                list = null;
                LoadDataToGrid();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridView1.CurrentRow;
            FormSuaNhanVien f = new FormSuaNhanVien();
            f.emp.Employee_id = Convert.ToInt32(row.Cells[0].Value);
            if (row.Cells[1].Value.ToString() == "Nhân viên")
            {
                f.emp.Role_id = 2;
            }
            if (row.Cells[1].Value.ToString() == "Quản lý")
            {
                f.emp.Role_id = 1;
            }

            if (row.Cells[6].Value.ToString() == "Khóa")
            {
                f.emp.Is_deleted = true;
            }
            if (row.Cells[6].Value.ToString() == "Mở")
            {
                f.emp.Is_deleted = false;
            }
            f.emp.Full_name = row.Cells[2].Value.ToString();
            f.emp.Phone_number = row.Cells[3].Value.ToString();
            f.emp.Username = row.Cells[4].Value.ToString();
            f.emp.Password_hash = row.Cells[5].Value.ToString();
            f.list = list;
            f.ShowDialog();
            if (f.Tag.ToString() == "1")
            {
                if (EmployeeBUS.Instance.UpdateEmployee(f.emp))
                {
                    MessageBox.Show("Sửa thành công!");
                    ManagerUC_QuanLyNV_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("Lỗi ! sửa thất bại");
                }
                f.Close();
            }
            list = null;
            LoadDataToGrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridView1.CurrentRow;
            FormSuaNhanVien f = new FormSuaNhanVien();
            f.emp.Employee_id = Convert.ToInt32(row.Cells[0].Value);
            f.emp.Full_name = row.Cells[2].Value.ToString();
            f.emp.Phone_number = row.Cells[3].Value.ToString();
            f.emp.Username = row.Cells[4].Value.ToString();
            f.emp.Password_hash = row.Cells[5].Value.ToString();
            if (row.Cells[1].Value.ToString() == "Nhân viên")
            {
                f.emp.Role_id = 2;
            }
            if (row.Cells[1].Value.ToString() == "Quản lý")
            {
                f.emp.Role_id = 1;
            }
            if (row.Cells[6].Value.ToString() == "Mở")
            {
                f.emp.Is_deleted = true;
                if (EmployeeBUS.Instance.UpdateEmployee(f.emp))
                {
                    MessageBox.Show("Khóa thành công!");
                    ManagerUC_QuanLyNV_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("Lỗi ! sửa thất bại");
                }
                list = null;
                LoadDataToGrid();
            }
        }
    }
}
