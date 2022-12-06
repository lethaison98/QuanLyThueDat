using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Application.ViewModel
{
    public class QuyetDinhThueDatViewModel
    {
        public int IdQuyetDinhThueDat { get; set; }
        public int IdDoanhNghiep { get; set; }
        public string TenDoanhNghiep { get; set; }
        public string SoQuyetDinhThueDat { get; set; }
        public string TenQuyetDinhThueDat { get; set; }
        public string NgayQuyetDinhThueDat { get; set; }
        public string SoQuyetDinhGiaoDat { get; set; }
        public string TenQuyetDinhGiaoDat { get; set; }
        public string NgayQuyetDinhGiaoDat { get; set; }
        public string TongDienTich { get; set; }
        public string ThoiHanThue { get; set; }
        public string DenNgayThue { get; set; }
        public string TuNgayThue { get; set; }
        public string MucDichSuDung { get; set; }
        public string HinhThucThue { get; set; }
        public string TextHinhThucThue { get; set; }
        public string ViTriThuaDat { get; set; }
        public string DiaChiThuaDat { get; set; }
        public List<QuyetDinhThueDatChiTietViewModel> DsQuyetDinhThueDatChiTiet { get; set; }


    }
    public class QuyetDinhThueDatChiTietViewModel
    {
        public int IdQuyetDinhThueDatChiTiet { get; set; }
        public int IdQuyetDinhThueDat { get; set; }
        public string HinhThucThue { get; set; }
        public string DienTich { get; set; }
        public string ThoiHanThue { get; set; }
        public string DenNgayThue { get; set; }
        public string TuNgayThue { get; set; }
        public string MucDichSuDung { get; set; }
    }
}
