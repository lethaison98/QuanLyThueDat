using QuanLyThueDat.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Application.Request
{
    public class HopDongThueDatRequest
    {
        public int IdHopDongThueDat { get; set; }
        public int IdDoanhNghiep { get; set; }
        public int? IdQuyetDinhThueDat { get; set; }
        public string SoHopDong { get; set; }
        public string TenHopDong { get; set; }
        public string NgayKyHopDong { get; set; }
        public string NguoiKy { get; set; }
        public string CoQuanKy { get; set; }
        public string NgayHieuLucHopDong { get; set; }
        public string NgayHetHieuLucHopDong { get; set; }
        public string SoHopDongDieuChinh { get; set; }
        public string GhiChu { get; set; }
        public decimal DienTich { get; set; }
        public List<FileTaiLieu> FileTaiLieu { get; set; } = new List<FileTaiLieu>();
    }
}
