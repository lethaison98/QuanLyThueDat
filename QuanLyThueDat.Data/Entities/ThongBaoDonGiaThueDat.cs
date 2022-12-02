using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Data.Entities
{
    public class ThongBaoDonGiaThueDat: BaseEntity
    {
        public int IdThongBaoDonGiaThueDat { get; set; }
        public int IdDoanhNghiep { get; set; }
        public DoanhNghiep DoanhNghiep { get; set; }
        public int? IdQuyetDinhThueDat { get; set; }
        public int? IdHopDongThueDat { get; set; }

        //Quyết định thuê đất
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

        //Thông báo đơn giá thuê đất
        public string SoThongBaoDonGiaThueDat { get; set; }
        public string TenThongBaoDonGiaThueDat { get; set; }
        public string LanThongBaoDonGiaThueDat { get; set; }
        public DateTime? NgayThongBaoDonGiaThueDat { get; set; }
        public decimal DienTichKhongPhaiNop { get; set; }
        public decimal DienTichPhaiNop { get; set; }
        public decimal DonGia { get; set; }
        public string ThoiHanDonGia { get; set; } 
        public DateTime? NgayHieuLucDonGiaThueDat { get; set; }
        public DateTime? NgayHetHieuLucDonGiaThueDat { get; set; }
        public string HinhThucThue { get; set; }
        public string LanhDaoKyThongBaoDonGiaThueDat { get; set; }

    }
}
