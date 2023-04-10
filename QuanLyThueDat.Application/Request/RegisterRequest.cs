using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Application.Request
{
    public class RegisterRequest
    {
        public string HoTen { get; set; }
        public string DonVi { get; set; }
        public DateTime NgaySinh { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
    public class RoleRequest
    {
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string MoTa { get; set; }
    }
    public class UserRequest
    {
        public Guid UserId { get; set; }
        public string HoTen { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<String>DsRole { get; set; }
    }
    public class ChangePasswordRequest
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; } 
    }
}
