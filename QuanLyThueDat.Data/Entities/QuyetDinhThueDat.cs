using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Data.Entities
{
    public class QuyetDinhThueDat: BaseEntity
    {
        public int IdQuyetDinhThueDat { get; set; }
        public int IdDoanhNghiep { get; set; }
        public DoanhNghiep DoanhNghiep { get; set; }
        public string SoQuyetDinhThueDat { get; set; }
        public string TenQuyetDinhThueDat { get; set; }
        public DateTime? NgayQuyetDinhThueDat { get; set; }
        public string SoQuyetDinhGiaoDat { get; set; }
        public string TenQuyetDinhGiaoDat { get; set; }
        public DateTime? NgayQuyetDinhGiaoDat { get; set; }
        public decimal TongDienTich { get; set; }
        public string ThoiHanThue { get; set; }
        public DateTime? DenNgayThue { get; set; }
        public DateTime? TuNgayThue { get; set; }
        public string MucDichSuDung { get; set; }
        public string HinhThucThue { get; set; }
        public string ViTriThuaDat { get; set; }
        public string DiaChiThuaDat { get; set; }
        public int IdQuanHuyen { get; set; }
        public int IdPhuongXa { get; set; }
        public List<QuyetDinhThueDatChiTiet> DsQuyetDinhThueDatChiTiet { get; set; }
    }
}
