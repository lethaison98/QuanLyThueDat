using QuanLyThueDat.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Application.Request
{
    public class QuyetDinhThueDatRequest
    {
        public int IdQuyetDinhThueDat { get; set; }
        public int IdDoanhNghiep { get; set; }
        public string SoQuyetDinhThueDat { get; set; }
        public string TenQuyetDinhThueDat { get; set; }
        public string NgayQuyetDinhThueDat { get; set; }
        public string SoQuyetDinhGiaoDat { get; set; }
        public string TenQuyetDinhGiaoDat { get; set; }
        public string NgayQuyetDinhGiaoDat { get; set; }
        public decimal TongDienTich { get; set; }
        public string ThoiHanThue { get; set; }
        public string DenNgayThue { get; set; }
        public string TuNgayThue { get; set; }
        public string MucDichSuDung { get; set; }
        public string HinhThucThue { get; set; }
        public string ViTriThuaDat { get; set; }
        public string DiaChiThuaDat { get; set; }
        public int IdQuanHuyen { get; set; }
        public List<QuyetDinhThueDatChiTietRequest> QuyetDinhThueDatChiTiet { get; set; } 
        public List<FileTaiLieu> FileTaiLieu { get; set; } = new List<FileTaiLieu>();
    }
    public class QuyetDinhThueDatChiTietRequest
    {
        public int IdQuyetDinhThueDatChiTiet { get; set; }
        public int IdQuyetDinhThueDat { get; set; }
        public string HinhThucThue { get; set; }
        public decimal DienTich { get; set; }
        public string ThoiHanThue { get; set; }
        public string DenNgayThue { get; set; }
        public string TuNgayThue { get; set; }
        public string MucDichSuDung { get; set; }
    }
}
