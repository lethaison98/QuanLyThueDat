using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Application.ViewModel
{
    public class ThongBaoGhiThuGhiChiViewModel
    {
        public int IdThongBaoGhiThuGhiChi { get; set; }
        public int IdDoanhNghiep { get; set; }
        public string TenDoanhNghiep { get; set; }
        public string SoQuyetDinhThueDat { get; set; }
        public int? IdQuyetDinhThueDat { get; set; }
        public string SoThongBaoGhiThuGhiChi { get; set; }
        public string NgayThongBaoGhiThuGhiChi { get; set; }
        public string SoTienGhiThu { get; set; }
        public string NoiDungGhiThu { get; set; }
        public string SoTienGhiChi { get; set; }
        public string NoiDungGhiChi { get; set; }
        public string GhiChu { get; set; }
        public List<FileTaiLieuViewModel> DsFileTaiLieu { get; set; }
        public QuyenDuLieuViewModel QuyenDuLieu { get; set; } = new QuyenDuLieuViewModel();
    }
}
