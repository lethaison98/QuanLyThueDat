﻿using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OfficeOpenXml;
using QuanLyThueDat.Application.Common.Constant;
using QuanLyThueDat.Application.Interfaces;
using QuanLyThueDat.Application.Request;
using QuanLyThueDat.Application.ViewModel;
using QuanLyThueDat.Application.ViewModel.BaoCaoViewModel;
using QuanLyThueDat.Data.EF;
using QuanLyThueDat.Data.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Application.Service
{
    public class BaoCaoService : IBaoCaoService
    {
        private readonly QuanLyThueDatDbContext _context;
        private readonly IDoanhNghiepService _DoanhNghiepService;
        private readonly IQuyetDinhThueDatService _QuyetDinhThueDatService;
        private readonly IHopDongThueDatService _HopDongThueDatService;
        private readonly IQuyetDinhMienTienThueDatService _QuyetDinhMienTienThueDatService;
        private readonly IThongBaoDonGiaThueDatService _ThongBaoDonGiaThueDatService;
        private readonly IThongBaoTienThueDatService _ThongBaoTienThueDatService;

        public BaoCaoService(QuanLyThueDatDbContext context,
            IDoanhNghiepService DoanhNghiepService,
            IQuyetDinhThueDatService QuyetDinhThueDatService,
            IHopDongThueDatService HopDongThueDatService,
            IQuyetDinhMienTienThueDatService QuyetDinhMienTienThueDatService,
            IThongBaoDonGiaThueDatService ThongBaoDonGiaThueDatService,
            IThongBaoTienThueDatService ThongBaoTienThueDatService)
        {
            _context = context;
            _DoanhNghiepService = DoanhNghiepService;
            _QuyetDinhThueDatService = QuyetDinhThueDatService;
            _HopDongThueDatService = HopDongThueDatService;
            _QuyetDinhMienTienThueDatService = QuyetDinhMienTienThueDatService;
            _ThongBaoDonGiaThueDatService = ThongBaoDonGiaThueDatService;
            _ThongBaoTienThueDatService = ThongBaoTienThueDatService;
        }
        public async Task<ApiResult<List<BaoCaoDoanhNghiepThueDatViewModel>>> BaoCaoDoanhNghiepThueDat()
        {
            var result = new List<BaoCaoDoanhNghiepThueDatViewModel>();
            var dsDoanhNghiep = await _DoanhNghiepService.GetAll();
            if(dsDoanhNghiep.Data.Count != 0)
            {
                foreach (var doanhNghiep in dsDoanhNghiep.Data)
                {
                    var dsQuyetDinhThueDat = await _QuyetDinhThueDatService.GetListQuyetDinhThueDatChiTiet(doanhNghiep.IdDoanhNghiep);
                    var dsHopDong = await _HopDongThueDatService.GetAll(doanhNghiep.IdDoanhNghiep);
                    if(dsQuyetDinhThueDat.Data.Count != 0)
                    {
                        var dsQuyetDinhThueDatTraTienHangNam = dsQuyetDinhThueDat.Data.Where(x => x.HinhThucThue == "ThueDatTraTienHangNam" || x.HinhThucThue == "HopDongThueLaiDat");
                        foreach (var quyetDinhThueDat in dsQuyetDinhThueDatTraTienHangNam)
                        {
                            if (!String.IsNullOrEmpty(quyetDinhThueDat.SoQuyetDinhThueDat))
                            {
                                var rq = new ThongBaoDonGiaThueDatRequest();
                                rq.IdQuyetDinhThueDat = quyetDinhThueDat.IdQuyetDinhThueDat;
                                //rq.SoQuyetDinhThueDat = quyetDinhThueDat.SoQuyetDinhThueDat;
                                //rq.NgayQuyetDinhThueDat = quyetDinhThueDat.NgayQuyetDinhThueDat;
                                var dsThongBaoDonGiaThueDat = await _ThongBaoDonGiaThueDatService.GetAllByRequest(rq);
                                if (dsThongBaoDonGiaThueDat.Data.Count != 0)
                                {
                                    foreach (var thongBaoDonGia in dsThongBaoDonGiaThueDat.Data)
                                    {
                                        var item = new BaoCaoDoanhNghiepThueDatViewModel();
                                        item.DoanhNghiepViewModel = doanhNghiep;
                                        item.QuyetDinhThueDatViewModel = quyetDinhThueDat;
                                        item.ThongBaoDonGiaThueDatViewModel = thongBaoDonGia;
                                        foreach (var hopDong in dsHopDong.Data)
                                        {
                                            if (quyetDinhThueDat.IdQuyetDinhThueDat == hopDong.IdQuyetDinhThueDat)
                                            {
                                                item.HopDongThueDatViewModel = hopDong;
                                            }
                                        }
                                        result.Add(item);
                                    }
                                }
                                else
                                {
                                    var item = new BaoCaoDoanhNghiepThueDatViewModel();
                                    item.DoanhNghiepViewModel = doanhNghiep;
                                    item.QuyetDinhThueDatViewModel = quyetDinhThueDat;
                                    foreach (var hopDong in dsHopDong.Data)
                                    {
                                        if (quyetDinhThueDat.IdQuyetDinhThueDat == hopDong.IdQuyetDinhThueDat)
                                        {
                                            item.HopDongThueDatViewModel = hopDong;
                                        }
                                    }
                                    result.Add(item);
                                }
                            }                        
                            else
                            {
                                var item = new BaoCaoDoanhNghiepThueDatViewModel();
                                item.DoanhNghiepViewModel = doanhNghiep;
                                item.QuyetDinhThueDatViewModel = quyetDinhThueDat;
                                foreach (var hopDong in dsHopDong.Data)
                                {
                                    if (quyetDinhThueDat.IdQuyetDinhThueDat == hopDong.IdQuyetDinhThueDat)
                                    {
                                        item.HopDongThueDatViewModel = hopDong;
                                    }
                                }
                                result.Add(item);
                            }
                        }
                    }
                    else
                    {
                        var item = new BaoCaoDoanhNghiepThueDatViewModel();
                        item.DoanhNghiepViewModel = doanhNghiep;
                        result.Add(item);
                    }

                }
            }
            return new ApiSuccessResult<List<BaoCaoDoanhNghiepThueDatViewModel>>() { Data = result };
        }
        public async Task<ApiResult<List<ThongBaoTienThueDatViewModel>>> BaoCaoTienThueDat(int? nam)
        {
            var query = from a in _context.ThongBaoTienThueDat.Include(x => x.DoanhNghiep).Include(x => x.DsThongBaoTienThueDatChiTiet)
                        select a;
            
            if (nam != null && nam != 0)
            {
                query = query.Where(x => x.Nam == nam);
            }
            var data = await query.OrderByDescending(x => x.Nam).ThenBy(x => x.SoThongBaoTienThueDat).ToListAsync();
            var result = new List<ThongBaoTienThueDatViewModel>();
            foreach (var entity in data)
            {
                var thoiHanDonGia = "";
                var soThongBaoDonGia = "";
                var donGia = "";
                if (!String.IsNullOrEmpty(entity.SoThongBaoDonGiaThueDat))
                {
                    var tbDonGia = await _context.ThongBaoDonGiaThueDat.FirstOrDefaultAsync(x => x.SoThongBaoDonGiaThueDat == entity.SoThongBaoDonGiaThueDat);
                    if (tbDonGia != null)
                    {
                        thoiHanDonGia = tbDonGia.ThoiHanDonGia + (tbDonGia.NgayHieuLucDonGiaThueDat != null ? " từ ngày " + tbDonGia.NgayHieuLucDonGiaThueDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "") + (tbDonGia.NgayHetHieuLucDonGiaThueDat != null ? " đến ngày " + tbDonGia.NgayHetHieuLucDonGiaThueDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "");
                        soThongBaoDonGia = tbDonGia.SoThongBaoDonGiaThueDat + " ngày " + tbDonGia.NgayThongBaoDonGiaThueDat?.ToString("N", new CultureInfo("vi-VN"));
                        donGia = tbDonGia.DonGia.ToString("N0", new CultureInfo("vi-VN"));
                    }
                }
                else
                {
                    if(entity.DsThongBaoTienThueDatChiTiet.Count > 0)
                    {
                        var tbDonGia = await _context.ThongBaoDonGiaThueDat.FirstOrDefaultAsync(x => x.IdThongBaoDonGiaThueDat == entity.DsThongBaoTienThueDatChiTiet.Last().IdThongBaoDonGiaThueDat);
                        thoiHanDonGia = tbDonGia.ThoiHanDonGia + (tbDonGia.NgayHieuLucDonGiaThueDat != null ? " từ ngày " + tbDonGia.NgayHieuLucDonGiaThueDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "") + (tbDonGia.NgayHetHieuLucDonGiaThueDat != null ? " đến ngày " + tbDonGia.NgayHetHieuLucDonGiaThueDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "");
                        soThongBaoDonGia = tbDonGia.SoThongBaoDonGiaThueDat + (tbDonGia.NgayThongBaoDonGiaThueDat != null ? " ngày " + tbDonGia.NgayThongBaoDonGiaThueDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "");
                        donGia = tbDonGia.DonGia.ToString("N0", new CultureInfo("vi-VN"));
                    }
                }
                var thongBaoTienThueDat = new ThongBaoTienThueDatViewModel
                {
                    IdThongBaoTienThueDat = entity.IdThongBaoTienThueDat,
                    IdDoanhNghiep = entity.IdDoanhNghiep,
                    IdQuyetDinhThueDat = entity.IdQuyetDinhThueDat,
                    IdHopDongThueDat = entity.IdHopDongThueDat,
                    IdQuyetDinhMienTienThueDat = entity.IdQuyetDinhMienTienThueDat,
                    IdThongBaoDonGiaThueDat = entity.IdThongBaoDonGiaThueDat,
                    TenDoanhNghiep = entity.DoanhNghiep.TenDoanhNghiep,
                    MaSoThue = entity.DoanhNghiep.MaSoThue,
                    CoQuanQuanLyThue = entity.DoanhNghiep.CoQuanQuanLyThue,
                    DiaChi = entity.DoanhNghiep.DiaChi,
                    SoDienThoai = entity.DoanhNghiep.SoDienThoai,
                    Email = entity.DoanhNghiep.Email,
                    LoaiThongBaoTienThueDat = entity.LoaiThongBaoTienThueDat,
                    SoQuyetDinhThueDat = entity.SoQuyetDinhThueDat,
                    TenQuyetDinhThueDat = entity.TenQuyetDinhThueDat,
                    NgayQuyetDinhThueDat = entity.NgayQuyetDinhThueDat != null ? entity.NgayQuyetDinhThueDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    MucDichSuDung = entity.MucDichSuDung,
                    SoThongBaoTienThueDat = entity.SoThongBaoTienThueDat,
                    LanhDaoKyThongBaoTienThueDat = entity.LanhDaoKyThongBaoTienThueDat,
                    SoThongBaoDonGiaThueDat = soThongBaoDonGia,
                    TenThongBaoDonGiaThueDat = entity.TenThongBaoDonGiaThueDat,
                    NgayThongBaoDonGiaThueDat = entity.NgayThongBaoDonGiaThueDat != null ? entity.NgayThongBaoDonGiaThueDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    ThoiHanDonGiaThueDat = thoiHanDonGia,
                    ViTriThuaDat = entity.ViTriThuaDat,
                    DienTichKhongPhaiNop = entity.DienTichKhongPhaiNop.ToString("N", new CultureInfo("vi-VN")),
                    DienTichPhaiNop = entity.DienTichPhaiNop.ToString("N", new CultureInfo("vi-VN")),
                    DonGia = donGia,
                    NgayThongBaoTienThueDat = entity.NgayThongBaoTienThueDat != null ? entity.NgayThongBaoTienThueDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    SoTien = entity.SoTien.ToString("N0", new CultureInfo("vi-VN")),
                    SoTienMienGiam = entity.SoTienMienGiam.ToString("N0", new CultureInfo("vi-VN")),
                    SoTienPhaiNop = entity.SoTienPhaiNop.ToString("N0", new CultureInfo("vi-VN")),
                    Nam = entity.Nam,
                    DiaChiThuaDat = entity.DiaChiThuaDat,
                    ThoiHanThue = entity.ThoiHanThue,
                    DenNgayThue = entity.DenNgayThue != null ? entity.DenNgayThue.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    TuNgayThue = entity.TuNgayThue != null ? entity.TuNgayThue.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    TongDienTich = entity.TongDienTich.ToString("N", new CultureInfo("vi-VN")),
                };
                var listFileViewModel = new List<FileTaiLieuViewModel>();
                var listFile = _context.FileTaiLieu.Include(x => x.File).Where(x => x.IdLoaiTaiLieu == NhomLoaiTaiLieuConstant.NhomThongBaoTienThueDat && x.IdTaiLieu == entity.IdThongBaoTienThueDat && x.TrangThai != 4).ToList();
                foreach (var item in listFile)
                {
                    var fileViewModel = new FileTaiLieuViewModel();
                    fileViewModel.IdFileTaiLieu = item.IdFileTaiLieu;
                    fileViewModel.IdFile = item.IdFile;
                    fileViewModel.TenFile = item.File.TenFile;
                    fileViewModel.LinkFile = item.File.LinkFile;
                    fileViewModel.LoaiTaiLieu = item.LoaiTaiLieu;
                    fileViewModel.IdLoaiTaiLieu = item.IdLoaiTaiLieu;
                    fileViewModel.IdTaiLieu = item.IdTaiLieu;
                    listFileViewModel.Add(fileViewModel);
                }
                thongBaoTienThueDat.DsFileTaiLieu = listFileViewModel;
                result.Add(thongBaoTienThueDat);
            }
            return new ApiSuccessResult<List<ThongBaoTienThueDatViewModel>>() { Data = result };
        }
    }
}
