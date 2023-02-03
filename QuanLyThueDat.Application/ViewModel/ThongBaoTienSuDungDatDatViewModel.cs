using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Application.ViewModel
{
    public class ThongBaoTienSuDungDatViewModel
    {
        public int IdThongBaoTienSuDungDat { get; set; }
        public int IdDoanhNghiep { get; set; }
        public int? IdQuyetDinhThueDat { get; set; }
        public int? IdHopDongThueDat { get; set; }

        //DoanhNghiep
        public string TenDoanhNghiep { get; set; }
        public string DiaChi { get; set; }
        public string MaSoThue { get; set; }
        public string CoQuanQuanLyThue { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }

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

        //Thông báo tiền sử dụng đất thuê đất
        public string SoThongBaoTienSuDungDat { get; set; }
        public string TenThongBaoTienSuDungDat { get; set; }
        public string LanThongBaoTienSuDungDat { get; set; }
        public string NgayThongBaoTienSuDungDat { get; set; }
        public string DienTichKhongPhaiNop { get; set; }
        public string DienTichPhaiNop { get; set; }
        public string DonGia { get; set; }
        public string ThoiHanDonGia { get; set; }
        public string SoTien { get; set; }
        public string SoTienMienGiam { get; set; }
        public string LyDoMienGiam { get; set; }
        public string SoTienBoiThuongGiaiPhongMatBang { get; set; }
        public string LyDoBoiThuongGiaiPhongMatBang { get; set; }
        public string SoTienPhaiNop { get; set; }
        public string NgayHieuLucTienSuDungDat { get; set; }
        public string NgayHetHieuLucTienSuDungDat { get; set; }
        public string HinhThucThue { get; set; }
        public string LanhDaoKyThongBaoTienSuDungDat { get; set; }
        public string TextTenLanhDao { get; set; }
        public string TextChucVuLanhDao { get; set; }
        public string TextKyThayLanhDao { get; set; }
        public string TextSoTienPhaiNop { get; set; }
        public string TextTongDienTich { get; set; }
        public List<FileTaiLieuViewModel> DsFileTaiLieu { get; set; }
    }
}
