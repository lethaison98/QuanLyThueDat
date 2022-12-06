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
    public class ThongBaoDonGiaThueDatService : IThongBaoDonGiaThueDatService
    {
        private readonly QuanLyThueDatDbContext _context;

        public ThongBaoDonGiaThueDatService(QuanLyThueDatDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<int>> InsertUpdate(ThongBaoDonGiaThueDatRequest rq)
        {
            var result = 0;
            var entity = _context.ThongBaoDonGiaThueDat.FirstOrDefault(x => x.IdThongBaoDonGiaThueDat == rq.IdThongBaoDonGiaThueDat);
            if (entity == null)
            {
                entity = new ThongBaoDonGiaThueDat()
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
                    SoThongBaoDonGiaThueDat = rq.SoThongBaoDonGiaThueDat,
                    TenThongBaoDonGiaThueDat = rq.TenThongBaoDonGiaThueDat,
                    LanThongBaoDonGiaThueDat = rq.LanThongBaoDonGiaThueDat,
                    DienTichKhongPhaiNop = rq.DienTichKhongPhaiNop,
                    DienTichPhaiNop = rq.DienTichPhaiNop,
                    DonGia = rq.DonGia,
                    ThoiHanDonGia = rq.ThoiHanDonGia,
                    NgayThongBaoDonGiaThueDat = string.IsNullOrEmpty(rq.NgayThongBaoDonGiaThueDat) ? null : DateTime.Parse(rq.NgayThongBaoDonGiaThueDat, new CultureInfo("vi-VN")),
                    NgayHieuLucDonGiaThueDat = string.IsNullOrEmpty(rq.NgayHieuLucDonGiaThueDat) ? null : DateTime.Parse(rq.NgayHieuLucDonGiaThueDat, new CultureInfo("vi-VN")),
                    NgayHetHieuLucDonGiaThueDat = string.IsNullOrEmpty(rq.NgayHetHieuLucDonGiaThueDat) ? null : DateTime.Parse(rq.NgayHetHieuLucDonGiaThueDat, new CultureInfo("vi-VN")),
                    HinhThucThue = rq.HinhThucThue,
                    LanhDaoKyThongBaoDonGiaThueDat = rq.LanhDaoKyThongBaoDonGiaThueDat
                };
            }
            else
            {
                entity.IdThongBaoDonGiaThueDat = rq.IdThongBaoDonGiaThueDat;
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
                entity.SoThongBaoDonGiaThueDat = rq.SoThongBaoDonGiaThueDat;
                entity.TenThongBaoDonGiaThueDat = rq.TenThongBaoDonGiaThueDat;
                entity.LanThongBaoDonGiaThueDat = rq.LanThongBaoDonGiaThueDat;
                entity.DienTichKhongPhaiNop = rq.DienTichKhongPhaiNop;
                entity.DienTichPhaiNop = rq.DienTichPhaiNop;
                entity.DonGia = rq.DonGia;
                entity.ThoiHanDonGia = rq.ThoiHanDonGia;
                entity.NgayThongBaoDonGiaThueDat = string.IsNullOrEmpty(rq.NgayThongBaoDonGiaThueDat) ? null : DateTime.Parse(rq.NgayThongBaoDonGiaThueDat, new CultureInfo("vi-VN"));
                entity.NgayHieuLucDonGiaThueDat = string.IsNullOrEmpty(rq.NgayHieuLucDonGiaThueDat) ? null : DateTime.Parse(rq.NgayHieuLucDonGiaThueDat, new CultureInfo("vi-VN"));
                entity.NgayHetHieuLucDonGiaThueDat = string.IsNullOrEmpty(rq.NgayHetHieuLucDonGiaThueDat) ? null : DateTime.Parse(rq.NgayHetHieuLucDonGiaThueDat, new CultureInfo("vi-VN"));
                entity.HinhThucThue = rq.HinhThucThue;
                entity.LanhDaoKyThongBaoDonGiaThueDat = rq.LanhDaoKyThongBaoDonGiaThueDat;
            }

            _context.ThongBaoDonGiaThueDat.Update(entity);
            await _context.SaveChangesAsync();
            result = entity.IdThongBaoDonGiaThueDat;
            return new ApiSuccessResult<int>() { Data = result };
        }

        public async Task<ApiResult<bool>> Delete(int idThongBaoDonGiaThueDat)
        {
            var result = false;
            var data = _context.ThongBaoDonGiaThueDat.FirstOrDefault(x => x.IdThongBaoDonGiaThueDat == idThongBaoDonGiaThueDat);
            if (data != null)
            {
                _context.ThongBaoDonGiaThueDat.Remove(data);
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

        public async Task<ApiResult<List<ThongBaoDonGiaThueDatViewModel>>> GetAll()
        {
            var result = new List<ThongBaoDonGiaThueDatViewModel>();
            var data = await _context.ThongBaoDonGiaThueDat.Include(x => x.DoanhNghiep).ToListAsync();
            foreach (var item in data)
            {
                var ThongBaoDonGiaThueDat = new ThongBaoDonGiaThueDatViewModel
                {
                    IdThongBaoDonGiaThueDat = item.IdThongBaoDonGiaThueDat,
                    IdDoanhNghiep = item.IdDoanhNghiep,
                    TenDoanhNghiep = item.DoanhNghiep.TenDoanhNghiep,
                    SoThongBaoDonGiaThueDat = item.SoThongBaoDonGiaThueDat,
                    TenThongBaoDonGiaThueDat = item.TenThongBaoDonGiaThueDat,
                    ViTriThuaDat = item.ViTriThuaDat,
                    DienTichKhongPhaiNop = item.DienTichKhongPhaiNop.ToString("N", new CultureInfo("vi-VN")),
                    DienTichPhaiNop = item.DienTichPhaiNop.ToString("N", new CultureInfo("vi-VN")),
                    DonGia = item.DonGia.ToString("N0", new CultureInfo("vi-VN")),
                    NgayThongBaoDonGiaThueDat = item.NgayThongBaoDonGiaThueDat != null ? item.NgayThongBaoDonGiaThueDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    NgayHieuLucDonGiaThueDat = item.NgayHieuLucDonGiaThueDat != null ? item.NgayHieuLucDonGiaThueDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    NgayHetHieuLucDonGiaThueDat = item.NgayHetHieuLucDonGiaThueDat != null ? item.NgayHetHieuLucDonGiaThueDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                };
                result.Add(ThongBaoDonGiaThueDat);
            }
            return new ApiSuccessResult<List<ThongBaoDonGiaThueDatViewModel>>() { Data = result };
        }

        public async Task<ApiResult<PageViewModel<ThongBaoDonGiaThueDatViewModel>>> GetAllPaging(int? idDoanhNghiep, string keyword, int pageIndex, int pageSize)
        {
            var query = from a in _context.ThongBaoDonGiaThueDat.Include(x => x.DoanhNghiep)
                        select a;
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => (x.SoThongBaoDonGiaThueDat.ToLower().Contains(keyword.ToLower())) || (x.DoanhNghiep.TenDoanhNghiep.ToLower().Contains(keyword.ToLower())) || (x.DoanhNghiep.MaSoThue.ToLower().Contains(keyword.ToLower())));
            }
            if (idDoanhNghiep != null)
            {
                query = query.Where(x => x.IdDoanhNghiep == idDoanhNghiep);
            }
            var data = await query.OrderByDescending(x => x.NgayThongBaoDonGiaThueDat)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();
            var listItem = new List<ThongBaoDonGiaThueDatViewModel>();
            foreach (var item in data)
            {
                var x = item.DoanhNghiep;
                var ThongBaoDonGiaThueDat = new ThongBaoDonGiaThueDatViewModel
                {
                    IdThongBaoDonGiaThueDat = item.IdThongBaoDonGiaThueDat,
                    IdDoanhNghiep = item.IdDoanhNghiep,
                    TenDoanhNghiep = item.DoanhNghiep.TenDoanhNghiep,
                    SoThongBaoDonGiaThueDat = item.SoThongBaoDonGiaThueDat,
                    TenThongBaoDonGiaThueDat = item.TenThongBaoDonGiaThueDat,
                    ViTriThuaDat = item.ViTriThuaDat,
                    DienTichKhongPhaiNop = item.DienTichKhongPhaiNop.ToString("N", new CultureInfo("vi-VN")),
                    DienTichPhaiNop = item.DienTichPhaiNop.ToString("N", new CultureInfo("vi-VN")),
                    DonGia = item.DonGia.ToString("N0", new CultureInfo("vi-VN")),
                    NgayThongBaoDonGiaThueDat = item.NgayThongBaoDonGiaThueDat != null ? item.NgayThongBaoDonGiaThueDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    NgayHieuLucDonGiaThueDat = item.NgayHieuLucDonGiaThueDat != null ? item.NgayHieuLucDonGiaThueDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    NgayHetHieuLucDonGiaThueDat = item.NgayHetHieuLucDonGiaThueDat != null ? item.NgayHetHieuLucDonGiaThueDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                };
                listItem.Add(ThongBaoDonGiaThueDat);
            }
            var result = new PageViewModel<ThongBaoDonGiaThueDatViewModel>()
            {
                Items = listItem,
                PageIndex = pageIndex,
                TotalRecord = query.Count(),
                PageSize = pageSize
            };
            return new ApiSuccessResult<PageViewModel<ThongBaoDonGiaThueDatViewModel>>() { Data = result };
        }

        public async Task<ApiResult<ThongBaoDonGiaThueDatViewModel>> GetById(int idThongBaoDonGiaThueDat)
        {
            var result = new ThongBaoDonGiaThueDatViewModel();
            var entity = await _context.ThongBaoDonGiaThueDat.Include(x => x.DoanhNghiep).FirstOrDefaultAsync(x => x.IdThongBaoDonGiaThueDat == idThongBaoDonGiaThueDat);
            if (entity != null)
            {
                result = new ThongBaoDonGiaThueDatViewModel
                {
                    IdThongBaoDonGiaThueDat = entity.IdThongBaoDonGiaThueDat,
                    IdDoanhNghiep = entity.IdDoanhNghiep,
                    TenDoanhNghiep = entity.DoanhNghiep.TenDoanhNghiep,
                    MaSoThue = entity.DoanhNghiep.MaSoThue,
                    CoQuanQuanLyThue = entity.DoanhNghiep.CoQuanQuanLyThue,
                    DiaChi = entity.DoanhNghiep.DiaChi,
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
                    SoThongBaoDonGiaThueDat = entity.SoThongBaoDonGiaThueDat,
                    TenThongBaoDonGiaThueDat = entity.TenThongBaoDonGiaThueDat,
                    LanThongBaoDonGiaThueDat = entity.LanThongBaoDonGiaThueDat,
                    DienTichKhongPhaiNop = entity.DienTichKhongPhaiNop.ToString("N", new CultureInfo("vi-VN")),
                    DienTichPhaiNop = entity.DienTichPhaiNop.ToString("N", new CultureInfo("vi-VN")),
                    DonGia = entity.DonGia.ToString("N0", new CultureInfo("vi-VN")),
                    ThoiHanDonGia = entity.ThoiHanDonGia,
                    NgayThongBaoDonGiaThueDat = entity.NgayThongBaoDonGiaThueDat != null ? entity.NgayThongBaoDonGiaThueDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    NgayHieuLucDonGiaThueDat = entity.NgayHieuLucDonGiaThueDat != null ? entity.NgayHieuLucDonGiaThueDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    NgayHetHieuLucDonGiaThueDat = entity.NgayHetHieuLucDonGiaThueDat != null ? entity.NgayHetHieuLucDonGiaThueDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    HinhThucThue = entity.HinhThucThue,
                    LanhDaoKyThongBaoDonGiaThueDat = entity.LanhDaoKyThongBaoDonGiaThueDat
                };
                return new ApiSuccessResult<ThongBaoDonGiaThueDatViewModel>() { Data = result };

            }
            else
            {
                return new ApiErrorResult<ThongBaoDonGiaThueDatViewModel>("Không tìm thấy dữ liệu");
            }
        }

        public async Task<ApiResult<List<ThongBaoDonGiaThueDatViewModel>>> GetAllByRequest(ThongBaoDonGiaThueDatRequest rq)
        {
            var result = new List<ThongBaoDonGiaThueDatViewModel>();
            var query = from a in _context.ThongBaoDonGiaThueDat.Include(x => x.DoanhNghiep)
                        select a;
            if (rq.IdDoanhNghiep != 0)
            {
                query = query.Where(x => x.IdDoanhNghiep == rq.IdDoanhNghiep);
            }
            if (rq.IdQuyetDinhThueDat != 0)
            {
                query = query.Where(x => x.IdQuyetDinhThueDat == rq.IdQuyetDinhThueDat || x.IdQuyetDinhThueDat == null);
            }
            var data = await query.OrderByDescending(x => x.NgayThongBaoDonGiaThueDat).ToListAsync();
            foreach (var item in data)
            {
                var ThongBaoDonGiaThueDat = new ThongBaoDonGiaThueDatViewModel
                {
                    IdThongBaoDonGiaThueDat = item.IdThongBaoDonGiaThueDat,
                    IdDoanhNghiep = item.IdDoanhNghiep,
                    TenDoanhNghiep = item.DoanhNghiep.TenDoanhNghiep,
                    SoThongBaoDonGiaThueDat = item.SoThongBaoDonGiaThueDat,
                    TenThongBaoDonGiaThueDat = item.TenThongBaoDonGiaThueDat,
                    ViTriThuaDat = item.ViTriThuaDat,
                    DienTichKhongPhaiNop = item.DienTichKhongPhaiNop.ToString("N", new CultureInfo("vi-VN")),
                    DienTichPhaiNop = item.DienTichPhaiNop.ToString("N", new CultureInfo("vi-VN")),
                    DonGia = item.DonGia.ToString("N0", new CultureInfo("vi-VN")),
                    NgayThongBaoDonGiaThueDat = item.NgayThongBaoDonGiaThueDat != null ? item.NgayThongBaoDonGiaThueDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    NgayHieuLucDonGiaThueDat = item.NgayHieuLucDonGiaThueDat != null ? item.NgayHieuLucDonGiaThueDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    NgayHetHieuLucDonGiaThueDat = item.NgayHetHieuLucDonGiaThueDat != null ? item.NgayHetHieuLucDonGiaThueDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                };
                result.Add(ThongBaoDonGiaThueDat);
            }
            return new ApiSuccessResult<List<ThongBaoDonGiaThueDatViewModel>>()
            {
                Data = result
            };
        }
        public async Task<ApiResult<List<ThongBaoDonGiaThueDatViewModel>>> CanhBaoThongBaoDonGiaThueDatSapHetHan()
        {
            var result = new List<ThongBaoDonGiaThueDatViewModel>();
            var data = await _context.ThongBaoDonGiaThueDat.Include(x => x.DoanhNghiep).Where(x=> (DateTime.Now.Date.AddDays(90) > x.NgayHetHieuLucDonGiaThueDat) && (DateTime.Now < x.NgayHetHieuLucDonGiaThueDat)).ToListAsync();
            foreach (var item in data)
            {
                double soNgayConLai = 0;
                if (item.NgayHetHieuLucDonGiaThueDat.HasValue)
                {
                    soNgayConLai = ((item.NgayHetHieuLucDonGiaThueDat.Value - DateTime.Now.Date).TotalDays);
                    soNgayConLai = soNgayConLai > 0 ? soNgayConLai : 0;
                }

                var ThongBaoDonGiaThueDat = new ThongBaoDonGiaThueDatViewModel
                {
                    IdThongBaoDonGiaThueDat = item.IdThongBaoDonGiaThueDat,
                    IdDoanhNghiep = item.IdDoanhNghiep,
                    TenDoanhNghiep = item.DoanhNghiep.TenDoanhNghiep,
                    SoThongBaoDonGiaThueDat = item.SoThongBaoDonGiaThueDat,
                    TenThongBaoDonGiaThueDat = item.TenThongBaoDonGiaThueDat,
                    ViTriThuaDat = item.ViTriThuaDat,
                    DienTichKhongPhaiNop = item.DienTichKhongPhaiNop.ToString("N", new CultureInfo("vi-VN")),
                    DienTichPhaiNop = item.DienTichPhaiNop.ToString("N", new CultureInfo("vi-VN")),
                    DonGia = item.DonGia.ToString("N0", new CultureInfo("vi-VN")),
                    ThoiHanDonGia = soNgayConLai.ToString(),
                    NgayThongBaoDonGiaThueDat = item.NgayThongBaoDonGiaThueDat != null ? item.NgayThongBaoDonGiaThueDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    NgayHieuLucDonGiaThueDat = item.NgayHieuLucDonGiaThueDat != null ? item.NgayHieuLucDonGiaThueDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    NgayHetHieuLucDonGiaThueDat = item.NgayHetHieuLucDonGiaThueDat != null ? item.NgayHetHieuLucDonGiaThueDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                };
                result.Add(ThongBaoDonGiaThueDat);
            }
            return new ApiSuccessResult<List<ThongBaoDonGiaThueDatViewModel>>() { Data = result };
        }
    }
}
