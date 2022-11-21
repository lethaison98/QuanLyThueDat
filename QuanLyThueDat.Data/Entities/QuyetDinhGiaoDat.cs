using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Data.Entities
{
    public class QuyetDinhGiaoDat
    {
        public int IdQuyetDinhGiaoDat { get; set; }
        public string SoQuyetDinh { get; set; }
        public DateTime? NgayKy { get; set; }
        public string ViTriThuaDat { get; set; }
        public decimal DienTich { get; set; }
        public DateTime? NgayHieuLuc { get; set; }
        public DateTime? NgayHetHieuLuc { get; set; }
    }
}
