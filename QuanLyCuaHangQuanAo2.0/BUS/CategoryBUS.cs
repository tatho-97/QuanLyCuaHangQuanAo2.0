using BaiTapLon.DAO;
using BTL_QuanLyKhoHang_Nhom20.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiTapLon.BUS
{
    internal class CategoryBUS
    {
        private static CategoryBUS instance = new CategoryBUS();
        public static CategoryBUS Instance { get { return instance; } }
        public List<Category> GetAllCategories()
        {
            return CategoryDAO.Instance.GetAllCategories();
        }
    }
}