using QuanLyThueDat.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Application.Request
{
    public class ThongBaoGhiThuGhiChiRequest
    {
        public int IdThongBaoGhiThuGhiChi { get; set; }
        public int IdDoanhNghiep { get; set; }
        public int? IdQuyetDinhThueDat { get; set; }
        public string SoThongBaoGhiThuGhiChi { get; set; }
        public string NgayThongBaoGhiThuGhiChi { get; set; }
        public decimal SoTienGhiThu { get; set; }
        public string NoiDungGhiThu { get; set; }
        public decimal SoTienGhiChi { get; set; }
        public string NoiDungGhiChi { get; set; }
        public string GhiChu { get; set; }
        public List<FileTaiLieu> FileTaiLieu { get; set; } = new List<FileTaiLieu>();
    }
}
