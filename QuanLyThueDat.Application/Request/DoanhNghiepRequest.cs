using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Application.Request
{
    public class DoanhNghiepRequest
    {
        public int IdDoanhNghiep { get; set; }
        public string TenDoanhNghiep { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public string TenNguoiDaiDien { get; set; }
        public string CoQuanQuanLyThue { get; set; }
        public string MaSoThue { get; set; }
        public string NgayCap { get; set; }
        public string NoiCap { get; set; }
        public string GhiChu { get; set; }
    }
}
