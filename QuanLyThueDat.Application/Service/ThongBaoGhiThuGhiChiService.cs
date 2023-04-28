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
    public class ThongBaoGhiThuGhiChiService : IThongBaoGhiThuGhiChiService
    {
        private readonly QuanLyThueDatDbContext _context;
        public IHttpContextAccessor _accessor;
        private readonly IQuyetDinhThueDatService _quyetDinhThueDatService;
        public ThongBaoGhiThuGhiChiService(QuanLyThueDatDbContext context, IHttpContextAccessor HttpContextAccessor, IQuyetDinhThueDatService QuyetDinhThueDatService)
        {
            _context = context;
            _accessor = HttpContextAccessor;
            _quyetDinhThueDatService = QuyetDinhThueDatService;

        }
        public async Task<ApiResult<int>> InsertUpdate(ThongBaoGhiThuGhiChiRequest rq)
        {
            var claimsIdentity = _accessor.HttpContext.User.Identity as ClaimsIdentity;
            var tenUser = claimsIdentity.FindFirst("HoTen")?.Value;
            var userId = claimsIdentity.FindFirst("UserId")?.Value;
            var result = 0;
            var entity = _context.ThongBaoGhiThuGhiChi.FirstOrDefault(x => x.IdThongBaoGhiThuGhiChi == rq.IdThongBaoGhiThuGhiChi);
            if (entity == null)
            {
                entity = new ThongBaoGhiThuGhiChi()
                {
                    IdDoanhNghiep = rq.IdDoanhNghiep,
                    IdQuyetDinhThueDat = rq.IdQuyetDinhThueDat,
                    SoThongBaoGhiThuGhiChi = rq.SoThongBaoGhiThuGhiChi,
                    SoTienGhiChi = rq.SoTienGhiChi,
                    SoTienGhiThu = rq.SoTienGhiThu,
                    NoiDungGhiThu = rq.NoiDungGhiThu,
                    NoiDungGhiChi = rq.NoiDungGhiChi,
                    NgayThongBaoGhiThuGhiChi = string.IsNullOrEmpty(rq.NgayThongBaoGhiThuGhiChi) ? null : DateTime.Parse(rq.NgayThongBaoGhiThuGhiChi, new CultureInfo("vi-VN")),
                    NgayTao = DateTime.Now,
                    NguoiTao = tenUser,
                    IdNguoiTao = userId,
                    GhiChu = rq.GhiChu,
                };
            }
            else
            {
                entity.IdThongBaoGhiThuGhiChi = rq.IdThongBaoGhiThuGhiChi;
                entity.IdDoanhNghiep = rq.IdDoanhNghiep;
                entity.IdQuyetDinhThueDat = rq.IdQuyetDinhThueDat;
                entity.SoThongBaoGhiThuGhiChi = rq.SoThongBaoGhiThuGhiChi;
                entity.SoTienGhiChi = rq.SoTienGhiChi;
                entity.SoTienGhiThu = rq.SoTienGhiThu;
                entity.NoiDungGhiThu = rq.NoiDungGhiThu;
                entity.NoiDungGhiChi = rq.NoiDungGhiChi;
                entity.NgayThongBaoGhiThuGhiChi = string.IsNullOrEmpty(rq.NgayThongBaoGhiThuGhiChi) ? null : DateTime.Parse(rq.NgayThongBaoGhiThuGhiChi, new CultureInfo("vi-VN"));
                entity.NgayCapNhat = DateTime.Now;
                entity.NguoiCapNhat = tenUser;
                entity.IdNguoiCapNhat = userId;
                entity.GhiChu = rq.GhiChu;
            }

            _context.ThongBaoGhiThuGhiChi.Update(entity);
            await _context.SaveChangesAsync();
            var listIdOldFile = rq.FileTaiLieu.Where(x => x.IdFileTaiLieu > 0).Select(x => x.IdFileTaiLieu);
            var listRemoveFile = _context.FileTaiLieu.Where(x => x.IdTaiLieu == entity.IdThongBaoGhiThuGhiChi && x.IdLoaiTaiLieu == NhomLoaiTaiLieuConstant.NhomThongBaoGhiThuGhiChi && !listIdOldFile.Contains(x.IdFileTaiLieu));
            foreach (var item in listRemoveFile)
            {
                item.TrangThai = 4;
            }
            _context.FileTaiLieu.UpdateRange(listRemoveFile);

            var listNewFile = rq.FileTaiLieu.Where(x => x.IdFileTaiLieu == 0);
            foreach (var item in listNewFile)
            {
                item.IdTaiLieu = entity.IdThongBaoGhiThuGhiChi;
                item.IdLoaiTaiLieu = NhomLoaiTaiLieuConstant.NhomThongBaoGhiThuGhiChi;
                item.TrangThai = 1;
                item.NgayTao = DateTime.Now;
            }
            _context.FileTaiLieu.AddRange(listNewFile);
            await _context.SaveChangesAsync();
            result = entity.IdThongBaoGhiThuGhiChi;
            return new ApiSuccessResult<int>() { Data = result };
        }

        public async Task<ApiResult<bool>> Delete(int idThongBaoGhiThuGhiChi)
        {
            var result = false;
            var data = _context.ThongBaoGhiThuGhiChi.FirstOrDefault(x => x.IdThongBaoGhiThuGhiChi == idThongBaoGhiThuGhiChi);
            if (data != null)
            {
                data.IsDeleted = true;
                //_context.ThongBaoGhiThuGhiChi.Remove(data);
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

        public async Task<ApiResult<List<ThongBaoGhiThuGhiChiViewModel>>> GetAll(int? idDoanhNghiep)
        {
            var result = new List<ThongBaoGhiThuGhiChiViewModel>();
            var data = new List<ThongBaoGhiThuGhiChi>();
            if (idDoanhNghiep == null)
            {
                data = await _context.ThongBaoGhiThuGhiChi.Include(x => x.DoanhNghiep).Where(x => !x.IsDeleted).ToListAsync();
            }
            else
            {
                data = await _context.ThongBaoGhiThuGhiChi.Include(x => x.DoanhNghiep).Where(x => !x.IsDeleted).Where(x => x.IdDoanhNghiep == idDoanhNghiep).ToListAsync();
            }
            foreach (var item in data)
            {
                var ThongBaoGhiThuGhiChi = new ThongBaoGhiThuGhiChiViewModel
                {
                    IdThongBaoGhiThuGhiChi = item.IdThongBaoGhiThuGhiChi,
                    IdDoanhNghiep = item.IdDoanhNghiep,
                    IdQuyetDinhThueDat = item.IdQuyetDinhThueDat,
                    SoThongBaoGhiThuGhiChi = item.SoThongBaoGhiThuGhiChi,
                    SoTienGhiChi = item.SoTienGhiChi.ToString("N0", new CultureInfo("vi-VN")),
                    SoTienGhiThu = item.SoTienGhiThu.ToString("N0", new CultureInfo("vi-VN")),
                    NoiDungGhiThu = item.NoiDungGhiThu,
                    NoiDungGhiChi = item.NoiDungGhiChi,
                    NgayThongBaoGhiThuGhiChi = item.NgayThongBaoGhiThuGhiChi != null ? item.NgayThongBaoGhiThuGhiChi.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    GhiChu = item.GhiChu,
                };
                result.Add(ThongBaoGhiThuGhiChi);
            }
            return new ApiSuccessResult<List<ThongBaoGhiThuGhiChiViewModel>>() { Data = result };
        }

        public async Task<ApiResult<PageViewModel<ThongBaoGhiThuGhiChiViewModel>>> GetAllPaging(int? idDoanhNghiep, string keyword, int pageIndex, int pageSize)
        {
            var query = from a in _context.ThongBaoGhiThuGhiChi.Include(x => x.DoanhNghiep).Include(x => x.DoanhNghiep).Where(x => !x.IsDeleted)
                        select a;
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => (x.SoThongBaoGhiThuGhiChi.ToLower().Contains(keyword.ToLower())) || (x.DoanhNghiep.TenDoanhNghiep.ToLower().Contains(keyword.ToLower())) || (x.DoanhNghiep.MaSoThue.ToLower().Contains(keyword.ToLower())));
            }
            if (idDoanhNghiep != null)
            {
                query = query.Where(x => x.IdDoanhNghiep == idDoanhNghiep);
            }
            var data = await query.OrderByDescending(x => x.NgayThongBaoGhiThuGhiChi)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();
            var listItem = new List<ThongBaoGhiThuGhiChiViewModel>();
            foreach (var entity in data)
            {
                var x = entity.DoanhNghiep;
                var ThongBaoGhiThuGhiChi = new ThongBaoGhiThuGhiChiViewModel
                {
                    IdThongBaoGhiThuGhiChi = entity.IdThongBaoGhiThuGhiChi,
                    IdDoanhNghiep = entity.IdDoanhNghiep,
                    IdQuyetDinhThueDat = entity.IdQuyetDinhThueDat,
                    SoThongBaoGhiThuGhiChi = entity.SoThongBaoGhiThuGhiChi,
                    SoTienGhiChi = entity.SoTienGhiChi.ToString("N0", new CultureInfo("vi-VN")),
                    SoTienGhiThu = entity.SoTienGhiThu.ToString("N0", new CultureInfo("vi-VN")),
                    NoiDungGhiThu = entity.NoiDungGhiThu,
                    NoiDungGhiChi = entity.NoiDungGhiChi,
                    NgayThongBaoGhiThuGhiChi = entity.NgayThongBaoGhiThuGhiChi != null ? entity.NgayThongBaoGhiThuGhiChi.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    GhiChu = entity.GhiChu,
                };
                if (entity.IdQuyetDinhThueDat != null)
                {
                    ThongBaoGhiThuGhiChi.QuyenDuLieu = _quyetDinhThueDatService.CheckQuyenDuLieu(entity.IdQuyetDinhThueDat.Value);
                }
                var listFileViewModel = new List<FileTaiLieuViewModel>();
                var listFile = _context.FileTaiLieu.Include(x => x.File).Where(x => x.IdLoaiTaiLieu == NhomLoaiTaiLieuConstant.NhomThongBaoGhiThuGhiChi && x.IdTaiLieu == entity.IdThongBaoGhiThuGhiChi && x.TrangThai != 4).ToList();
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
                ThongBaoGhiThuGhiChi.DsFileTaiLieu = listFileViewModel;
                listItem.Add(ThongBaoGhiThuGhiChi);
            }
            var result = new PageViewModel<ThongBaoGhiThuGhiChiViewModel>()
            {
                Items = listItem,
                PageIndex = pageIndex,
                TotalRecord = query.Count(),
                PageSize = pageSize
            };
            return new ApiSuccessResult<PageViewModel<ThongBaoGhiThuGhiChiViewModel>>() { Data = result };
        }

        public async Task<ApiResult<ThongBaoGhiThuGhiChiViewModel>> GetById(int idThongBaoGhiThuGhiChi)
        {
            var result = new ThongBaoGhiThuGhiChiViewModel();
            var entity = await _context.ThongBaoGhiThuGhiChi.Include(x => x.DoanhNghiep).FirstOrDefaultAsync(x => x.IdThongBaoGhiThuGhiChi == idThongBaoGhiThuGhiChi);
            if (entity != null)
            {
                result = new ThongBaoGhiThuGhiChiViewModel
                {
                    IdThongBaoGhiThuGhiChi = entity.IdThongBaoGhiThuGhiChi,
                    IdDoanhNghiep = entity.IdDoanhNghiep,
                    IdQuyetDinhThueDat = entity.IdQuyetDinhThueDat,
                    TenDoanhNghiep = entity.DoanhNghiep.TenDoanhNghiep,
                    SoThongBaoGhiThuGhiChi = entity.SoThongBaoGhiThuGhiChi,
                    SoTienGhiChi = entity.SoTienGhiChi.ToString("N0", new CultureInfo("vi-VN")),
                    SoTienGhiThu = entity.SoTienGhiThu.ToString("N0", new CultureInfo("vi-VN")),
                    NoiDungGhiThu = entity.NoiDungGhiThu,
                    NoiDungGhiChi = entity.NoiDungGhiChi,
                    NgayThongBaoGhiThuGhiChi = entity.NgayThongBaoGhiThuGhiChi != null ? entity.NgayThongBaoGhiThuGhiChi.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    GhiChu = entity.GhiChu,
                };
                if (entity.IdQuyetDinhThueDat != null)
                {
                    var quyetDinhThueDat = await _quyetDinhThueDatService.GetById(entity.IdQuyetDinhThueDat.Value);
                    result.SoQuyetDinhThueDat = quyetDinhThueDat.Data.SoQuyetDinhThueDat + " - ngày " + quyetDinhThueDat.Data.NgayQuyetDinhThueDat;
                    result.QuyenDuLieu = _quyetDinhThueDatService.CheckQuyenDuLieu(entity.IdQuyetDinhThueDat.Value);
                }
                var listFileViewModel = new List<FileTaiLieuViewModel>();
                var listFile = _context.FileTaiLieu.Include(x => x.File).Where(x => x.IdLoaiTaiLieu == NhomLoaiTaiLieuConstant.NhomThongBaoGhiThuGhiChi && x.IdTaiLieu == entity.IdThongBaoGhiThuGhiChi && x.TrangThai != 4).ToList();
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
                return new ApiSuccessResult<ThongBaoGhiThuGhiChiViewModel>() { Data = result };

            }
            else
            {
                return new ApiErrorResult<ThongBaoGhiThuGhiChiViewModel>("Không tìm thấy dữ liệu");
            }
        }
    }
}
