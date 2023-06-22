using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Data.Entities
{
    public class ThongBaoTienThueDatChiTiet
    {
        public int IdThongBaoTienThueDatChiTiet { get; set; }
        public int IdThongBaoTienThueDat { get; set; }
        public int? IdThongBaoDonGiaThueDat { get; set; }
        public int? IdQuyetDinhMienTienThueDat { get; set; }
        public int? IdThongBaoTienThueDatGoc { get; set; }

        public ThongBaoTienThueDat ThongBaoTienThueDat { get; set; }
        public int Nam { get; set; }
        public decimal DonGia { get; set; }
        public decimal DienTichKhongPhaiNop { get; set; } 
        public decimal DienTichPhaiNop { get; set; }
        public decimal SoTien { get; set; }
        public decimal SoTienMienGiam { get; set; }
        public decimal SoTienPhaiNop { get; set; }
        public DateTime? TuNgayTinhTien { get; set; }
        public DateTime? DenNgayTinhTien { get; set; }
        public string GhiChu { get; set; }
    }
}
