using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Data.Entities
{
    public class ThongBaoTienThueDatChiTietViewModel
    {
        public int IdThongBaoTienThueDatChiTiet { get; set; }
        public int IdThongBaoTienThueDat { get; set; }

        public int Nam { get; set; }
        public string DonGia { get; set; }
        public string DienTichKhongPhaiNop { get; set; } 
        public string DienTichPhaiNop { get; set; }
        public string SoTien { get; set; }
        public string SoTienMienGiam { get; set; }
        public string SoTienPhaiNop { get; set; }
        public string TuNgayTinhTien { get; set; }
        public string DenNgayTinhTien { get; set; }
        public string GhiChu { get; set; }
    }
}
