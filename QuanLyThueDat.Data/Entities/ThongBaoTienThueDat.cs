using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Data.Entities
{
    public class ThongBaoTienThueDat: BaseEntity
    {
        public int IdThongBaoTienThueDat { get; set; }
        public int IdDoanhNghiep { get; set; }
        public DoanhNghiep DoanhNghiep { get; set; }
        public int? IdQuyetDinhThueDat { get; set; }
        public int? IdHopDongThueDat { get; set; }
        public int? IdQuyetDinhMienTienThueDat { get; set; }
        public int? IdThongBaoDonGiaThueDat { get; set; }
        public string SoThongBaoTienThueDat { get; set; }
        public int Nam { get; set; }
        public DateTime? NgayThongBaoTienThueDat { get; set; }
        public string LanThongBaoTienThueDat { get; set; }

        //QuyetDinhThueDat
        public string SoQuyetDinhThueDat { get; set; }
        public string TenQuyetDinhThueDat { get; set; }
        public DateTime? NgayQuyetDinhThueDat { get; set; }
        public string MucDichSuDung { get; set; }
        public string ViTriThuaDat { get; set; }
        public string DiaChiThuaDat { get; set; }
        public string ThoiHanThue { get; set; }
        public DateTime? DenNgayThue { get; set; }
        public DateTime? TuNgayThue { get; set; }
        public decimal TongDienTich { get; set; }

        //ThongBaoDonGiaThueDat
        public string SoThongBaoDonGiaThueDat { get; set; } 
        public string TenThongBaoDonGiaThueDat { get; set; } 
        public DateTime? NgayThongBaoDonGiaThueDat { get; set; }
        public decimal DonGia { get; set; }

        //QuyetDinhMienTienThueDat
        public decimal DienTichKhongPhaiNop { get; set; }
        public decimal SoTienMienGiam { get; set; }
        
        public decimal DienTichPhaiNop { get; set; }
        public decimal SoTien { get; set; }
        public decimal SoTienPhaiNop { get; set; }
    }
}
