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
    public class QuyetDinhMienTienThueDatService : IQuyetDinhMienTienThueDatService
    {
        private readonly QuanLyThueDatDbContext _context;

        public QuyetDinhMienTienThueDatService(QuanLyThueDatDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<bool>> InsertUpdate(QuyetDinhMienTienThueDatRequest rq)
        {
            var result = false;
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

            }

            _context.QuyetDinhMienTienThueDat.Update(entity);
            await _context.SaveChangesAsync();
            result = true;
            return new ApiSuccessResult<bool>() { Data = result };
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
                    DienTichMienTienThueDat = item.DienTichMienTienThueDat,
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
                query = query.Where(x => x.TenQuyetDinhMienTienThueDat.ToLower().Contains(keyword.ToLower()) || x.DoanhNghiep.MaSoThue.ToLower().Contains(keyword.ToLower()));
            }
            if (idDoanhNghiep != null)
            {
                query = query.Where(x => x.IdDoanhNghiep == idDoanhNghiep);
            }
            var data = await query.OrderByDescending(x => x.TenQuyetDinhMienTienThueDat)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();
            var listItem = new List<QuyetDinhMienTienThueDatViewModel>();
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
                    DienTichMienTienThueDat = item.DienTichMienTienThueDat,
                    ThoiHanMienTienThueDat = item.ThoiHanMienTienThueDat,
                    NgayHieuLucMienTienThueDat = item.NgayHieuLucMienTienThueDat != null ? item.NgayHieuLucMienTienThueDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    NgayHetHieuLucMienTienThueDat = item.NgayHetHieuLucMienTienThueDat != null ? item.NgayHetHieuLucMienTienThueDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                };
                listItem.Add(QuyetDinhMienTienThueDat);
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
                    SoQuyetDinhMienTienThueDat = entity.SoQuyetDinhMienTienThueDat,
                    TenQuyetDinhMienTienThueDat = entity.TenQuyetDinhMienTienThueDat,
                    NgayQuyetDinhMienTienThueDat = entity.NgayQuyetDinhMienTienThueDat != null ? entity.NgayQuyetDinhMienTienThueDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    DienTichMienTienThueDat = entity.DienTichMienTienThueDat,
                    ThoiHanMienTienThueDat = entity.ThoiHanMienTienThueDat,
                    NgayHieuLucMienTienThueDat = entity.NgayHieuLucMienTienThueDat != null ? entity.NgayHieuLucMienTienThueDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    NgayHetHieuLucMienTienThueDat = entity.NgayHetHieuLucMienTienThueDat != null ? entity.NgayHetHieuLucMienTienThueDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",

                };
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
                    DienTichMienTienThueDat = item.DienTichMienTienThueDat,
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
