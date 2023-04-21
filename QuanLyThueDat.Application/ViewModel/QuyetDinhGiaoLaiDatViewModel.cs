using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Application.ViewModel
{
    public class QuyetDinhGiaoLaiDatViewModel
    {
        public int IdQuyetDinhGiaoLaiDat { get; set; }
        public int IdQuyetDinhThueDat { get; set; }
        public int IdDoanhNghiep { get; set; }
        public string TenDoanhNghiep { get; set; }
        public string SoQuyetDinh { get; set; }
        public string NgayKy { get; set; }
        public string ViTriThuaDat { get; set; }
        public string TongDienTich { get; set; }
        public string DienTichKhongPhaiNop { get; set; }
        public string DienTichPhaiNop { get; set; }
        public bool TrangThai { get; set; }
        public string NgayHieuLuc { get; set; }
        public string NgayHetHieuLuc { get; set; }
    }
}
