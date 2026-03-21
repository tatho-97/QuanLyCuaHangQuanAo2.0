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
        public int CreateEmployee(Employee obj)
        {
            if (obj.Username=="" || obj.Full_name=="")
            {
                return -1;
            }
            return EmployeeDAO.Instance.InsertEmployee(obj);
        }
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
        public bool UpdateEmployee(Employee e)
        {
            if (e.Employee_id <= 0) return false;
            if (string.IsNullOrEmpty(e.Full_name)) return false;

            return EmployeeDAO.Instance.UpdateEmployee(e);
        }
    }
}