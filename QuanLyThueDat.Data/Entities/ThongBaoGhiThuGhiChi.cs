using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Data.Entities
{
    public class ThongBaoGhiThuGhiChi: BaseEntity
    {
        public int IdThongBaoGhiThuGhiChi { get; set; }
        public int IdDoanhNghiep { get; set; }
        public DoanhNghiep DoanhNghiep { get; set; }
        public int? IdQuyetDinhThueDat { get; set; }
        public string SoThongBaoGhiThuGhiChi { get; set; }
        public DateTime? NgayThongBaoGhiThuGhiChi { get; set; }
        public decimal SoTienGhiThu { get; set; }
        public string NoiDungGhiThu { get; set; }
        public decimal SoTienGhiChi { get; set; }
        public string NoiDungGhiChi { get; set; }
        public string GhiChu { get; set; }
    }
}
