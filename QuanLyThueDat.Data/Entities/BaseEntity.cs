using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Data.Entities
{
    public class BaseEntity
    {
        public DateTime? NgayTao { get; set; }
        public string NguoiTao { get; set; }
        public string IdNguoiTao { get; set; }
        public DateTime? NgayCapNhat { get; set; }
        public string NguoiCapNhat { get; set; }
        public string IdNguoiCapNhat { get; set; }
        public bool IsDeleted { get; set; }
    }
}
