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
        public string NgayHieuLucHopDong { get; set; }
        public string NgayHetHieuLucHopDong { get; set; }
    }
}
