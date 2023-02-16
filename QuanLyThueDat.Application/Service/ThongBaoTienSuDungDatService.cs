using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using QuanLyThueDat.Application.Common.Constant;
using QuanLyThueDat.Application.Interfaces;
using QuanLyThueDat.Application.Request;
using QuanLyThueDat.Application.ViewModel;
using QuanLyThueDat.Data.EF;
using QuanLyThueDat.Data.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Application.Service
{
    public class ThongBaoTienSuDungDatService : IThongBaoTienSuDungDatService
    {
        private readonly QuanLyThueDatDbContext _context;
        public IHttpContextAccessor _accessor;
        public ThongBaoTienSuDungDatService(QuanLyThueDatDbContext context, IHttpContextAccessor HttpContextAccessor)
        {
            _context = context;
            _accessor = HttpContextAccessor;
        }
        public async Task<ApiResult<int>> InsertUpdate(ThongBaoTienSuDungDatRequest rq)
        {
            var claimsIdentity = _accessor.HttpContext.User.Identity as ClaimsIdentity;
            var tenUser = claimsIdentity.FindFirst("HoTen")?.Value;
            var userId = claimsIdentity.FindFirst("UserId")?.Value;
            var result = 0;
            var entity = _context.ThongBaoTienSuDungDat.FirstOrDefault(x => x.IdThongBaoTienSuDungDat == rq.IdThongBaoTienSuDungDat);
            if (entity == null)
            {
                entity = new ThongBaoTienSuDungDat()
                {
                    IdDoanhNghiep = rq.IdDoanhNghiep,
                    IdQuyetDinhThueDat = rq.IdQuyetDinhThueDat,
                    SoQuyetDinhThueDat = rq.SoQuyetDinhThueDat,
                    TenQuyetDinhThueDat = rq.TenQuyetDinhThueDat,
                    NgayQuyetDinhThueDat = string.IsNullOrEmpty(rq.NgayQuyetDinhThueDat) ? null : DateTime.Parse(rq.NgayQuyetDinhThueDat, new CultureInfo("vi-VN")),
                    ViTriThuaDat = rq.ViTriThuaDat,
                    MucDichSuDung = rq.MucDichSuDung,
                    DiaChiThuaDat = rq.DiaChiThuaDat,
                    ThoiHanThue = rq.ThoiHanThue,
                    TongDienTich = rq.TongDienTich,
                    TuNgayThue = string.IsNullOrEmpty(rq.TuNgayThue) ? null : DateTime.Parse(rq.TuNgayThue, new CultureInfo("vi-VN")),
                    DenNgayThue = string.IsNullOrEmpty(rq.DenNgayThue) ? null : DateTime.Parse(rq.DenNgayThue, new CultureInfo("vi-VN")),
                    SoThongBaoTienSuDungDat = rq.SoThongBaoTienSuDungDat,
                    TenThongBaoTienSuDungDat = rq.TenThongBaoTienSuDungDat,
                    LanThongBaoTienSuDungDat = rq.LanThongBaoTienSuDungDat,
                    DienTichKhongPhaiNop = rq.DienTichKhongPhaiNop,
                    DienTichPhaiNop = rq.DienTichPhaiNop,
                    DonGia = rq.DonGia,
                    ThoiHanDonGia = rq.ThoiHanDonGia,

                    SoTien = rq.SoTien,
                    SoTienBoiThuongGiaiPhongMatBang = rq.SoTienBoiThuongGiaiPhongMatBang,
                    LyDoBoiThuongGiaiPhongMatBang = rq.LyDoBoiThuongGiaiPhongMatBang,
                    SoTienMienGiam = rq.SoTienMienGiam,
                    LyDoMienGiam = rq.LyDoMienGiam,
                    SoTienPhaiNop = rq.SoTienPhaiNop,

                    NgayThongBaoTienSuDungDat = string.IsNullOrEmpty(rq.NgayThongBaoTienSuDungDat) ? null : DateTime.Parse(rq.NgayThongBaoTienSuDungDat, new CultureInfo("vi-VN")),
                    NgayHieuLucTienSuDungDat = string.IsNullOrEmpty(rq.NgayHieuLucTienSuDungDat) ? null : DateTime.Parse(rq.NgayHieuLucTienSuDungDat, new CultureInfo("vi-VN")),
                    NgayHetHieuLucTienSuDungDat = string.IsNullOrEmpty(rq.NgayHetHieuLucTienSuDungDat) ? null : DateTime.Parse(rq.NgayHetHieuLucTienSuDungDat, new CultureInfo("vi-VN")),
                    HinhThucThue = rq.HinhThucThue,
                    LanhDaoKyThongBaoTienSuDungDat = rq.LanhDaoKyThongBaoTienSuDungDat,
                    NgayTao = DateTime.Now,
                    NguoiTao = tenUser,
                    IdNguoiTao = userId
                };
            }
            else
            {
                entity.IdThongBaoTienSuDungDat = rq.IdThongBaoTienSuDungDat;
                entity.IdDoanhNghiep = rq.IdDoanhNghiep;
                entity.IdQuyetDinhThueDat = rq.IdQuyetDinhThueDat;
                entity.SoQuyetDinhThueDat = rq.SoQuyetDinhThueDat;
                entity.TenQuyetDinhThueDat = rq.TenQuyetDinhThueDat;
                entity.NgayQuyetDinhThueDat = string.IsNullOrEmpty(rq.NgayQuyetDinhThueDat) ? null : DateTime.Parse(rq.NgayQuyetDinhThueDat, new CultureInfo("vi-VN"));
                entity.ViTriThuaDat = rq.ViTriThuaDat;
                entity.MucDichSuDung = rq.MucDichSuDung;
                entity.DiaChiThuaDat = rq.DiaChiThuaDat;
                entity.ThoiHanThue = rq.ThoiHanThue;
                entity.TongDienTich = rq.TongDienTich;
                entity.TuNgayThue = string.IsNullOrEmpty(rq.TuNgayThue) ? null : DateTime.Parse(rq.TuNgayThue, new CultureInfo("vi-VN"));
                entity.DenNgayThue = string.IsNullOrEmpty(rq.DenNgayThue) ? null : DateTime.Parse(rq.DenNgayThue, new CultureInfo("vi-VN"));
                entity.SoThongBaoTienSuDungDat = rq.SoThongBaoTienSuDungDat;
                entity.TenThongBaoTienSuDungDat = rq.TenThongBaoTienSuDungDat;
                entity.LanThongBaoTienSuDungDat = rq.LanThongBaoTienSuDungDat;
                entity.DienTichKhongPhaiNop = rq.DienTichKhongPhaiNop;
                entity.DienTichPhaiNop = rq.DienTichPhaiNop;
                entity.DonGia = rq.DonGia;
                entity.ThoiHanDonGia = rq.ThoiHanDonGia;

                entity.SoTien = rq.SoTien;
                entity.SoTienBoiThuongGiaiPhongMatBang = rq.SoTienBoiThuongGiaiPhongMatBang;
                entity.LyDoBoiThuongGiaiPhongMatBang = rq.LyDoBoiThuongGiaiPhongMatBang;
                entity.SoTienMienGiam = rq.SoTienMienGiam;
                entity.LyDoMienGiam = rq.LyDoMienGiam;
                entity.SoTienPhaiNop = rq.SoTienPhaiNop;

                entity.NgayThongBaoTienSuDungDat = string.IsNullOrEmpty(rq.NgayThongBaoTienSuDungDat) ? null : DateTime.Parse(rq.NgayThongBaoTienSuDungDat, new CultureInfo("vi-VN"));
                entity.NgayHieuLucTienSuDungDat = string.IsNullOrEmpty(rq.NgayHieuLucTienSuDungDat) ? null : DateTime.Parse(rq.NgayHieuLucTienSuDungDat, new CultureInfo("vi-VN"));
                entity.NgayHetHieuLucTienSuDungDat = string.IsNullOrEmpty(rq.NgayHetHieuLucTienSuDungDat) ? null : DateTime.Parse(rq.NgayHetHieuLucTienSuDungDat, new CultureInfo("vi-VN"));
                entity.HinhThucThue = rq.HinhThucThue;
                entity.LanhDaoKyThongBaoTienSuDungDat = rq.LanhDaoKyThongBaoTienSuDungDat;
                entity.NgayCapNhat = DateTime.Now;
                entity.NguoiCapNhat = tenUser;
                entity.IdNguoiCapNhat = userId;
            }

            _context.ThongBaoTienSuDungDat.Update(entity);

            var listIdOldFile = rq.FileTaiLieu.Where(x => x.IdFileTaiLieu > 0).Select(x => x.IdFileTaiLieu);
            var listRemoveFile = _context.FileTaiLieu.Where(x => x.IdTaiLieu == entity.IdThongBaoTienSuDungDat && x.IdLoaiTaiLieu == NhomLoaiTaiLieuConstant.NhomThongBaoTienSuDungDat && !listIdOldFile.Contains(x.IdFileTaiLieu));
            foreach (var item in listRemoveFile)
            {
                item.TrangThai = 4;
            }
            _context.FileTaiLieu.UpdateRange(listRemoveFile);

            var listNewFile = rq.FileTaiLieu.Where(x => x.IdFileTaiLieu == 0);
            foreach (var item in listNewFile)
            {
                item.IdTaiLieu = entity.IdThongBaoTienSuDungDat;
                item.IdLoaiTaiLieu = NhomLoaiTaiLieuConstant.NhomThongBaoTienSuDungDat;
                item.TrangThai = 1;
                item.NgayTao = DateTime.Now;
            }
            _context.FileTaiLieu.AddRange(listNewFile);
            await _context.SaveChangesAsync();
            result = entity.IdThongBaoTienSuDungDat;
            return new ApiSuccessResult<int>() { Data = result };
        }

        public async Task<ApiResult<bool>> Delete(int idThongBaoTienSuDungDat)
        {
            var result = false;
            var data = _context.ThongBaoTienSuDungDat.FirstOrDefault(x => x.IdThongBaoTienSuDungDat == idThongBaoTienSuDungDat);
            if (data != null)
            {
                _context.ThongBaoTienSuDungDat.Remove(data);
                await _context.SaveChangesAsync();
                result = true;
                return new ApiSuccessResult<bool>() { Data = result };
            }
            else
            {
                result = false;
                return new ApiErrorResult<bool>() { Data = result };
            }

        }

        public async Task<ApiResult<List<ThongBaoTienSuDungDatViewModel>>> GetAll()
        {
            var result = new List<ThongBaoTienSuDungDatViewModel>();
            var data = await _context.ThongBaoTienSuDungDat.Include(x => x.DoanhNghiep).ToListAsync();
            foreach (var item in data)
            {
                var ThongBaoTienSuDungDat = new ThongBaoTienSuDungDatViewModel
                {
                    IdThongBaoTienSuDungDat = item.IdThongBaoTienSuDungDat,
                    IdDoanhNghiep = item.IdDoanhNghiep,
                    TenDoanhNghiep = item.DoanhNghiep.TenDoanhNghiep,
                    SoThongBaoTienSuDungDat = item.SoThongBaoTienSuDungDat,
                    TenThongBaoTienSuDungDat = item.TenThongBaoTienSuDungDat,
                    ViTriThuaDat = item.ViTriThuaDat,
                    DienTichKhongPhaiNop = item.DienTichKhongPhaiNop.ToString("N", new CultureInfo("vi-VN")),
                    DienTichPhaiNop = item.DienTichPhaiNop.ToString("N", new CultureInfo("vi-VN")),
                    DonGia = item.DonGia.ToString("N0", new CultureInfo("vi-VN")),
                    NgayThongBaoTienSuDungDat = item.NgayThongBaoTienSuDungDat != null ? item.NgayThongBaoTienSuDungDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    NgayHieuLucTienSuDungDat = item.NgayHieuLucTienSuDungDat != null ? item.NgayHieuLucTienSuDungDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    NgayHetHieuLucTienSuDungDat = item.NgayHetHieuLucTienSuDungDat != null ? item.NgayHetHieuLucTienSuDungDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                };
                result.Add(ThongBaoTienSuDungDat);
            }
            return new ApiSuccessResult<List<ThongBaoTienSuDungDatViewModel>>() { Data = result };
        }

        public async Task<ApiResult<PageViewModel<ThongBaoTienSuDungDatViewModel>>> GetAllPaging(int? idDoanhNghiep, string keyword, int pageIndex, int pageSize)
        {
            var query = from a in _context.ThongBaoTienSuDungDat.Include(x => x.DoanhNghiep)
                        select a;
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => (x.SoThongBaoTienSuDungDat.ToLower().Contains(keyword.ToLower())) || (x.DoanhNghiep.TenDoanhNghiep.ToLower().Contains(keyword.ToLower())) || (x.DoanhNghiep.MaSoThue.ToLower().Contains(keyword.ToLower())));
            }
            if (idDoanhNghiep != null)
            {
                query = query.Where(x => x.IdDoanhNghiep == idDoanhNghiep);
            }
            var data = await query.OrderByDescending(x => x.NgayThongBaoTienSuDungDat)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();
            var listItem = new List<ThongBaoTienSuDungDatViewModel>();
            foreach (var entity in data)
            {
                var x = entity.DoanhNghiep;
                var thongBaoTienSuDungDat = new ThongBaoTienSuDungDatViewModel
                {
                    IdThongBaoTienSuDungDat = entity.IdThongBaoTienSuDungDat,
                    IdDoanhNghiep = entity.IdDoanhNghiep,
                    TenDoanhNghiep = entity.DoanhNghiep.TenDoanhNghiep,
                    SoThongBaoTienSuDungDat = entity.SoThongBaoTienSuDungDat,
                    TenThongBaoTienSuDungDat = entity.TenThongBaoTienSuDungDat,
                    ViTriThuaDat = entity.ViTriThuaDat,
                    DienTichKhongPhaiNop = entity.DienTichKhongPhaiNop.ToString("N", new CultureInfo("vi-VN")),
                    DienTichPhaiNop = entity.DienTichPhaiNop.ToString("N", new CultureInfo("vi-VN")),
                    DonGia = entity.DonGia.ToString("N0", new CultureInfo("vi-VN")),
                    SoTienPhaiNop = entity.SoTienPhaiNop.ToString("N0", new CultureInfo("vi-VN")),
                    NgayThongBaoTienSuDungDat = entity.NgayThongBaoTienSuDungDat != null ? entity.NgayThongBaoTienSuDungDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    NgayHieuLucTienSuDungDat = entity.NgayHieuLucTienSuDungDat != null ? entity.NgayHieuLucTienSuDungDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    NgayHetHieuLucTienSuDungDat = entity.NgayHetHieuLucTienSuDungDat != null ? entity.NgayHetHieuLucTienSuDungDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                };
                var listFileViewModel = new List<FileTaiLieuViewModel>();
                var listFile = _context.FileTaiLieu.Include(x => x.File).Where(x => x.IdLoaiTaiLieu == NhomLoaiTaiLieuConstant.NhomThongBaoTienSuDungDat && x.IdTaiLieu == entity.IdThongBaoTienSuDungDat && x.TrangThai != 4).ToList();
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
                thongBaoTienSuDungDat.DsFileTaiLieu = listFileViewModel;
                listItem.Add(thongBaoTienSuDungDat);
            }
            var result = new PageViewModel<ThongBaoTienSuDungDatViewModel>()
            {
                Items = listItem,
                PageIndex = pageIndex,
                TotalRecord = query.Count(),
                PageSize = pageSize
            };
            return new ApiSuccessResult<PageViewModel<ThongBaoTienSuDungDatViewModel>>() { Data = result };
        }

        public async Task<ApiResult<ThongBaoTienSuDungDatViewModel>> GetById(int idThongBaoTienSuDungDat)
        {
            var result = new ThongBaoTienSuDungDatViewModel();
            var entity = await _context.ThongBaoTienSuDungDat.Include(x => x.DoanhNghiep).FirstOrDefaultAsync(x => x.IdThongBaoTienSuDungDat == idThongBaoTienSuDungDat);
            if (entity != null)
            {
                result = new ThongBaoTienSuDungDatViewModel
                {
                    IdThongBaoTienSuDungDat = entity.IdThongBaoTienSuDungDat,
                    IdDoanhNghiep = entity.IdDoanhNghiep,
                    TenDoanhNghiep = entity.DoanhNghiep.TenDoanhNghiep,
                    MaSoThue = entity.DoanhNghiep.MaSoThue,
                    CoQuanQuanLyThue = entity.DoanhNghiep.CoQuanQuanLyThue,
                    DiaChi = entity.DoanhNghiep.DiaChi,
                    SoDienThoai = entity.DoanhNghiep.SoDienThoai,
                    Email = entity.DoanhNghiep.Email,
                    IdQuyetDinhThueDat = entity.IdQuyetDinhThueDat,
                    SoQuyetDinhThueDat = entity.SoQuyetDinhThueDat,
                    TenQuyetDinhThueDat = entity.TenQuyetDinhThueDat,
                    NgayQuyetDinhThueDat = entity.NgayQuyetDinhThueDat != null ? entity.NgayQuyetDinhThueDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    ViTriThuaDat = entity.ViTriThuaDat,
                    MucDichSuDung = entity.MucDichSuDung,
                    DiaChiThuaDat = entity.DiaChiThuaDat,
                    ThoiHanThue = entity.ThoiHanThue,
                    TongDienTich = entity.TongDienTich.ToString("N", new CultureInfo("vi-VN")),
                    TuNgayThue = entity.TuNgayThue != null ? entity.TuNgayThue.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    DenNgayThue = entity.DenNgayThue != null ? entity.DenNgayThue.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    SoThongBaoTienSuDungDat = entity.SoThongBaoTienSuDungDat,
                    TenThongBaoTienSuDungDat = entity.TenThongBaoTienSuDungDat,
                    LanThongBaoTienSuDungDat = entity.LanThongBaoTienSuDungDat,
                    DienTichKhongPhaiNop = entity.DienTichKhongPhaiNop.ToString("N", new CultureInfo("vi-VN")),
                    DienTichPhaiNop = entity.DienTichPhaiNop.ToString("N", new CultureInfo("vi-VN")),
                    DonGia = entity.DonGia.ToString("N0", new CultureInfo("vi-VN")),
                    ThoiHanDonGia = entity.ThoiHanDonGia,

                    SoTien = entity.SoTien.ToString("N0", new CultureInfo("vi-VN")),
                    SoTienBoiThuongGiaiPhongMatBang = entity.SoTienBoiThuongGiaiPhongMatBang.ToString("N0", new CultureInfo("vi-VN")),
                    LyDoBoiThuongGiaiPhongMatBang = entity.LyDoBoiThuongGiaiPhongMatBang,
                    SoTienMienGiam = entity.SoTienMienGiam.ToString("N0", new CultureInfo("vi-VN")),
                    LyDoMienGiam = entity.LyDoMienGiam,
                    SoTienPhaiNop = entity.SoTienPhaiNop.ToString("N0", new CultureInfo("vi-VN")),

                    NgayThongBaoTienSuDungDat = entity.NgayThongBaoTienSuDungDat != null ? entity.NgayThongBaoTienSuDungDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    NgayHieuLucTienSuDungDat = entity.NgayHieuLucTienSuDungDat != null ? entity.NgayHieuLucTienSuDungDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    NgayHetHieuLucTienSuDungDat = entity.NgayHetHieuLucTienSuDungDat != null ? entity.NgayHetHieuLucTienSuDungDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    HinhThucThue = entity.HinhThucThue,
                    LanhDaoKyThongBaoTienSuDungDat = entity.LanhDaoKyThongBaoTienSuDungDat
                };
                var listFileViewModel = new List<FileTaiLieuViewModel>();
                var listFile = _context.FileTaiLieu.Include(x => x.File).Where(x => x.IdLoaiTaiLieu == NhomLoaiTaiLieuConstant.NhomThongBaoTienSuDungDat && x.IdTaiLieu == entity.IdThongBaoTienSuDungDat && x.TrangThai != 4).ToList();
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
                result.DsFileTaiLieu = listFileViewModel;
                return new ApiSuccessResult<ThongBaoTienSuDungDatViewModel>() { Data = result };

            }
            else
            {
                return new ApiErrorResult<ThongBaoTienSuDungDatViewModel>("Không tìm thấy dữ liệu");
            }
        }

        public async Task<ApiResult<List<ThongBaoTienSuDungDatViewModel>>> GetAllByRequest(ThongBaoTienSuDungDatRequest rq)
        {
            var result = new List<ThongBaoTienSuDungDatViewModel>();
            var query = from a in _context.ThongBaoTienSuDungDat.Include(x => x.DoanhNghiep)
                        select a;
            if (rq.IdDoanhNghiep != 0)
            {
                query = query.Where(x => x.IdDoanhNghiep == rq.IdDoanhNghiep);
            }
            if (rq.IdQuyetDinhThueDat != 0)
            {
                query = query.Where(x => x.IdQuyetDinhThueDat == rq.IdQuyetDinhThueDat || x.IdQuyetDinhThueDat == null);
            }
            var data = await query.OrderByDescending(x => x.NgayThongBaoTienSuDungDat).ToListAsync();
            foreach (var item in data)
            {
                var ThongBaoTienSuDungDat = new ThongBaoTienSuDungDatViewModel
                {
                    IdThongBaoTienSuDungDat = item.IdThongBaoTienSuDungDat,
                    IdDoanhNghiep = item.IdDoanhNghiep,
                    TenDoanhNghiep = item.DoanhNghiep.TenDoanhNghiep,
                    SoThongBaoTienSuDungDat = item.SoThongBaoTienSuDungDat,
                    TenThongBaoTienSuDungDat = item.TenThongBaoTienSuDungDat,
                    ViTriThuaDat = item.ViTriThuaDat,
                    DienTichKhongPhaiNop = item.DienTichKhongPhaiNop.ToString("N", new CultureInfo("vi-VN")),
                    DienTichPhaiNop = item.DienTichPhaiNop.ToString("N", new CultureInfo("vi-VN")),
                    DonGia = item.DonGia.ToString("N0", new CultureInfo("vi-VN")),
                    NgayThongBaoTienSuDungDat = item.NgayThongBaoTienSuDungDat != null ? item.NgayThongBaoTienSuDungDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    NgayHieuLucTienSuDungDat = item.NgayHieuLucTienSuDungDat != null ? item.NgayHieuLucTienSuDungDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    NgayHetHieuLucTienSuDungDat = item.NgayHetHieuLucTienSuDungDat != null ? item.NgayHetHieuLucTienSuDungDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                };
                result.Add(ThongBaoTienSuDungDat);
            }
            return new ApiSuccessResult<List<ThongBaoTienSuDungDatViewModel>>()
            {
                Data = result
            };
        }
    }
}
