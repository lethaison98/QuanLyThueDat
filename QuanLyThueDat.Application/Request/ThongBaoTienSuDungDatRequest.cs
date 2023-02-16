﻿using QuanLyThueDat.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Application.Request
{
    public class ThongBaoTienSuDungDatRequest
    {
        public int IdThongBaoTienSuDungDat { get; set; }
        public int IdDoanhNghiep { get; set; }
        public int? IdQuyetDinhThueDat { get; set; }
        public int? IdHopDongThueDat { get; set; }

        //Quyết định thuê đất
        public string SoQuyetDinhThueDat { get; set; }
        public string TenQuyetDinhThueDat { get; set; }
        public string NgayQuyetDinhThueDat { get; set; }
        public string MucDichSuDung { get; set; }
        public string ViTriThuaDat { get; set; }
        public string DiaChiThuaDat { get; set; }
        public string ThoiHanThue { get; set; }
        public string DenNgayThue { get; set; }
        public string TuNgayThue { get; set; }
        public decimal TongDienTich { get; set; }

        //Thông báo Tien Su Dung Dat
        public string SoThongBaoTienSuDungDat { get; set; }
        public string TenThongBaoTienSuDungDat { get; set; }
        public string LanThongBaoTienSuDungDat { get; set; }
        public string NgayThongBaoTienSuDungDat { get; set; }
        public decimal DienTichKhongPhaiNop { get; set; }
        public decimal DienTichPhaiNop { get; set; }
        public decimal DonGia { get; set; }
        public string ThoiHanDonGia { get; set; }
        public decimal SoTien { get; set; }
        public decimal SoTienMienGiam { get; set; }
        public string LyDoMienGiam { get; set; }
        public decimal SoTienBoiThuongGiaiPhongMatBang { get; set; }
        public string LyDoBoiThuongGiaiPhongMatBang { get; set; }
        public decimal SoTienPhaiNop { get; set; }
        public string NgayHieuLucTienSuDungDat { get; set; }
        public string NgayHetHieuLucTienSuDungDat { get; set; }
        public string HinhThucThue { get; set; }
        public string LanhDaoKyThongBaoTienSuDungDat { get; set; }
        public List<FileTaiLieu> FileTaiLieu { get; set; }
    }
}