using System.Collections.Generic;
using System.Windows.Forms;
using QuanLyCuaHangQuanAo2._0.DTO; 
using QuanLyCuaHangQuanAo2._0.DAO; 

namespace QuanLyCuaHangQuanAo2._0.BUS
{
    public class ProductBUS
    {
        private static ProductBUS instance;

        public static ProductBUS Instance
        {
            get { if (instance == null) instance = new ProductBUS(); return instance; }
        }

        private ProductBUS() { }
        public List<Product> GetAllProducts()
        {
            return ProductDAO.Instance.GetAllProducts();
        }
        public bool InsertProduct(Product product)
        {
            if (string.IsNullOrEmpty(product.Product_name)) return false;
            if (product.Product_sellingPrice < 0) return false;

            return ProductDAO.Instance.InsertProduct(product);
        }

        public bool UpdateProduct(Product product)
        {
            if (product.Product_id <= 0) return false;
            if (string.IsNullOrEmpty(product.Product_name)) return false;

            return ProductDAO.Instance.UpdateProduct(product);
        }
        public List<Product> SearchProducts(string searchType, string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword)) return GetAllProducts();
            if (searchType == "ID" || searchType == "Giá tiền")
            {
                if (!double.TryParse(keyword, out _))
                {
                    MessageBox.Show("Vui lòng nhập số hợp lệ!", "Lỗi định dạng");
                    return GetAllProducts();
                }
            }

            return ProductDAO.Instance.SearchProducts(searchType, keyword);
        }
    }
}