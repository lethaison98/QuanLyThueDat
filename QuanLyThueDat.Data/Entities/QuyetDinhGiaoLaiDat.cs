using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Data.Entities
{
    public class QuyetDinhGiaoLaiDat
    {
        public int IdQuyetDinhGiaoLaiDat { get; set; }
        public int IdDoanhNghiep { get; set; }
        public DoanhNghiep DoanhNghiep { get; set; }
        public string SoQuyetDinh { get; set; }
        public DateTime? NgayKy { get; set; }
        public string ViTriThuaDat { get; set; }
        public decimal TongDienTich { get; set; }
        public decimal DienTichKhongPhaiNop { get; set; }
        public decimal DienTichPhaiNop { get; set; }
        public bool TrangThai { get; set; }
        public DateTime? NgayHieuLuc { get; set; }
        public DateTime? NgayHetHieuLuc { get; set; }
    }
}
