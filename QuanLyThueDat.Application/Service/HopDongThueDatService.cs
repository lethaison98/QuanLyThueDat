using Microsoft.EntityFrameworkCore;
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

        public async Task<ApiResult<bool>> InsertUpdate(HopDongThueDatRequest rq)
        {
            var result = false;
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
            await _context.SaveChangesAsync();
            result = true;
            return new ApiSuccessResult<bool>() { Data = result };
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

        public async Task<ApiResult<List<HopDongThueDatViewModel>>> GetAll()
        {
            var result = new List<HopDongThueDatViewModel>();
            var data = await _context.HopDongThueDat.Include(x => x.DoanhNghiep).ToListAsync();
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
            foreach (var item in data)
            {
                var x = item.DoanhNghiep;
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
                listItem.Add(HopDongThueDat);
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
                return new ApiSuccessResult<HopDongThueDatViewModel>() { Data = result };

            }
            else
            {
                return new ApiErrorResult<HopDongThueDatViewModel>("Không tìm thấy dữ liệu");
            }
        }
    }
}
