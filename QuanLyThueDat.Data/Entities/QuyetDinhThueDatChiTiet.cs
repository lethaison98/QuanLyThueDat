using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Data.Entities
{
    public class QuyetDinhThueDatChiTiet
    {
        public int IdQuyetDinhThueDatChiTiet { get; set; }
        public int IdQuyetDinhThueDat { get; set; }
        public QuyetDinhThueDat QuyetDinhThueDat { get; set; }
        public string HinhThucThue { get; set; }
        public decimal DienTich { get; set; }
        public string ThoiHanThue { get; set; }
        public DateTime? DenNgayThue { get; set; }
        public DateTime? TuNgayThue { get; set; }
        public string MucDichSuDung { get; set; }
        public string GhiChu { get; set; }
    }
}
