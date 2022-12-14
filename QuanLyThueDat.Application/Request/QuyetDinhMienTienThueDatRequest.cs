using QuanLyThueDat.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Application.Request
{
    public class QuyetDinhMienTienThueDatRequest
    {
        public int IdQuyetDinhMienTienThueDat { get; set; }
        public int IdDoanhNghiep { get; set; }
        public int? IdQuyetDinhThueDat { get; set; }
        public string SoQuyetDinhMienTienThueDat { get; set; }
        public string TenQuyetDinhMienTienThueDat { get; set; }
        public string NgayQuyetDinhMienTienThueDat { get; set; }
        public decimal DienTichMienTienThueDat { get; set; }
        public string ThoiHanMienTienThueDat { get; set; }
        public string NgayHieuLucMienTienThueDat { get; set; }
        public string NgayHetHieuLucMienTienThueDat { get; set; }
        public List<FileTaiLieu> FileTaiLieu { get; set; }
    }
}
