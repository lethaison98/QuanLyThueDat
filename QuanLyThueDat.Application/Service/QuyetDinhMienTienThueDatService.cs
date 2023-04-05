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
    public class QuyetDinhMienTienThueDatService : IQuyetDinhMienTienThueDatService
    {
        private readonly QuanLyThueDatDbContext _context;
        public IHttpContextAccessor _accessor;
        private readonly IQuyetDinhThueDatService _quyetDinhThueDatService;
        public QuyetDinhMienTienThueDatService(QuanLyThueDatDbContext context, IHttpContextAccessor HttpContextAccessor, IQuyetDinhThueDatService QuyetDinhThueDatService)
        {
            _context = context;
            _accessor = HttpContextAccessor;
            _quyetDinhThueDatService = QuyetDinhThueDatService;

        }
        public async Task<ApiResult<int>> InsertUpdate(QuyetDinhMienTienThueDatRequest rq)
        {
            var claimsIdentity = _accessor.HttpContext.User.Identity as ClaimsIdentity;
            var tenUser = claimsIdentity.FindFirst("HoTen")?.Value;
            var userId = claimsIdentity.FindFirst("UserId")?.Value;
            var result = 0;
            var entity = _context.QuyetDinhMienTienThueDat.FirstOrDefault(x => x.IdQuyetDinhMienTienThueDat == rq.IdQuyetDinhMienTienThueDat);
            if (entity == null)
            {
                entity = new QuyetDinhMienTienThueDat()
                {
                    IdQuyetDinhMienTienThueDat = rq.IdQuyetDinhMienTienThueDat,
                    IdQuyetDinhThueDat = rq.IdQuyetDinhMienTienThueDat,
                    IdDoanhNghiep = rq.IdDoanhNghiep,
                    SoQuyetDinhMienTienThueDat = rq.SoQuyetDinhMienTienThueDat,
                    TenQuyetDinhMienTienThueDat = rq.TenQuyetDinhMienTienThueDat,
                    NgayQuyetDinhMienTienThueDat = string.IsNullOrEmpty(rq.NgayQuyetDinhMienTienThueDat) ? null : DateTime.Parse(rq.NgayQuyetDinhMienTienThueDat, new CultureInfo("vi-VN")),
                    DienTichMienTienThueDat = rq.DienTichMienTienThueDat,
                    ThoiHanMienTienThueDat = rq.ThoiHanMienTienThueDat,
                    NgayHieuLucMienTienThueDat = string.IsNullOrEmpty(rq.NgayHieuLucMienTienThueDat) ? null : DateTime.Parse(rq.NgayHieuLucMienTienThueDat, new CultureInfo("vi-VN")),
                    NgayHetHieuLucMienTienThueDat = string.IsNullOrEmpty(rq.NgayHetHieuLucMienTienThueDat) ? null : DateTime.Parse(rq.NgayHetHieuLucMienTienThueDat, new CultureInfo("vi-VN")),
                    NgayTao = DateTime.Now,
                    NguoiTao = tenUser,
                    IdNguoiTao = userId
                };
            }
            else
            {
                entity.IdQuyetDinhMienTienThueDat = rq.IdQuyetDinhMienTienThueDat;
                entity.IdDoanhNghiep = rq.IdDoanhNghiep;
                entity.IdQuyetDinhThueDat = rq.IdQuyetDinhThueDat;
                entity.SoQuyetDinhMienTienThueDat = rq.SoQuyetDinhMienTienThueDat;
                entity.TenQuyetDinhMienTienThueDat = rq.TenQuyetDinhMienTienThueDat;
                entity.NgayQuyetDinhMienTienThueDat = string.IsNullOrEmpty(rq.NgayQuyetDinhMienTienThueDat) ? null : DateTime.Parse(rq.NgayQuyetDinhMienTienThueDat, new CultureInfo("vi-VN"));
                entity.DienTichMienTienThueDat = rq.DienTichMienTienThueDat;
                entity.ThoiHanMienTienThueDat = rq.ThoiHanMienTienThueDat;
                entity.NgayHieuLucMienTienThueDat = string.IsNullOrEmpty(rq.NgayHieuLucMienTienThueDat) ? null : DateTime.Parse(rq.NgayHieuLucMienTienThueDat, new CultureInfo("vi-VN"));
                entity.NgayHetHieuLucMienTienThueDat = string.IsNullOrEmpty(rq.NgayHetHieuLucMienTienThueDat) ? null : DateTime.Parse(rq.NgayHetHieuLucMienTienThueDat, new CultureInfo("vi-VN"));
                entity.NgayCapNhat = DateTime.Now;
                entity.NguoiCapNhat = tenUser;
                entity.IdNguoiCapNhat = userId;
            }

            _context.QuyetDinhMienTienThueDat.Update(entity);
            await _context.SaveChangesAsync();
            var listIdOldFile = rq.FileTaiLieu.Where(x => x.IdFileTaiLieu > 0).Select(x => x.IdFileTaiLieu);
            var listRemoveFile = _context.FileTaiLieu.Where(x => x.IdTaiLieu == entity.IdQuyetDinhMienTienThueDat && x.IdLoaiTaiLieu == NhomLoaiTaiLieuConstant.NhomQuyetDinhMienTienThueDat && !listIdOldFile.Contains(x.IdFileTaiLieu));
            foreach (var item in listRemoveFile)
            {
                item.TrangThai = 4;
            }
            _context.FileTaiLieu.UpdateRange(listRemoveFile);

            var listNewFile = rq.FileTaiLieu.Where(x => x.IdFileTaiLieu == 0);
            foreach (var item in listNewFile)
            {
                item.IdTaiLieu = entity.IdQuyetDinhMienTienThueDat;
                item.IdLoaiTaiLieu = NhomLoaiTaiLieuConstant.NhomQuyetDinhMienTienThueDat;
                item.TrangThai = 1;
                item.NgayTao = DateTime.Now;
            }
            _context.FileTaiLieu.AddRange(listNewFile);
            await _context.SaveChangesAsync();
            result = entity.IdQuyetDinhMienTienThueDat;
            return new ApiSuccessResult<int>() { Data = result };
        }

        public async Task<ApiResult<bool>> Delete(int idQuyetDinhMienTienThueDat)
        {
            var result = false;
            var data = _context.QuyetDinhMienTienThueDat.FirstOrDefault(x => x.IdQuyetDinhMienTienThueDat == idQuyetDinhMienTienThueDat);
            if (data != null)
            {
                _context.QuyetDinhMienTienThueDat.Remove(data);
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

        public async Task<ApiResult<List<QuyetDinhMienTienThueDatViewModel>>> GetAll(int? idDoanhNghiep)
        {
            var result = new List<QuyetDinhMienTienThueDatViewModel>();
            var data = new List<QuyetDinhMienTienThueDat>();
            if (idDoanhNghiep == null)
            {
                data = await _context.QuyetDinhMienTienThueDat.Include(x => x.DoanhNghiep).ToListAsync();
            }
            else
            {
                data = await _context.QuyetDinhMienTienThueDat.Include(x => x.DoanhNghiep).Where(x => x.IdDoanhNghiep == idDoanhNghiep).ToListAsync();
            }

            foreach (var item in data)
            {
                var QuyetDinhMienTienThueDat = new QuyetDinhMienTienThueDatViewModel
                {
                    IdQuyetDinhMienTienThueDat = item.IdQuyetDinhMienTienThueDat,
                    IdDoanhNghiep = item.IdDoanhNghiep,
                    IdQuyetDinhThueDat = item.IdQuyetDinhThueDat,
                    TenDoanhNghiep = item.DoanhNghiep.TenDoanhNghiep,
                    SoQuyetDinhMienTienThueDat = item.SoQuyetDinhMienTienThueDat,
                    TenQuyetDinhMienTienThueDat = item.TenQuyetDinhMienTienThueDat,
                    NgayQuyetDinhMienTienThueDat = item.NgayQuyetDinhMienTienThueDat != null ? item.NgayQuyetDinhMienTienThueDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    DienTichMienTienThueDat = item.DienTichMienTienThueDat.ToString("N", new CultureInfo("vi-VN")),
                    ThoiHanMienTienThueDat = item.ThoiHanMienTienThueDat,
                    NgayHieuLucMienTienThueDat = item.NgayHieuLucMienTienThueDat != null ? item.NgayHieuLucMienTienThueDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    NgayHetHieuLucMienTienThueDat = item.NgayHetHieuLucMienTienThueDat != null ? item.NgayHetHieuLucMienTienThueDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                };
                result.Add(QuyetDinhMienTienThueDat);
            }
            return new ApiSuccessResult<List<QuyetDinhMienTienThueDatViewModel>>() { Data = result };
        }

        public async Task<ApiResult<PageViewModel<QuyetDinhMienTienThueDatViewModel>>> GetAllPaging(int? idDoanhNghiep, string keyword, int pageIndex, int pageSize)
        {
            var query = from a in _context.QuyetDinhMienTienThueDat.Include(x => x.DoanhNghiep)
                        select a;
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.TenQuyetDinhMienTienThueDat.ToLower().Contains(keyword.ToLower()) || x.DoanhNghiep.MaSoThue.ToLower().Contains(keyword.ToLower()) || x.DoanhNghiep.TenDoanhNghiep.ToLower().Contains(keyword.ToLower()));
            }
            if (idDoanhNghiep != null)
            {
                query = query.Where(x => x.IdDoanhNghiep == idDoanhNghiep);
            }
            var data = await query.OrderByDescending(x => x.TenQuyetDinhMienTienThueDat)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();
            var listItem = new List<QuyetDinhMienTienThueDatViewModel>();
            foreach (var entity in data)
            {
                var quyetDinhMienTienThueDat = new QuyetDinhMienTienThueDatViewModel
                {
                    IdQuyetDinhMienTienThueDat = entity.IdQuyetDinhMienTienThueDat,
                    IdDoanhNghiep = entity.IdDoanhNghiep,
                    IdQuyetDinhThueDat = entity.IdQuyetDinhThueDat,
                    TenDoanhNghiep = entity.DoanhNghiep.TenDoanhNghiep,
                    MaSoThue = entity.DoanhNghiep.MaSoThue,
                    SoQuyetDinhMienTienThueDat = entity.SoQuyetDinhMienTienThueDat,
                    TenQuyetDinhMienTienThueDat = entity.TenQuyetDinhMienTienThueDat,
                    NgayQuyetDinhMienTienThueDat = entity.NgayQuyetDinhMienTienThueDat != null ? entity.NgayQuyetDinhMienTienThueDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    DienTichMienTienThueDat = entity.DienTichMienTienThueDat.ToString("N", new CultureInfo("vi-VN")),
                    ThoiHanMienTienThueDat = entity.ThoiHanMienTienThueDat,
                    NgayHieuLucMienTienThueDat = entity.NgayHieuLucMienTienThueDat != null ? entity.NgayHieuLucMienTienThueDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    NgayHetHieuLucMienTienThueDat = entity.NgayHetHieuLucMienTienThueDat != null ? entity.NgayHetHieuLucMienTienThueDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                };
                if (entity.IdQuyetDinhThueDat != null)
                {
                    quyetDinhMienTienThueDat.QuyenDuLieu = _quyetDinhThueDatService.CheckQuyenDuLieu(entity.IdQuyetDinhThueDat.Value);
                }
                var listFileViewModel = new List<FileTaiLieuViewModel>();
                var listFile = _context.FileTaiLieu.Include(x => x.File).Where(x => x.IdLoaiTaiLieu == NhomLoaiTaiLieuConstant.NhomQuyetDinhMienTienThueDat && x.IdTaiLieu == entity.IdQuyetDinhMienTienThueDat && x.TrangThai != 4).ToList();
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
                quyetDinhMienTienThueDat.DsFileTaiLieu = listFileViewModel;
                listItem.Add(quyetDinhMienTienThueDat);
            }
            var result = new PageViewModel<QuyetDinhMienTienThueDatViewModel>()
            {
                Items = listItem,
                PageIndex = pageIndex,
                TotalRecord = query.Count(),
                PageSize = pageSize
            };
            return new ApiSuccessResult<PageViewModel<QuyetDinhMienTienThueDatViewModel>>() { Data = result };
        }

        public async Task<ApiResult<QuyetDinhMienTienThueDatViewModel>> GetById(int idQuyetDinhMienTienThueDat)
        {
            var result = new QuyetDinhMienTienThueDatViewModel();
            var entity = await _context.QuyetDinhMienTienThueDat.Include(x => x.DoanhNghiep).FirstOrDefaultAsync(x => x.IdQuyetDinhMienTienThueDat == idQuyetDinhMienTienThueDat);
            if (entity != null)
            {
                result = new QuyetDinhMienTienThueDatViewModel
                {
                    IdQuyetDinhMienTienThueDat = entity.IdQuyetDinhMienTienThueDat,
                    IdDoanhNghiep = entity.IdDoanhNghiep,
                    TenDoanhNghiep = entity.DoanhNghiep.TenDoanhNghiep,
                    MaSoThue = entity.DoanhNghiep.MaSoThue,
                    SoQuyetDinhMienTienThueDat = entity.SoQuyetDinhMienTienThueDat,
                    TenQuyetDinhMienTienThueDat = entity.TenQuyetDinhMienTienThueDat,
                    NgayQuyetDinhMienTienThueDat = entity.NgayQuyetDinhMienTienThueDat != null ? entity.NgayQuyetDinhMienTienThueDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    DienTichMienTienThueDat = entity.DienTichMienTienThueDat.ToString("N", new CultureInfo("vi-VN")),
                    ThoiHanMienTienThueDat = entity.ThoiHanMienTienThueDat,
                    NgayHieuLucMienTienThueDat = entity.NgayHieuLucMienTienThueDat != null ? entity.NgayHieuLucMienTienThueDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    NgayHetHieuLucMienTienThueDat = entity.NgayHetHieuLucMienTienThueDat != null ? entity.NgayHetHieuLucMienTienThueDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",

                };
                if (entity.IdQuyetDinhThueDat != null)
                {
                    result.QuyenDuLieu = _quyetDinhThueDatService.CheckQuyenDuLieu(entity.IdQuyetDinhThueDat.Value);
                }
                var listFileViewModel = new List<FileTaiLieuViewModel>();
                var listFile = _context.FileTaiLieu.Include(x => x.File).Where(x => x.IdLoaiTaiLieu == NhomLoaiTaiLieuConstant.NhomQuyetDinhMienTienThueDat && x.IdTaiLieu == entity.IdQuyetDinhMienTienThueDat && x.TrangThai != 4).ToList();
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
                return new ApiSuccessResult<QuyetDinhMienTienThueDatViewModel>() { Data = result };

            }
            else
            {
                return new ApiErrorResult<QuyetDinhMienTienThueDatViewModel>("Không tìm thấy dữ liệu");
            }
        }
        public async Task<ApiResult<List<QuyetDinhMienTienThueDatViewModel>>> CanhBaoQuyetDinhMienTienThueDatSapHetHan()
        {
            var result = new List<QuyetDinhMienTienThueDatViewModel>();
            var data = await _context.QuyetDinhMienTienThueDat.Include(x => x.DoanhNghiep).Where(x => (DateTime.Now.Date.AddDays(90) > x.NgayHetHieuLucMienTienThueDat) && (DateTime.Now < x.NgayHetHieuLucMienTienThueDat)).ToListAsync();
            foreach (var item in data)
            {
                double soNgayConLai = 0;
                if (item.NgayHetHieuLucMienTienThueDat.HasValue)
                {
                    soNgayConLai = ((item.NgayHetHieuLucMienTienThueDat.Value - DateTime.Now.Date).TotalDays);
                    soNgayConLai = soNgayConLai > 0 ? soNgayConLai : 0;
                }
                var ThongBaoDonGiaThueDat = new QuyetDinhMienTienThueDatViewModel
                {
                    IdQuyetDinhMienTienThueDat = item.IdQuyetDinhMienTienThueDat,
                    IdDoanhNghiep = item.IdDoanhNghiep,
                    IdQuyetDinhThueDat = item.IdQuyetDinhThueDat,
                    TenDoanhNghiep = item.DoanhNghiep.TenDoanhNghiep,
                    SoQuyetDinhMienTienThueDat = item.SoQuyetDinhMienTienThueDat,
                    TenQuyetDinhMienTienThueDat = item.TenQuyetDinhMienTienThueDat,
                    NgayQuyetDinhMienTienThueDat = item.NgayQuyetDinhMienTienThueDat != null ? item.NgayQuyetDinhMienTienThueDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    DienTichMienTienThueDat = item.DienTichMienTienThueDat.ToString("N", new CultureInfo("vi-VN")),
                    ThoiHanMienTienThueDat = soNgayConLai.ToString(),
                    NgayHieuLucMienTienThueDat = item.NgayHieuLucMienTienThueDat != null ? item.NgayHieuLucMienTienThueDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    NgayHetHieuLucMienTienThueDat = item.NgayHetHieuLucMienTienThueDat != null ? item.NgayHetHieuLucMienTienThueDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                };
                result.Add(ThongBaoDonGiaThueDat);
            }
            return new ApiSuccessResult<List<QuyetDinhMienTienThueDatViewModel>>() { Data = result };
        }
    }
}
