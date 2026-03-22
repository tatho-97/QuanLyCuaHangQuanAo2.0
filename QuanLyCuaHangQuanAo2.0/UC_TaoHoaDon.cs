using System;
using System.Collections.Generic;
using System.Windows.Forms;
using QuanLyCuaHangQuanAo2._0.BUS;
using QuanLyCuaHangQuanAo2._0.DTO;
using QuanLyCuaHangQuanAo2._0.DAO;
namespace QuanLyCuaHangQuanAo2._0
{
    public partial class UC_TaoHoaDon : UserControl
    {
        public UC_TaoHoaDon()
        {
            InitializeComponent();
        }
        Form currentForm;
        private void UC_TaoHoaDon_Load(object sender, EventArgs e)
        {
            currentForm = this.FindForm();
            if (currentForm is ManagerForm)
            {
                ManagerForm f = (ManagerForm)currentForm;
                int tongtien = 0;
                int tongso = 0;
                foreach (Product_selected pd in f.product_Selecteds)
                {
                    if (pd.SoLuongChon > 0 && pd.SoLuongTon > 0)
                    {
                        dataGridView1.Rows.Add(pd.MaSP, pd.TenSP, pd.Loai, pd.Size, pd.GiaBan, pd.SoLuongTon, pd.SoLuongChon, pd.ThanhTien);
                        tongtien = tongtien += Convert.ToInt32(pd.ThanhTien);
                        tongso = tongso + Convert.ToInt32(pd.SoLuongChon);
                    }
                }
                textBox3.Text = tongtien.ToString();
                textBox1.Text = tongso.ToString();
            }
            else if (currentForm is StaffForm)
            {
                StaffForm f = (StaffForm)currentForm;
                int tongtien = 0;
                int tongso = 0;
                foreach (Product_selected pd in f.product_Selecteds)
                {
                    dataGridView1.Rows.Add(pd.MaSP, pd.TenSP, pd.Loai, pd.Size, pd.GiaBan, pd.SoLuongTon, pd.SoLuongChon, pd.ThanhTien);
                    tongso = tongso + Convert.ToInt32(pd.SoLuongChon);
                    tongtien = tongtien += Convert.ToInt32(pd.ThanhTien);
                }
                textBox3.Text = tongtien.ToString();
                textBox1.Text = tongso.ToString();
            }
        }
        bool Textcheck()
        {
            bool check = true;
            if (string.IsNullOrWhiteSpace(textBox4.Text) || string.IsNullOrWhiteSpace(textBox5.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên và số điện thoại!");
                check = false;
            }

            if (textBox5.Text.Length < 10 || textBox5.Text.Length > 11 || !long.TryParse(textBox5.Text, out _))
            {
                MessageBox.Show("Số điện thoại phải là chữ số và có độ dài từ 10-11 ký tự!");
                check = false;
            }
            bool hasDigit = false;
            foreach (char c in textBox4.Text)
            {
                if (char.IsDigit(c))
                {
                    hasDigit = true;
                    break;
                }
            }

            if (hasDigit)
            {
                MessageBox.Show("Tên khách hàng không được chứa chữ số!");
                check = false;
            }

            if (string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("Tên khách hàng không được để trống!");
                check = false;
            }
            return check;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (!Textcheck()) return;
            List<Customer> danhSachKH = CustomerBUS.Instance.SearchCustomer(textBox4.Text, textBox5.Text);
            Customer currentCustomer = null;

            if (danhSachKH.Count > 0)
            {
                MessageBox.Show("Khách hàng cũ!");
                currentCustomer = danhSachKH[0];
            }
            else
            {
                MessageBox.Show("Khách hàng mới");
                if (CustomerBUS.Instance.InsertCustomer(textBox4.Text, textBox5.Text))
                {
                    MessageBox.Show("Thêm khách hàng thành công!");
                    danhSachKH = CustomerBUS.Instance.SearchCustomer(textBox4.Text, textBox5.Text);
                    currentCustomer = danhSachKH[0];
                }
                else
                {
                    MessageBox.Show("Lỗi cơ sở dữ liệu, không thể thêm khách hàng!");
                    return;
                }
            }
            Form currentForm = this.FindForm();
            if (currentForm is ManagerForm)
            {
                ManagerForm f = (ManagerForm)currentForm;
                if (f.product_Selecteds.Count > 0 && currentCustomer != null)
                {
                    Order invoice = new Order();
                    invoice.KhachHang = currentCustomer;
                    invoice.Customer_id = currentCustomer.Customer_id;
                    List<Employee> em = EmployeeBUS.Instance.SearchEmployee("ID", f.id.ToString());
                    invoice.NhanVien = em[0];
                    invoice.Order_date = DateTime.Now;
                    invoice.Total_quantity = Convert.ToInt32(textBox1.Text);
                    invoice.Total_amount = Convert.ToInt64(textBox3.Text);
                    invoice.Order_id = OrderBUS.Instance.CreateOrder(invoice);
                    if (invoice.Order_id == -1)
                    {
                        MessageBox.Show("Lỗi tạo hóa đơn!");
                        return;
                    }

                    List<OrderDetail> list_odt = new List<OrderDetail>();

                    // 3. Xử lý từng sản phẩm trong hóa đơn
                    foreach (Product_selected pd in f.product_Selecteds)
                    {
                        int updatedStock = pd.SoLuongTon - pd.SoLuongChon;
                        
                        Product p = new Product();
                        p.Product_id = Convert.ToInt32(pd.MaSP);
                        p.Product_name = pd.TenSP;
                        p.Product_size = pd.Size;
                        p.Product_importPrice = pd.GiaNhap;
                        p.Category_name = pd.Loai;
                        p.Product_sellingPrice = pd.GiaBan;
                        p.Product_stockQuantity = updatedStock; // Gán số lượng mới sau khi trừ
                        p.Is_deleted = false;

                        // Thiết lập chi tiết hóa đơn
                        OrderDetail odt = new OrderDetail();
                        odt.Order_id = invoice.Order_id;
                        odt.Product_id = p.Product_id;
                        odt.Quantity = pd.SoLuongChon;
                        odt.Unit_price = pd.GiaBan;
                        list_odt.Add(odt);

                        // Cập nhật lại số lượng trong Database
                        ProductDAO.Instance.UpdateProduct(p);
                    }

                    OrderBUS.Instance.ProcessFullPayment(invoice, list_odt);
                    MessageBox.Show("Thanh toán thành công!");
                    
                    button2_Click(sender, e);
                }
            }
            else if (currentForm is StaffForm)
            {
                StaffForm f = (StaffForm)currentForm;
                if (f.product_Selecteds.Count > 0 && currentCustomer != null)
                {
                    if (f.product_Selecteds.Count > 0 && currentCustomer != null)
                    {
                        Order invoice = new Order();
                        invoice.KhachHang = currentCustomer;
                        invoice.Customer_id = currentCustomer.Customer_id;
                        List<Employee> em = EmployeeBUS.Instance.SearchEmployee("ID", f.id.ToString());
                        invoice.NhanVien = em[0];
                        invoice.Order_date = DateTime.Now;
                        invoice.Total_quantity = Convert.ToInt32(textBox1.Text);
                        invoice.Total_amount = Convert.ToInt64(textBox3.Text);
                        invoice.Order_id = OrderBUS.Instance.CreateOrder(invoice);
                        if (invoice.Order_id == -1)
                        {
                            MessageBox.Show("Lỗi tạo hóa đơn!");
                            return;
                        }

                        List<OrderDetail> list_odt = new List<OrderDetail>();

                        // 3. Xử lý từng sản phẩm trong hóa đơn
                        foreach (Product_selected pd in f.product_Selecteds)
                        {
                            int updatedStock = pd.SoLuongTon - pd.SoLuongChon;

                            Product p = new Product();
                            p.Product_id = Convert.ToInt32(pd.MaSP);
                            p.Product_name = pd.TenSP;
                            p.Product_size = pd.Size;
                            p.Category_name = pd.Loai;
                            p.Product_importPrice = pd.GiaNhap;
                            p.Product_sellingPrice = pd.GiaBan;
                            p.Product_stockQuantity = updatedStock; // Gán số lượng mới sau khi trừ
                            p.Is_deleted = false;

                            // Thiết lập chi tiết hóa đơn
                            OrderDetail odt = new OrderDetail();
                            odt.Order_id = invoice.Order_id;
                            odt.Product_id = p.Product_id;
                            odt.Quantity = pd.SoLuongChon;
                            odt.Unit_price = pd.GiaBan;
                            list_odt.Add(odt);

                            // Cập nhật lại số lượng trong Database
                            ProductDAO.Instance.UpdateProduct(p);
                        }

                        OrderBUS.Instance.ProcessFullPayment(invoice, list_odt);
                        MessageBox.Show("Thanh toán thành công!");
                        button2_Click(sender, e);
                    }
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            currentForm = this.FindForm();
            if (currentForm is ManagerForm)
            {
                ManagerForm f = (ManagerForm)currentForm;
                f.ucSanPham = new ManagerUC_SanPham();
                f.product_Selecteds.Clear();
                MessageBox.Show("Sản phẩm chọn đã được xóa!");
            }
            else if (currentForm is StaffForm)
            {
                StaffForm f = (StaffForm)currentForm;
                f.ucSanPham = new StaffUC_SanPham();
                f.product_Selecteds.Clear();
                MessageBox.Show("Sản phẩm chọn đã được xóa!");
            }
            
            dataGridView1.Rows.Clear();
            textBox1.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";

        }
    }
}
