using System.Collections.Generic;
using QuanLyCuaHangQuanAo2._0.DTO;
using QuanLyCuaHangQuanAo2._0.DAO;

namespace QuanLyCuaHangQuanAo2._0.BUS
{
    public class EmployeeBUS
    {
        private static EmployeeBUS instance;

        public static EmployeeBUS Instance
        {
            get { if (instance == null) instance = new EmployeeBUS(); return instance; }
        }

        private EmployeeBUS() { }

        public List<Employee> GetAllEmployee()
        {
            return EmployeeDAO.Instance.GetAllEmployee();
        }

        public List<Employee> SearchEmployee(string searchType, string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return GetAllEmployee();
            }
            return EmployeeDAO.Instance.SearchEmployee(searchType, keyword);
        }
    }
}