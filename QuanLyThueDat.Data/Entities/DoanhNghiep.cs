using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Data.Entities
{
    public class DoanhNghiep: BaseEntity
    {
        public int IdDoanhNghiep { get; set; }
        public string TenDoanhNghiep { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public string TenNguoiDaiDien { get; set; }
        public string CoQuanQuanLyThue { get; set; }
        public string MaSoThue { get; set; }
        public DateTime? NgayCap { get; set; }
        public string NoiCap { get; set; }
        public string GhiChu { get; set; }
        public List<HopDongThueDat> DsHopDongThueDat { get; set; }
        public List<QuyetDinhDonGiaThueDat> DsQuyetDinhDonGiaThueDat { get; set; }
        public List<QuyetDinhGiaoDat> DsQuyetDinhGiaoDat { get; set; }
        public List<QuyetDinhGiaoLaiDat> DsQuyetDinhGiaoLaiDat { get; set; }
        public List<QuyetDinhThueDat> DsQuyetDinhThueDat { get; set; }
        public List<QuyetDinhMienTienThueDat> DsQuyetDinhMienTienThueDat { get; set; }
        public List<ThongBaoTienThueDat> DsThongBaoTienThueDat { get; set; }
        public List<ThongBaoTienSuDungDat> DsThongBaoTienSuDungDat { get; set; }
        public List<ThongBaoDonGiaThueDat> DsThongBaoDonGiaThueDat { get; set; }
        public List<ThongBaoGhiThuGhiChi> DsThongBaoGhiThuGhiChi { get; set; }

    }
}
