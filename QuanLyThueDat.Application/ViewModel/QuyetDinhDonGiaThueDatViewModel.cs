using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Application.ViewModel
{
    public class QuyetDinhDonGiaThueDatViewModel
    {
        public int IdQuyetDinhDonGiaThueDat { get; set; }
        public int IdDoanhNghiep { get; set; }
        public string TenDoanhNghiep { get; set; }
        public string SoQuyetDinh { get; set; }
        public DateTime? NgayKy { get; set; }
        public decimal DienTich { get; set; }
        public string ViTriThuaDat { get; set; }
        public DateTime? NgayHieuLuc { get; set; }
        public DateTime? NgayHetHieuLuc { get; set; }

    }
}
