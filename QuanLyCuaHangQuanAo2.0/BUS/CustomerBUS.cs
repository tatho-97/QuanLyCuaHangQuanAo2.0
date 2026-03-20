using System.Collections.Generic;
using QuanLyCuaHangQuanAo2._0.DTO;
using QuanLyCuaHangQuanAo2._0.DAO;

namespace QuanLyCuaHangQuanAo2._0.BUS
{
    public class CustomerBUS 
    {
        private static CustomerBUS instance;

        public static CustomerBUS Instance
        {
            get { if (instance == null) instance = new CustomerBUS(); return instance; }
        }

        private CustomerBUS() { }

        public List<Customer> SearchCustomer (string ten,string sdt)
        {
            return CustomerDAO.Instance.SearchCustomer(ten,sdt);
        }

        public List<Customer> GetAllCustomers()
        {
            return CustomerDAO.Instance.GetAllCustomers();
        }
        public bool InsertCustomer(string ten,string sdt)
        {
            return CustomerDAO.Instance.InsertCustomer(ten,sdt);
        }
    }
}