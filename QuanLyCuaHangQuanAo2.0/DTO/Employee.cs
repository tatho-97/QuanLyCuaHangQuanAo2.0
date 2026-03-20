
namespace QuanLyCuaHangQuanAo2._0.DTO
{
    public class Employee : User
    {
        public int Employee_id { get; set; } //
        public int Role_id { get; set; } //
        public string Username { get; set; } //
        public string Password_hash { get; set; } //
        public bool Is_deleted { get; set; } //
    }
}
