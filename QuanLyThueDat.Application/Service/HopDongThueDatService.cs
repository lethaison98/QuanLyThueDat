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
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Application.Service
{
    public class HopDongThueDatService : IHopDongThueDatService
    {
        private readonly QuanLyThueDatDbContext _context;

        public HopDongThueDatService(QuanLyThueDatDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<int>> InsertUpdate(HopDongThueDatRequest rq)
        {
            var result = 0;
            var entity = _context.HopDongThueDat.FirstOrDefault(x => x.IdHopDongThueDat == rq.IdHopDongThueDat);
            if (entity == null)
            {
                entity = new HopDongThueDat()
                {
                    IdDoanhNghiep = rq.IdDoanhNghiep,
                    IdQuyetDinhThueDat = rq.IdQuyetDinhThueDat,
                    SoHopDong = rq.SoHopDong,
                    TenHopDong = rq.TenHopDong,
                    CoQuanKy = rq.CoQuanKy,
                    NguoiKy = rq.NguoiKy,
                    NgayKyHopDong = string.IsNullOrEmpty(rq.NgayKyHopDong) ? null : DateTime.Parse(rq.NgayKyHopDong, new CultureInfo("vi-VN")),
                    NgayHieuLucHopDong = string.IsNullOrEmpty(rq.NgayHieuLucHopDong) ? null : DateTime.Parse(rq.NgayHieuLucHopDong, new CultureInfo("vi-VN")),
                    NgayHetHieuLucHopDong = string.IsNullOrEmpty(rq.NgayHetHieuLucHopDong) ? null : DateTime.Parse(rq.NgayHetHieuLucHopDong, new CultureInfo("vi-VN")),
                };
            }
            else
            {
                entity.IdHopDongThueDat = rq.IdHopDongThueDat;
                entity.IdDoanhNghiep = rq.IdDoanhNghiep;
                entity.IdQuyetDinhThueDat = rq.IdQuyetDinhThueDat;
                entity.SoHopDong = rq.SoHopDong;
                entity.TenHopDong = rq.TenHopDong;
                entity.CoQuanKy = rq.CoQuanKy;
                entity.NguoiKy = rq.NguoiKy;
                entity.NgayKyHopDong = string.IsNullOrEmpty(rq.NgayKyHopDong) ? null : DateTime.Parse(rq.NgayKyHopDong, new CultureInfo("vi-VN"));
                entity.NgayHieuLucHopDong = string.IsNullOrEmpty(rq.NgayHieuLucHopDong) ? null : DateTime.Parse(rq.NgayHieuLucHopDong, new CultureInfo("vi-VN"));
                entity.NgayHetHieuLucHopDong = string.IsNullOrEmpty(rq.NgayHetHieuLucHopDong) ? null : DateTime.Parse(rq.NgayHetHieuLucHopDong, new CultureInfo("vi-VN"));
            }

            _context.HopDongThueDat.Update(entity);
            var listIdOldFile = rq.FileTaiLieu.Where(x => x.IdFileTaiLieu > 0).Select(x => x.IdFileTaiLieu);
            var listRemoveFile = _context.FileTaiLieu.Where(x => x.IdTaiLieu == entity.IdHopDongThueDat && x.IdLoaiTaiLieu == NhomLoaiTaiLieuConstant.NhomHopDongThueDat && !listIdOldFile.Contains(x.IdFileTaiLieu));
            foreach (var item in listRemoveFile)
            {
                item.TrangThai = 4;
            }
            _context.FileTaiLieu.UpdateRange(listRemoveFile);

            var listNewFile = rq.FileTaiLieu.Where(x => x.IdFileTaiLieu == 0);
            foreach (var item in listNewFile)
            {
                item.IdTaiLieu = entity.IdHopDongThueDat;
                item.IdLoaiTaiLieu = NhomLoaiTaiLieuConstant.NhomHopDongThueDat;
                item.TrangThai = 1;
                item.NgayTao = DateTime.Now;
            }
            _context.FileTaiLieu.AddRange(listNewFile);
            await _context.SaveChangesAsync();
            result = entity.IdHopDongThueDat;
            return new ApiSuccessResult<int>() { Data = result };
        }

        public async Task<ApiResult<bool>> Delete(int idHopDongThueDat)
        {
            var result = false;
            var data = _context.HopDongThueDat.FirstOrDefault(x => x.IdHopDongThueDat == idHopDongThueDat);
            if (data != null)
            {
                _context.HopDongThueDat.Remove(data);
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

        public async Task<ApiResult<List<HopDongThueDatViewModel>>> GetAll(int? idDoanhNghiep)
        {
            var result = new List<HopDongThueDatViewModel>();
            var data = new List<HopDongThueDat>();
            if (idDoanhNghiep == null)
            {
                data = await _context.HopDongThueDat.Include(x => x.DoanhNghiep).ToListAsync();
            }
            else
            {
                data = await _context.HopDongThueDat.Include(x => x.DoanhNghiep).Where(x => x.IdDoanhNghiep == idDoanhNghiep).ToListAsync();
            }
            foreach (var item in data)
            {
                var HopDongThueDat = new HopDongThueDatViewModel
                {
                    IdHopDongThueDat = item.IdHopDongThueDat,
                    IdDoanhNghiep = item.IdDoanhNghiep,
                    IdQuyetDinhThueDat = item.IdQuyetDinhThueDat,
                    TenDoanhNghiep = item.DoanhNghiep.TenDoanhNghiep,
                    SoHopDong = item.SoHopDong,
                    TenHopDong = item.TenHopDong,
                    CoQuanKy = item.CoQuanKy,
                    NguoiKy = item.NguoiKy,
                    NgayKyHopDong = item.NgayKyHopDong != null ? item.NgayKyHopDong.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    NgayHieuLucHopDong = item.NgayHieuLucHopDong != null ? item.NgayHieuLucHopDong.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    NgayHetHieuLucHopDong = item.NgayHetHieuLucHopDong != null ? item.NgayHetHieuLucHopDong.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                };
                result.Add(HopDongThueDat);
            }
            return new ApiSuccessResult<List<HopDongThueDatViewModel>>() { Data = result };
        }

        public async Task<ApiResult<PageViewModel<HopDongThueDatViewModel>>> GetAllPaging(int? idDoanhNghiep, string keyword, int pageIndex, int pageSize)
        {
            var query = from a in _context.HopDongThueDat.Include(x => x.DoanhNghiep).Include(x => x.DoanhNghiep)
                        select a;
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => (x.SoHopDong.ToLower().Contains(keyword.ToLower())) || (x.DoanhNghiep.TenDoanhNghiep.ToLower().Contains(keyword.ToLower())) || (x.DoanhNghiep.MaSoThue.ToLower().Contains(keyword.ToLower())));
            }
            if (idDoanhNghiep != null)
            {
                query = query.Where(x => x.IdDoanhNghiep == idDoanhNghiep);
            }
            var data = await query.OrderByDescending(x => x.NgayKyHopDong)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();
            var listItem = new List<HopDongThueDatViewModel>();
            foreach (var entity in data)
            {
                var x = entity.DoanhNghiep;
                var hopDongThueDat = new HopDongThueDatViewModel
                {
                    IdHopDongThueDat = entity.IdHopDongThueDat,
                    IdDoanhNghiep = entity.IdDoanhNghiep,
                    IdQuyetDinhThueDat = entity.IdQuyetDinhThueDat,
                    TenDoanhNghiep = entity.DoanhNghiep.TenDoanhNghiep,
                    SoHopDong = entity.SoHopDong,
                    TenHopDong = entity.TenHopDong,
                    CoQuanKy = entity.CoQuanKy,
                    NguoiKy = entity.NguoiKy,
                    NgayKyHopDong = entity.NgayKyHopDong != null ? entity.NgayKyHopDong.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    NgayHieuLucHopDong = entity.NgayHieuLucHopDong != null ? entity.NgayHieuLucHopDong.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    NgayHetHieuLucHopDong = entity.NgayHetHieuLucHopDong != null ? entity.NgayHetHieuLucHopDong.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                };
                var listFileViewModel = new List<FileTaiLieuViewModel>();
                var listFile = _context.FileTaiLieu.Include(x => x.File).Where(x => x.IdLoaiTaiLieu == NhomLoaiTaiLieuConstant.NhomHopDongThueDat && x.IdTaiLieu == entity.IdHopDongThueDat && x.TrangThai != 4).ToList();
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
                hopDongThueDat.DsFileTaiLieu = listFileViewModel;
                listItem.Add(hopDongThueDat);
            }
            var result = new PageViewModel<HopDongThueDatViewModel>()
            {
                Items = listItem,
                PageIndex = pageIndex,
                TotalRecord = query.Count(),
                PageSize = pageSize
            };
            return new ApiSuccessResult<PageViewModel<HopDongThueDatViewModel>>() { Data = result };
        }

        public async Task<ApiResult<HopDongThueDatViewModel>> GetById(int idHopDongThueDat)
        {
            var result = new HopDongThueDatViewModel();
            var entity = await _context.HopDongThueDat.Include(x => x.DoanhNghiep).FirstOrDefaultAsync(x => x.IdHopDongThueDat == idHopDongThueDat);
            if (entity != null)
            {
                result = new HopDongThueDatViewModel
                {
                    IdHopDongThueDat = entity.IdHopDongThueDat,
                    IdDoanhNghiep = entity.IdDoanhNghiep,
                    IdQuyetDinhThueDat = entity.IdQuyetDinhThueDat,
                    TenDoanhNghiep = entity.DoanhNghiep.TenDoanhNghiep,
                    SoHopDong = entity.SoHopDong,
                    TenHopDong = entity.TenHopDong,
                    CoQuanKy = entity.CoQuanKy,
                    NguoiKy = entity.NguoiKy,
                    NgayKyHopDong = entity.NgayKyHopDong != null ? entity.NgayKyHopDong.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    NgayHieuLucHopDong = entity.NgayHieuLucHopDong != null ? entity.NgayHieuLucHopDong.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    NgayHetHieuLucHopDong = entity.NgayHetHieuLucHopDong != null ? entity.NgayHetHieuLucHopDong.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                };
                var listFileViewModel = new List<FileTaiLieuViewModel>();
                var listFile = _context.FileTaiLieu.Include(x => x.File).Where(x => x.IdLoaiTaiLieu == NhomLoaiTaiLieuConstant.NhomHopDongThueDat && x.IdTaiLieu == entity.IdHopDongThueDat && x.TrangThai != 4).ToList();
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
                return new ApiSuccessResult<HopDongThueDatViewModel>() { Data = result };

            }
            else
            {
                return new ApiErrorResult<HopDongThueDatViewModel>("Không tìm thấy dữ liệu");
            }
        }
        public async Task<ApiResult<List<HopDongThueDatViewModel>>> CanhBaoHopDongSapHetHan()
        {
            var result = new List<HopDongThueDatViewModel>();
            var data = await _context.HopDongThueDat.Include(x => x.DoanhNghiep).Where(x => (DateTime.Now.Date.AddDays(90) > x.NgayHetHieuLucHopDong) && (DateTime.Now < x.NgayHetHieuLucHopDong)).ToListAsync();
            foreach (var item in data)
            {
                double soNgayConLai = 0;
                if (item.NgayHetHieuLucHopDong.HasValue)
                {
                    soNgayConLai = ((item.NgayHetHieuLucHopDong.Value - DateTime.Now.Date).TotalDays);
                    soNgayConLai = soNgayConLai > 0 ? soNgayConLai : 0;
                }

                var ThongBaoDonGiaThueDat = new HopDongThueDatViewModel
                {
                    IdHopDongThueDat = item.IdHopDongThueDat,
                    IdDoanhNghiep = item.IdDoanhNghiep,
                    TenDoanhNghiep = item.DoanhNghiep.TenDoanhNghiep,
                    SoHopDong = item.SoHopDong,
                    TenHopDong = item.TenHopDong,
                    CoQuanKy = item.CoQuanKy,
                    NguoiKy = item.NguoiKy,
                    ThoiHanHopDong = soNgayConLai.ToString(),
                    NgayKyHopDong = item.NgayKyHopDong != null ? item.NgayKyHopDong.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    NgayHieuLucHopDong = item.NgayHieuLucHopDong != null ? item.NgayHieuLucHopDong.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    NgayHetHieuLucHopDong = item.NgayHetHieuLucHopDong != null ? item.NgayHetHieuLucHopDong.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                };
                result.Add(ThongBaoDonGiaThueDat);
            }
            return new ApiSuccessResult<List<HopDongThueDatViewModel>>() { Data = result };
        }
    }
}
