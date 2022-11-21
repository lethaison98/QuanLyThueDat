using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Data.Entities
{
    public class HopDongThueDat
    {
        public int IdHopDongThueDat { get; set; }
        public int IdDoanhNghiep { get; set; }
        public DoanhNghiep DoanhNghiep { get; set; }
        public int? IdQuyetDinhThueDat { get; set; }
        public string SoHopDong { get; set; }
        public string TenHopDong { get; set; }
        public DateTime? NgayKyHopDong { get; set; }
        public DateTime? NgayHieuLucHopDong { get; set; }
        public DateTime? NgayHetHieuLucHopDong { get; set; }
    }
}
