using BaiTapLon.DAO;
using BTL_QuanLyKhoHang_Nhom20;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaiTapLon.BUS
{
    internal class ProductBUS
    {
        private static ProductBUS instance = new ProductBUS();
        public static ProductBUS Instance {  get { return instance; } }
        public List<Product> GetAllProducts()
        {
            return ProductDAO.Instance.GetAllProducts();
        }

        public bool InsertProduct(Product product)
        {
            if (string.IsNullOrEmpty(product.Product_name)) return false; // Tên không được để trống
            if (product.Product_sellingPrice < 0) return false;          // Giá không được âm

            return ProductDAO.Instance.InsertProduct(product);
        }

        public bool UpdateProduct(Product product)
        {
            if (product.Product_id <= 0) return false; // ID phải hợp lệ
            if (string.IsNullOrEmpty(product.Product_name)) return false;

            return ProductDAO.Instance.UpdateProduct(product);
        }

        public bool DeleteProduct(int id)
        {
            if (id <= 0) return false;
            return ProductDAO.Instance.DeleteProduct(id);
        }

        public List<Product> SearchProducts(string searchType, string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword)) return GetAllProducts();
            if (searchType == "ID" || searchType == "Giá tiền")
            {
                if (!double.TryParse(keyword, out _) && !string.IsNullOrEmpty(keyword))
                {
                    MessageBox.Show("Vui lòng nhập số hợp lệ cho mục này!", "Lỗi nhập liệu");
                }
            }
            return ProductDAO.Instance.SearchProducts(searchType, keyword);
        }
    }
}
