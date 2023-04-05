﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Application.ViewModel
{
    public class HopDongThueDatViewModel
    {
        public int IdHopDongThueDat { get; set; }
        public int IdDoanhNghiep { get; set; }
        public string TenDoanhNghiep { get; set; }
        public int? IdQuyetDinhThueDat { get; set; }
        public string SoHopDong { get; set; }
        public string TenHopDong { get; set; }
        public string NgayKyHopDong { get; set; }
        public string ThoiHanHopDong { get; set; }
        public string NguoiKy { get; set; }
        public string CoQuanKy { get; set; }
        public string NgayHieuLucHopDong { get; set; }
        public string NgayHetHieuLucHopDong { get; set; }
        public List<FileTaiLieuViewModel> DsFileTaiLieu { get; set; }
        public QuyenDuLieuViewModel QuyenDuLieu { get; set; } = new QuyenDuLieuViewModel();
    }
}
