using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Data.Entities
{
    public class AppRole : IdentityRole<Guid>
    {
        public string MoTa { get; set; }
    }
}
