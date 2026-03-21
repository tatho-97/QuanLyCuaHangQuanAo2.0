using System.Collections.Generic;
using QuanLyCuaHangQuanAo2._0.DTO;
using QuanLyCuaHangQuanAo2._0.DAO;

namespace QuanLyCuaHangQuanAo2._0.BUS
{
    public class ImportBUS
    {
        private static ImportBUS instance;

        public static ImportBUS Instance
        {
            get { if (instance == null) instance = new ImportBUS(); return instance; }
        }

        private ImportBUS() { }

        public List<Import_orders> GetAllImports()
        {
            return ImportDAO.Instance.GetAllImports();
        }

        public int CreateImport(Import_orders obj)
        {
            if (obj.Employee_id <= 0 || obj.Total_amount < 0)
            {
                return -1;
            }
            return ImportDAO.Instance.InsertImport(obj);
        }

        public bool ProcessFullImport(Import_orders importOrder, List<ImportDetail> details)
        {
            int importId = CreateImport(importOrder);

            if (importId > 0)
            {
                foreach (var item in details)
                {
                    item.Import_id = importId; 
                    ImportDetailDAO.Instance.InsertImportDetail(item);
                }
                return true; 
            }
            return false;
        }
    }
}