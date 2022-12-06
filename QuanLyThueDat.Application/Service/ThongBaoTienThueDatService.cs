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
    public class ThongBaoTienThueDatService : IThongBaoTienThueDatService
    {
        private readonly QuanLyThueDatDbContext _context;

        public ThongBaoTienThueDatService(QuanLyThueDatDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<int>> InsertUpdate(ThongBaoTienThueDatRequest rq)
        {
            var result = 0;
            var entity = _context.ThongBaoTienThueDat.Include(x => x.DsThongBaoTienThueDatChiTiet).FirstOrDefault(x => x.IdThongBaoTienThueDat == rq.IdThongBaoTienThueDat);
            if (entity == null)
            {
                entity = new ThongBaoTienThueDat()
                {
                    IdDoanhNghiep = rq.IdDoanhNghiep,
                    IdQuyetDinhThueDat = rq.IdQuyetDinhThueDat,
                    IdHopDongThueDat = rq.IdHopDongThueDat,
                    IdQuyetDinhMienTienThueDat = rq.IdQuyetDinhMienTienThueDat,
                    IdThongBaoDonGiaThueDat = rq.IdThongBaoDonGiaThueDat,
                    LoaiThongBaoTienThueDat = rq.LoaiThongBaoTienThueDat,
                    SoQuyetDinhThueDat = rq.SoQuyetDinhThueDat,
                    TenQuyetDinhThueDat = rq.TenQuyetDinhThueDat,
                    NgayQuyetDinhThueDat = string.IsNullOrEmpty(rq.NgayQuyetDinhThueDat) ? null : DateTime.Parse(rq.NgayQuyetDinhThueDat, new CultureInfo("vi-VN")),
                    MucDichSuDung = rq.MucDichSuDung,
                    SoThongBaoTienThueDat = rq.SoThongBaoTienThueDat,
                    SoThongBaoDonGiaThueDat = rq.SoThongBaoDonGiaThueDat,
                    TenThongBaoDonGiaThueDat = rq.TenThongBaoDonGiaThueDat,
                    ViTriThuaDat = rq.ViTriThuaDat,
                    DienTichKhongPhaiNop = rq.DienTichKhongPhaiNop,
                    DienTichPhaiNop = rq.DienTichPhaiNop,
                    DonGia = rq.DonGia,
                    NgayThongBaoDonGiaThueDat = string.IsNullOrEmpty(rq.NgayThongBaoDonGiaThueDat) ? null : DateTime.Parse(rq.NgayThongBaoDonGiaThueDat, new CultureInfo("vi-VN")),
                    NgayThongBaoTienThueDat = string.IsNullOrEmpty(rq.NgayThongBaoTienThueDat) ? null : DateTime.Parse(rq.NgayThongBaoTienThueDat, new CultureInfo("vi-VN")),
                    SoTien = rq.SoTien,
                    SoTienMienGiam = rq.SoTienMienGiam,
                    SoTienPhaiNop = rq.SoTienPhaiNop,
                    Nam = rq.Nam,
                    LanhDaoKyThongBaoTienThueDat = rq.LanhDaoKyThongBaoTienThueDat,
                    DiaChiThuaDat = rq.DiaChiThuaDat,
                    ThoiHanThue = rq.ThoiHanThue,
                    DenNgayThue = string.IsNullOrEmpty(rq.DenNgayThue) ? null : DateTime.Parse(rq.DenNgayThue, new CultureInfo("vi-VN")),
                    TuNgayThue = string.IsNullOrEmpty(rq.TuNgayThue) ? null : DateTime.Parse(rq.TuNgayThue, new CultureInfo("vi-VN")),
                    TongDienTich = rq.TongDienTich,
                };
            }
            else
            {
                entity.IdThongBaoTienThueDat = rq.IdThongBaoTienThueDat;
                entity.IdDoanhNghiep = rq.IdDoanhNghiep;
                entity.IdQuyetDinhThueDat = rq.IdQuyetDinhThueDat;
                entity.IdHopDongThueDat = rq.IdHopDongThueDat;
                entity.IdQuyetDinhMienTienThueDat = rq.IdQuyetDinhMienTienThueDat;
                entity.IdThongBaoDonGiaThueDat = rq.IdThongBaoDonGiaThueDat;
                entity.LoaiThongBaoTienThueDat = rq.LoaiThongBaoTienThueDat;
                entity.SoQuyetDinhThueDat = rq.SoQuyetDinhThueDat;
                entity.TenQuyetDinhThueDat = rq.TenQuyetDinhThueDat;
                entity.NgayQuyetDinhThueDat = string.IsNullOrEmpty(rq.NgayQuyetDinhThueDat) ? null : DateTime.Parse(rq.NgayQuyetDinhThueDat, new CultureInfo("vi-VN"));
                entity.MucDichSuDung = rq.MucDichSuDung;
                entity.SoThongBaoDonGiaThueDat = rq.SoThongBaoDonGiaThueDat;
                entity.TenThongBaoDonGiaThueDat = rq.TenThongBaoDonGiaThueDat;
                entity.NgayThongBaoDonGiaThueDat = string.IsNullOrEmpty(rq.NgayThongBaoDonGiaThueDat) ? null : DateTime.Parse(rq.NgayThongBaoDonGiaThueDat, new CultureInfo("vi-VN"));
                entity.SoThongBaoTienThueDat = rq.SoThongBaoTienThueDat;
                entity.ViTriThuaDat = rq.ViTriThuaDat;
                entity.DienTichKhongPhaiNop = rq.DienTichKhongPhaiNop;
                entity.DienTichPhaiNop = rq.DienTichPhaiNop;
                entity.DonGia = rq.DonGia;
                entity.NgayThongBaoTienThueDat = string.IsNullOrEmpty(rq.NgayThongBaoTienThueDat) ? null : DateTime.Parse(rq.NgayThongBaoTienThueDat, new CultureInfo("vi-VN"));
                entity.SoTien = rq.SoTien;
                entity.SoTienMienGiam = rq.SoTienMienGiam;
                entity.SoTienPhaiNop = rq.SoTienPhaiNop;
                entity.Nam = rq.Nam;
                entity.DiaChiThuaDat = rq.DiaChiThuaDat;
                entity.ThoiHanThue = rq.ThoiHanThue;
                entity.LanhDaoKyThongBaoTienThueDat = rq.LanhDaoKyThongBaoTienThueDat;
                entity.DenNgayThue = string.IsNullOrEmpty(rq.DenNgayThue) ? null : DateTime.Parse(rq.DenNgayThue, new CultureInfo("vi-VN"));
                entity.TuNgayThue = string.IsNullOrEmpty(rq.TuNgayThue) ? null : DateTime.Parse(rq.TuNgayThue, new CultureInfo("vi-VN"));
                entity.TongDienTich = rq.TongDienTich;
            }
            var listThongBaoTienThueDatChiTiet = new List<ThongBaoTienThueDatChiTiet>();
            if (rq.ThongBaoTienThueDatChiTiet != null)
            {
                foreach (var item in rq.ThongBaoTienThueDatChiTiet)
                {
                    var ct = new ThongBaoTienThueDatChiTiet();
                    ct.IdThongBaoTienThueDat = item.IdThongBaoTienThueDat;
                    ct.IdThongBaoTienThueDatChiTiet = item.IdThongBaoTienThueDatChiTiet;
                    ct.Nam = item.Nam; ;
                    ct.DonGia = item.DonGia;
                    ct.DienTichPhaiNop = item.DienTichPhaiNop;
                    ct.DienTichKhongPhaiNop = item.DienTichKhongPhaiNop;
                    ct.SoTien = item.SoTien;
                    ct.SoTienMienGiam = item.SoTienMienGiam;
                    ct.SoTienPhaiNop = item.SoTienPhaiNop;
                    ct.TuNgayTinhTien = string.IsNullOrEmpty(item.TuNgayTinhTien) ? null : DateTime.Parse(item.TuNgayTinhTien, new CultureInfo("vi-VN"));
                    ct.DenNgayTinhTien = string.IsNullOrEmpty(item.DenNgayTinhTien) ? null : DateTime.Parse(item.DenNgayTinhTien, new CultureInfo("vi-VN"));
                    listThongBaoTienThueDatChiTiet.Add(ct);
                }
            }
            entity.DsThongBaoTienThueDatChiTiet = listThongBaoTienThueDatChiTiet;
            _context.ThongBaoTienThueDat.Update(entity);
            await _context.SaveChangesAsync();
            result = entity.IdThongBaoTienThueDat;
            return new ApiSuccessResult<int>() { Data = result };
        }

        public async Task<ApiResult<bool>> Delete(int idThongBaoTienThueDat)
        {
            var result = false;
            var data = _context.ThongBaoTienThueDat.FirstOrDefault(x => x.IdThongBaoTienThueDat == idThongBaoTienThueDat);
            if (data != null)
            {
                _context.ThongBaoTienThueDat.Remove(data);
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

        public async Task<ApiResult<List<ThongBaoTienThueDatViewModel>>> GetAll()
        {
            var result = new List<ThongBaoTienThueDatViewModel>();
            var data = await _context.ThongBaoTienThueDat.Include(x => x.DoanhNghiep).ToListAsync();
            foreach (var item in data)
            {
                var ThongBaoTienThueDat = new ThongBaoTienThueDatViewModel
                {
                    IdThongBaoTienThueDat = item.IdThongBaoTienThueDat,
                    IdDoanhNghiep = item.IdDoanhNghiep,
                    TenDoanhNghiep = item.DoanhNghiep.TenDoanhNghiep,
                    SoThongBaoTienThueDat = item.SoThongBaoTienThueDat,
                    ViTriThuaDat = item.ViTriThuaDat,
                    DienTichKhongPhaiNop = item.DienTichKhongPhaiNop.ToString("N", new CultureInfo("vi-VN")),
                    DienTichPhaiNop = item.DienTichPhaiNop.ToString("N", new CultureInfo("vi-VN")),
                    DonGia = item.DonGia.ToString("N0", new CultureInfo("vi-VN")),
                    NgayThongBaoTienThueDat = item.NgayThongBaoTienThueDat != null ? item.NgayThongBaoTienThueDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    SoTien = item.SoTien.ToString("N0", new CultureInfo("vi-VN")),
                    SoTienMienGiam = item.SoTienMienGiam.ToString("N0", new CultureInfo("vi-VN")),
                    SoTienPhaiNop = item.SoTienPhaiNop.ToString("N0", new CultureInfo("vi-VN")),
                    LoaiThongBaoTienThueDat = item.LoaiThongBaoTienThueDat,
                    Nam = item.Nam,
                };
                result.Add(ThongBaoTienThueDat);
            }
            return new ApiSuccessResult<List<ThongBaoTienThueDatViewModel>>() { Data = result };
        }

        public async Task<ApiResult<PageViewModel<ThongBaoTienThueDatViewModel>>> GetAllPaging(int? idDoanhNghiep, string keyword, int pageIndex, int pageSize)
        {
            var query = from a in _context.ThongBaoTienThueDat.Include(x => x.DoanhNghiep).Include(x => x.DoanhNghiep)
                        select a;
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => (x.SoThongBaoTienThueDat.ToLower().Contains(keyword.ToLower())) || (x.DoanhNghiep.TenDoanhNghiep.ToLower().Contains(keyword.ToLower())) || (x.DoanhNghiep.MaSoThue.ToLower().Contains(keyword.ToLower())));
            }
            if (idDoanhNghiep != null)
            {
                query = query.Where(x => x.IdDoanhNghiep == idDoanhNghiep);
            }
            var data = await query.OrderByDescending(x => x.Nam).ThenBy(x=> x.SoThongBaoTienThueDat)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();
            var listItem = new List<ThongBaoTienThueDatViewModel>();
            foreach (var item in data)
            {
                var x = item.DoanhNghiep;
                var ThongBaoTienThueDat = new ThongBaoTienThueDatViewModel
                {
                    IdThongBaoTienThueDat = item.IdThongBaoTienThueDat,
                    IdDoanhNghiep = item.IdDoanhNghiep,
                    TenDoanhNghiep = item.DoanhNghiep.TenDoanhNghiep,
                    SoThongBaoTienThueDat = item.SoThongBaoTienThueDat,
                    ViTriThuaDat = item.ViTriThuaDat,
                    DienTichKhongPhaiNop = item.DienTichKhongPhaiNop.ToString("N", new CultureInfo("vi-VN")),
                    DienTichPhaiNop = item.DienTichPhaiNop.ToString("N", new CultureInfo("vi-VN")),
                    DonGia = item.DonGia.ToString("N0", new CultureInfo("vi-VN")),
                    NgayThongBaoTienThueDat = item.NgayThongBaoTienThueDat != null ? item.NgayThongBaoTienThueDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    SoTien = item.SoTien.ToString("N0", new CultureInfo("vi-VN")),
                    SoTienMienGiam = item.SoTienMienGiam.ToString("N0", new CultureInfo("vi-VN")),
                    SoTienPhaiNop = item.SoTienPhaiNop.ToString("N0", new CultureInfo("vi-VN")),
                    LoaiThongBaoTienThueDat = item.LoaiThongBaoTienThueDat,
                    Nam = item.Nam,
                };
                listItem.Add(ThongBaoTienThueDat);
            }
            var result = new PageViewModel<ThongBaoTienThueDatViewModel>()
            {
                Items = listItem,
                PageIndex = pageIndex,
                TotalRecord = query.Count(),
                PageSize = pageSize
            };
            return new ApiSuccessResult<PageViewModel<ThongBaoTienThueDatViewModel>>() { Data = result };
        }

        public async Task<ApiResult<ThongBaoTienThueDatViewModel>> GetById(int idThongBaoTienThueDat)
        {
            var result = new ThongBaoTienThueDatViewModel();
            var entity = await _context.ThongBaoTienThueDat.Include(x => x.DoanhNghiep).FirstOrDefaultAsync(x => x.IdThongBaoTienThueDat == idThongBaoTienThueDat);
            if (entity != null)
            {
                result = new ThongBaoTienThueDatViewModel
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
                    SoThongBaoDonGiaThueDat = entity.SoThongBaoDonGiaThueDat,
                    TenThongBaoDonGiaThueDat = entity.TenThongBaoDonGiaThueDat,
                    NgayThongBaoDonGiaThueDat = entity.NgayThongBaoDonGiaThueDat != null ? entity.NgayThongBaoDonGiaThueDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    ViTriThuaDat = entity.ViTriThuaDat,
                    DienTichKhongPhaiNop = entity.DienTichKhongPhaiNop.ToString("N", new CultureInfo("vi-VN")),
                    DienTichPhaiNop = entity.DienTichPhaiNop.ToString("N", new CultureInfo("vi-VN")),
                    DonGia = entity.DonGia.ToString("N0", new CultureInfo("vi-VN")),
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
                return new ApiSuccessResult<ThongBaoTienThueDatViewModel>() { Data = result };

            }
            else
            {
                return new ApiErrorResult<ThongBaoTienThueDatViewModel>("Không tìm thấy dữ liệu");
            }
        }
    }
}
