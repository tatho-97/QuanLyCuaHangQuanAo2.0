using System.Collections.Generic;
using QuanLyCuaHangQuanAo2._0.DTO;
using QuanLyCuaHangQuanAo2._0.DAO;

namespace QuanLyCuaHangQuanAo2._0.BUS
{
    public class OrderBUS
    {
        private static OrderBUS instance;

        public static OrderBUS Instance
        {
            get { if (instance == null) instance = new OrderBUS(); return instance; }
        }

        private OrderBUS() { }

        public List<Order> GetAllOrders()
        {
            return OrderDAO.Instance.GetAllOrders();
        }

        public int CreateOrder(Order obj)
        {
            if (obj.Customer_id <= 0 || obj.Total_quantity <= 0)
            {
                return -1;
            }
            return OrderDAO.Instance.InsertOrder(obj);
        }

        public bool ProcessFullPayment(Order order, List<OrderDetail> details)
        {
            int orderId = CreateOrder(order);
            if (orderId > 0)
            {
                foreach (var item in details)
                {
                    item.Order_id = orderId;
                    OrderDetailDAO.Instance.InsertOrderDetail(item);
                }
                return true;
            }
            return false;
        }
    }
}