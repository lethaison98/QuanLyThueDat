using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Application.ViewModel
{
    public class ThongBaoDonGiaThueDatViewModel
    {
        public int IdThongBaoDonGiaThueDat { get; set; }
        public int IdDoanhNghiep { get; set; }
        public int? IdQuyetDinhThueDat { get; set; }
        public int? IdHopDongThueDat { get; set; }

        //DoanhNghiep
        public string TenDoanhNghiep { get; set; }
        public string DiaChi { get; set; }
        public string MaSoThue { get; set; }
        public string CoQuanQuanLyThue { get; set; }

        //Quyết định thuê đất
        public string SoQuyetDinhThueDat { get; set; }
        public string TenQuyetDinhThueDat { get; set; }
        public string NgayQuyetDinhThueDat { get; set; }
        public string MucDichSuDung { get; set; }
        public string ViTriThuaDat { get; set; }
        public string DiaChiThuaDat { get; set; }
        public string ThoiHanThue { get; set; }
        public string DenNgayThue { get; set; }
        public string TuNgayThue { get; set; }
        public string TongDienTich { get; set; }

        //Thông báo đơn giá thuê đất
        public string SoThongBaoDonGiaThueDat { get; set; }
        public string TenThongBaoDonGiaThueDat { get; set; }
        public string LanThongBaoDonGiaThueDat { get; set; }
        public string NgayThongBaoDonGiaThueDat { get; set; }
        public string DienTichKhongPhaiNop { get; set; }
        public string DienTichPhaiNop { get; set; }
        public string DonGia { get; set; }
        public string ThoiHanDonGia { get; set; }
        public string NgayHieuLucDonGiaThueDat { get; set; }
        public string NgayHetHieuLucDonGiaThueDat { get; set; }
        public string HinhThucThue { get; set; }
        public string LanhDaoKyThongBaoDonGiaThueDat { get; set; }
        public string TextTenLanhDao { get; set; }
        public string TextChucVuLanhDao { get; set; }
        public string TextKyThayLanhDao { get; set; }
        public List<FileTaiLieuViewModel> DsFileTaiLieu { get; set; }
        public QuyenDuLieuViewModel QuyenDuLieu { get; set; } = new QuyenDuLieuViewModel();
    }
}
