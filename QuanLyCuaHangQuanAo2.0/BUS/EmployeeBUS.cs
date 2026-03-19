using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTL_QuanLyKhoHang_Nhom20;
using BTL_QuanLyKhoHang_Nhom20.DTO;
using BaiTapLon.DAO;
namespace BaiTapLon.BUS
{
    internal class EmployeeBUS
    {
        private static EmployeeBUS instance = new EmployeeBUS();
        public static EmployeeBUS Instance { get {return instance; } }
        public List<Employee> GetAllEmployee() { return EmployeeDAO.Instance.GetAllEmployee(); }
    }
}
