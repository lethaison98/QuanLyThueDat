using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Data.Entities
{
    public class QuyetDinhMienTienThueDat: BaseEntity
    {
        public int IdQuyetDinhMienTienThueDat { get; set; }
        public int IdDoanhNghiep { get; set; }
        public DoanhNghiep DoanhNghiep { get; set; }
        public int? IdQuyetDinhThueDat { get; set; }
        public string SoQuyetDinhMienTienThueDat { get; set; }
        public string TenQuyetDinhMienTienThueDat { get; set; }
        public DateTime? NgayQuyetDinhMienTienThueDat { get; set; }
        public decimal DienTichMienTienThueDat { get; set; }
        public decimal SoTienMienGiam { get; set; }
        public string LoaiQuyetDinhMienTienThueDat { get; set; }
        public string ThoiHanMienTienThueDat { get; set; }
        public DateTime? NgayHieuLucMienTienThueDat { get; set; }
        public DateTime? NgayHetHieuLucMienTienThueDat { get; set; }
        public string SoQuyetDinhMienTienThueDatDieuChinh { get; set; }
        public string GhiChu { get; set; }
    }
}
