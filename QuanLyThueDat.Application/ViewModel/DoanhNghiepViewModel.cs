using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Application.ViewModel
{
    public class DoanhNghiepViewModel
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
        public List<HopDongThueDatViewModel> DsHopDongThueDat { get; set; }
        public List<QuyetDinhDonGiaThueDatViewModel> DsQuyetDinhDonGiaThueDat { get; set; }
        public List<QuyetDinhGiaoDatViewModel> DsQuyetDinhGiaoDat { get; set; }
        public List<QuyetDinhGiaoLaiDatViewModel> DsQuyetDinhGiaoLaiDat { get; set; }
        public List<QuyetDinhThueDatViewModel> DsQuyetDinhThueDat { get; set; }
        public List<QuyetDinhMienTienThueDatViewModel> DsQuyetDinhMienTienThueDat { get; set; }
        public List<ThongBaoTienThueDatViewModel> DsThongBaoTienThueDat { get; set; }
        public List<ThongBaoDonGiaThueDatViewModel> DsThongBaoDonGiaThueDat { get; set; }

    }
}
