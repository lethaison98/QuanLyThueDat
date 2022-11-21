using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Data.Entities
{
    public class AppUser: IdentityUser<Guid>
    {
        public string HoTen { get; set; }
        public string DonVi { get; set; }
        public DateTime NgaySinh { get; set; }

    }
}
