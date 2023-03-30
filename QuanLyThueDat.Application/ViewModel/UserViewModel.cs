using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Application.ViewModel
{
    public class UserViewModel
    {
        public Guid UserId { get; set; }
        public string HoTen { get; set; }
        public string DonVi { get; set; }
        public string NgaySinh { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public List<String> DsRole { get; set; }
    }
}
