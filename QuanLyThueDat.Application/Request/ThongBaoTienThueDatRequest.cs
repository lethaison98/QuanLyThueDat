using QuanLyThueDat.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Application.Request
{
    public class ThongBaoTienThueDatRequest
    {
        public int IdThongBaoTienThueDat { get; set; }
        public int IdDoanhNghiep { get; set; }
        public int? IdQuyetDinhThueDat { get; set; }
        public int? IdHopDongThueDat { get; set; }
        public int? IdQuyetDinhMienTienThueDat { get; set; }
        public int? IdThongBaoDonGiaThueDat { get; set; }


        //QuyetDinhThueDat
        public string SoQuyetDinhThueDat { get; set; }
        public string TenQuyetDinhThueDat { get; set; }
        public string NgayQuyetDinhThueDat { get; set; }
        public string MucDichSuDung { get; set; }
        public string ViTriThuaDat { get; set; }
        public string DiaChiThuaDat { get; set; }
        public string ThoiHanThue { get; set; }
        public string DenNgayThue { get; set; }
        public string TuNgayThue { get; set; }
        public decimal TongDienTich { get; set; }

        //ThongBaoDonGiaThueDat
        public string SoThongBaoDonGiaThueDat { get; set; }
        public string TenThongBaoDonGiaThueDat { get; set; }
        public string NgayThongBaoDonGiaThueDat { get; set; }
        public decimal DonGia { get; set; }
        public string LanhDaoKyThongBaoDonGiaThueDat { get; set; }

        //QuyetDinhMienTienThueDat
        public decimal DienTichKhongPhaiNop { get; set; }
        public decimal SoTienMienGiam { get; set; }

        // Thông báo tiền thuê đất
        public string SoThongBaoTienThueDat { get; set; }
        public int Nam { get; set; }
        public string NgayThongBaoTienThueDat { get; set; }
        public string LoaiThongBaoTienThueDat { get; set; }
        public string LanhDaoKyThongBaoTienThueDat { get; set; }
        public decimal DienTichPhaiNop { get; set; }
        public decimal SoTien { get; set; }
        public decimal SoTienPhaiNop { get; set; }
        public List<ThongBaoTienThueDatChiTietRequest> ThongBaoTienThueDatChiTiet { get; set; }
        public List<FileTaiLieu> FileTaiLieu { get; set; }
    }
    public class ThongBaoTienThueDatChiTietRequest
    {
        public int IdThongBaoTienThueDatChiTiet { get; set; }
        public int IdThongBaoTienThueDat { get; set; }
        public int Nam { get; set; }
        public decimal DonGia { get; set; }
        public decimal DienTichKhongPhaiNop { get; set; }
        public decimal DienTichPhaiNop { get; set; }
        public decimal SoTien { get; set; }
        public decimal SoTienMienGiam { get; set; }
        public decimal SoTienPhaiNop { get; set; }
        public string TuNgayTinhTien { get; set; }
        public string DenNgayTinhTien { get; set; }
        public string GhiChu { get; set; }
    }
}
