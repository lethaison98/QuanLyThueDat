using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Application.ViewModel
{
    public class ThongBaoTienThueDatViewModel
    {
        public int IdThongBaoTienThueDat { get; set; }
        public int IdDoanhNghiep { get; set; }
        public int? IdQuyetDinhThueDat { get; set; }
        public int? IdHopDongThueDat { get; set; }
        public int? IdQuyetDinhMienTienThueDat { get; set; }
        public int? IdThongBaoDonGiaThueDat { get; set; }
        public string SoThongBaoTienThueDat { get; set; }
        public string TenThongBaoTienThueDat { get; set; }
        public int Nam { get; set; }
        public string NgayThongBaoTienThueDat { get; set; }
        public string LoaiThongBaoTienThueDat { get; set; }
        public string LanhDaoKyThongBaoTienThueDat { get; set; }
        public string TextLoaiThongBaoTienThueDat { get; set; }
        public string TextTenLanhDao { get; set; }
        public string TextChucVuLanhDao { get; set; }
        public string TextKyThayLanhDao { get; set; }
        
        //DoanhNghiep
        public string TenDoanhNghiep { get; set; }
        public string MaSoThue { get; set; }
        public string CoQuanQuanLyThue { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }

        //QuyetDinhThueDat
        public string SoQuyetDinhThueDat { get; set; }
        public string TenQuyetDinhThueDat { get; set; }
        public string NgayQuyetDinhThueDat { get; set; }
        public string MucDichSuDung { get; set; }

        //ThongBaoDonGiaThueDat
        public string SoThongBaoDonGiaThueDat { get; set; }
        public string TenThongBaoDonGiaThueDat { get; set; }
        public string NgayThongBaoDonGiaThueDat { get; set; }
        public string DonGia { get; set; }
        public string ThoiHanDonGiaThueDat { get; set; }

        public string ViTriThuaDat { get; set; }
        public string DiaChiThuaDat { get; set; }
        public string ThoiHanThue { get; set; }
        public string DenNgayThue { get; set; }
        public string TuNgayThue { get; set; }
        public string TongDienTich { get; set; }
        public string TextTongDienTich { get; set; }
        public string DienTichKhongPhaiNop { get; set; }
        public string DienTichPhaiNop { get; set; }
        public string SoTien { get; set; }
        public string SoTienMienGiam { get; set; }
        public string SoTienPhaiNop { get; set; }
        public string TextSoTienPhaiNop { get; set; }

        public List<ThongBaoTienThueDatChiTietViewModel> DsThongBaoTienThueDatChiTiet { get; set; }
        public List<FileTaiLieuViewModel> DsFileTaiLieu { get; set; }
    }

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
