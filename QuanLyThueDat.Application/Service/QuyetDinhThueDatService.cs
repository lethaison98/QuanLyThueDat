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
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Application.Service
{
    public class QuyetDinhThueDatService : IQuyetDinhThueDatService
    {
        private readonly QuanLyThueDatDbContext _context;

        public QuyetDinhThueDatService(QuanLyThueDatDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<int>> InsertUpdate(QuyetDinhThueDatRequest rq)
        {
            var result = 0;
            var entity = _context.QuyetDinhThueDat.Include(x => x.DsQuyetDinhThueDatChiTiet).FirstOrDefault(x => x.IdQuyetDinhThueDat == rq.IdQuyetDinhThueDat);
            if (entity == null)
            {
                entity = new QuyetDinhThueDat()
                {
                    IdQuyetDinhThueDat = rq.IdQuyetDinhThueDat,
                    IdDoanhNghiep = rq.IdDoanhNghiep,
                    SoQuyetDinhThueDat = rq.SoQuyetDinhThueDat,
                    TenQuyetDinhThueDat = rq.TenQuyetDinhThueDat,
                    NgayQuyetDinhThueDat = string.IsNullOrEmpty(rq.NgayQuyetDinhThueDat) ? null : DateTime.Parse(rq.NgayQuyetDinhThueDat, new CultureInfo("vi-VN")),
                    SoQuyetDinhGiaoDat = rq.SoQuyetDinhGiaoDat,
                    TenQuyetDinhGiaoDat = rq.TenQuyetDinhGiaoDat,
                    NgayQuyetDinhGiaoDat = string.IsNullOrEmpty(rq.NgayQuyetDinhGiaoDat) ? null : DateTime.Parse(rq.NgayQuyetDinhGiaoDat, new CultureInfo("vi-VN")),
                    TongDienTich = rq.TongDienTich,
                    ViTriThuaDat = rq.ViTriThuaDat,
                    DiaChiThuaDat = rq.DiaChiThuaDat
                    //ThoiHanThue = rq.ThoiHanThue,
                    //TuNgayThue = string.IsNullOrEmpty(rq.TuNgayThue) ? null : DateTime.Parse(rq.TuNgayThue, new CultureInfo("vi-VN")),
                    //DenNgayThue = string.IsNullOrEmpty(rq.DenNgayThue) ? null : DateTime.Parse(rq.DenNgayThue, new CultureInfo("vi-VN")),
                    //MucDichSuDung = rq.MucDichSuDung,
                    //HinhThucThue = rq.HinhThucThue,

                };
            }
            else
            {
                entity.IdQuyetDinhThueDat = rq.IdQuyetDinhThueDat;
                entity.IdDoanhNghiep = rq.IdDoanhNghiep;
                entity.SoQuyetDinhThueDat = rq.SoQuyetDinhThueDat;
                entity.TenQuyetDinhThueDat = rq.TenQuyetDinhThueDat;
                entity.NgayQuyetDinhThueDat = string.IsNullOrEmpty(rq.NgayQuyetDinhThueDat) ? null : DateTime.Parse(rq.NgayQuyetDinhThueDat, new CultureInfo("vi-VN"));
                entity.SoQuyetDinhGiaoDat = rq.SoQuyetDinhGiaoDat;
                entity.TenQuyetDinhGiaoDat = rq.TenQuyetDinhGiaoDat;
                entity.NgayQuyetDinhGiaoDat = string.IsNullOrEmpty(rq.NgayQuyetDinhGiaoDat) ? null : DateTime.Parse(rq.NgayQuyetDinhGiaoDat, new CultureInfo("vi-VN"));
                entity.TongDienTich = rq.TongDienTich;
                entity.ViTriThuaDat = rq.ViTriThuaDat;
                entity.DiaChiThuaDat = rq.DiaChiThuaDat;
                //entity.ThoiHanThue = rq.ThoiHanThue;
                //entity.TuNgayThue = string.IsNullOrEmpty(rq.TuNgayThue) ? null : DateTime.Parse(rq.TuNgayThue, new CultureInfo("vi-VN"));
                //entity.DenNgayThue = string.IsNullOrEmpty(rq.DenNgayThue) ? null : DateTime.Parse(rq.DenNgayThue, new CultureInfo("vi-VN"));
                //entity.MucDichSuDung = rq.MucDichSuDung;
                //entity.HinhThucThue = rq.HinhThucThue;

            }
            var listQuyetDinhThueDatChiTiet = new List<QuyetDinhThueDatChiTiet>();
            if (rq.QuyetDinhThueDatChiTiet.Count > 0)
            {
                foreach (var item in rq.QuyetDinhThueDatChiTiet)
                {
                    var ct = new QuyetDinhThueDatChiTiet();
                    ct.IdQuyetDinhThueDat = item.IdQuyetDinhThueDat;
                    ct.IdQuyetDinhThueDatChiTiet = item.IdQuyetDinhThueDatChiTiet;
                    ct.HinhThucThue = item.HinhThucThue; ;
                    ct.MucDichSuDung = item.MucDichSuDung;
                    ct.DienTich = item.DienTich;
                    ct.ThoiHanThue = item.ThoiHanThue;
                    ct.TuNgayThue = string.IsNullOrEmpty(item.TuNgayThue) ? null : DateTime.Parse(item.TuNgayThue, new CultureInfo("vi-VN"));
                    ct.DenNgayThue = string.IsNullOrEmpty(item.DenNgayThue) ? null : DateTime.Parse(item.DenNgayThue, new CultureInfo("vi-VN"));
                    listQuyetDinhThueDatChiTiet.Add(ct);
                }
            }
            entity.DsQuyetDinhThueDatChiTiet = listQuyetDinhThueDatChiTiet;
            _context.QuyetDinhThueDat.Update(entity);
            var listIdOldFile = rq.FileTaiLieu.Where(x => x.IdFileTaiLieu > 0).Select(x=> x.IdFileTaiLieu);
            var listRemoveFile = _context.FileTaiLieu.Where(x => x.IdTaiLieu == entity.IdQuyetDinhThueDat && x.IdLoaiTaiLieu == NhomLoaiTaiLieuConstant.NhomQuyetDinhThueDat && !listIdOldFile.Contains(x.IdFileTaiLieu));
            foreach(var item in listRemoveFile)
            {
                item.TrangThai = 4;
            }
            _context.FileTaiLieu.UpdateRange(listRemoveFile);

            var listNewFile = rq.FileTaiLieu.Where(x => x.IdFileTaiLieu == 0);
            foreach(var item in listNewFile)
            {
                item.IdTaiLieu = entity.IdQuyetDinhThueDat;
                item.IdLoaiTaiLieu = NhomLoaiTaiLieuConstant.NhomQuyetDinhThueDat;
                item.TrangThai = 1;
                item.NgayTao = DateTime.Now;
            }
            _context.FileTaiLieu.AddRange(listNewFile);
            await _context.SaveChangesAsync();
            result = entity.IdQuyetDinhThueDat;
            return new ApiSuccessResult<int>() { Data = result };
        }

        public async Task<ApiResult<bool>> Delete(int idQuyetDinhThueDat)
        {
            var result = false;
            var data = _context.QuyetDinhThueDat.FirstOrDefault(x => x.IdQuyetDinhThueDat == idQuyetDinhThueDat);
            if (data != null)
            {
                _context.QuyetDinhThueDat.Remove(data);
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

        public async Task<ApiResult<List<QuyetDinhThueDatViewModel>>> GetAll(int? idDoanhNghiep)
        {
            var result = new List<QuyetDinhThueDatViewModel>();
            var data = new List<QuyetDinhThueDat>();
            if (idDoanhNghiep == null)
            {
                data = await _context.QuyetDinhThueDat.Include(x => x.DoanhNghiep).ToListAsync();
            }
            else
            {
                data = await _context.QuyetDinhThueDat.Include(x => x.DoanhNghiep).Where(x => x.IdDoanhNghiep == idDoanhNghiep).ToListAsync();
            }

            foreach (var item in data)
            {
                var QuyetDinhThueDat = new QuyetDinhThueDatViewModel
                {
                    IdQuyetDinhThueDat = item.IdQuyetDinhThueDat,
                    IdDoanhNghiep = item.IdDoanhNghiep,
                    TenDoanhNghiep = item.DoanhNghiep.TenDoanhNghiep,
                    SoQuyetDinhThueDat = item.SoQuyetDinhThueDat,
                    TenQuyetDinhThueDat = item.TenQuyetDinhThueDat,
                    NgayQuyetDinhThueDat = item.NgayQuyetDinhThueDat != null ? item.NgayQuyetDinhThueDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    SoQuyetDinhGiaoDat = item.SoQuyetDinhGiaoDat,
                    TenQuyetDinhGiaoDat = item.TenQuyetDinhGiaoDat,
                    NgayQuyetDinhGiaoDat = item.NgayQuyetDinhGiaoDat != null ? item.NgayQuyetDinhGiaoDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    TongDienTich = item.TongDienTich.ToString("N", new CultureInfo("vi-VN")),
                    ThoiHanThue = item.ThoiHanThue,
                    TuNgayThue = item.TuNgayThue != null ? item.TuNgayThue.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    DenNgayThue = item.DenNgayThue != null ? item.DenNgayThue.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    MucDichSuDung = item.MucDichSuDung,
                    HinhThucThue = item.HinhThucThue,
                    ViTriThuaDat = item.ViTriThuaDat,
                    DiaChiThuaDat = item.DiaChiThuaDat
                };
                result.Add(QuyetDinhThueDat);
            }
            return new ApiSuccessResult<List<QuyetDinhThueDatViewModel>>() { Data = result };
        }

        public async Task<ApiResult<PageViewModel<QuyetDinhThueDatViewModel>>> GetAllPaging(int? idDoanhNghiep, string keyword, int pageIndex, int pageSize)
        {
            var query = from a in _context.QuyetDinhThueDat.Include(x => x.DoanhNghiep)
                        select a;
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.TenQuyetDinhThueDat.ToLower().Contains(keyword.ToLower()) || x.DoanhNghiep.MaSoThue.ToLower().Contains(keyword.ToLower())|| x.DoanhNghiep.TenDoanhNghiep.ToLower().Contains(keyword.ToLower()));
            }
            if (idDoanhNghiep != null)
            {
                query = query.Where(x => x.IdDoanhNghiep == idDoanhNghiep);
            }
            var data = await query.OrderByDescending(x => x.TenQuyetDinhThueDat)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();
            var listItem = new List<QuyetDinhThueDatViewModel>();
            foreach (var entity in data)
            {
                var quyetDinhThueDat = new QuyetDinhThueDatViewModel
                {
                    IdQuyetDinhThueDat = entity.IdQuyetDinhThueDat,
                    IdDoanhNghiep = entity.IdDoanhNghiep,
                    TenDoanhNghiep = entity.DoanhNghiep.TenDoanhNghiep,
                    SoQuyetDinhThueDat = entity.SoQuyetDinhThueDat,
                    TenQuyetDinhThueDat = entity.TenQuyetDinhThueDat,
                    NgayQuyetDinhThueDat = entity.NgayQuyetDinhThueDat != null ? entity.NgayQuyetDinhThueDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    SoQuyetDinhGiaoDat = entity.SoQuyetDinhGiaoDat,
                    TenQuyetDinhGiaoDat = entity.TenQuyetDinhGiaoDat,
                    NgayQuyetDinhGiaoDat = entity.NgayQuyetDinhGiaoDat != null ? entity.NgayQuyetDinhGiaoDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    TongDienTich = entity.TongDienTich.ToString("N", new CultureInfo("vi-VN")),
                    ThoiHanThue = entity.ThoiHanThue,
                    TuNgayThue = entity.TuNgayThue != null ? entity.TuNgayThue.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    DenNgayThue = entity.DenNgayThue != null ? entity.DenNgayThue.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    MucDichSuDung = entity.MucDichSuDung,
                    HinhThucThue = entity.HinhThucThue,
                    ViTriThuaDat = entity.ViTriThuaDat,
                    DiaChiThuaDat = entity.DiaChiThuaDat
                };
                var listFileViewModel = new List<FileTaiLieuViewModel>();
                var listFile = _context.FileTaiLieu.Include(x => x.File).Where(x => x.IdLoaiTaiLieu == NhomLoaiTaiLieuConstant.NhomQuyetDinhThueDat && x.IdTaiLieu == entity.IdQuyetDinhThueDat && x.TrangThai != 4).ToList();
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
                quyetDinhThueDat.DsFileTaiLieu = listFileViewModel;
                listItem.Add(quyetDinhThueDat);
            }
            var result = new PageViewModel<QuyetDinhThueDatViewModel>()
            {
                Items = listItem,
                PageIndex = pageIndex,
                TotalRecord = query.Count(),
                PageSize = pageSize
            };
            return new ApiSuccessResult<PageViewModel<QuyetDinhThueDatViewModel>>() { Data = result };
        }

        public async Task<ApiResult<QuyetDinhThueDatViewModel>> GetById(int idQuyetDinhThueDat)
        {
            var result = new QuyetDinhThueDatViewModel();
            var entity = await _context.QuyetDinhThueDat.Include(x => x.DoanhNghiep).Include(x => x.DsQuyetDinhThueDatChiTiet).FirstOrDefaultAsync(x => x.IdQuyetDinhThueDat == idQuyetDinhThueDat);
            if (entity != null)
            {
                result = new QuyetDinhThueDatViewModel
                {
                    IdQuyetDinhThueDat = entity.IdQuyetDinhThueDat,
                    IdDoanhNghiep = entity.IdDoanhNghiep,
                    TenDoanhNghiep = entity.DoanhNghiep.TenDoanhNghiep,
                    SoQuyetDinhThueDat = entity.SoQuyetDinhThueDat,
                    TenQuyetDinhThueDat = entity.TenQuyetDinhThueDat,
                    NgayQuyetDinhThueDat = entity.NgayQuyetDinhThueDat != null ? entity.NgayQuyetDinhThueDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    SoQuyetDinhGiaoDat = entity.SoQuyetDinhGiaoDat,
                    TenQuyetDinhGiaoDat = entity.TenQuyetDinhGiaoDat,
                    NgayQuyetDinhGiaoDat = entity.NgayQuyetDinhGiaoDat != null ? entity.NgayQuyetDinhGiaoDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    TongDienTich = entity.TongDienTich.ToString("N", new CultureInfo("vi-VN")),
                    ViTriThuaDat = entity.ViTriThuaDat,
                    DiaChiThuaDat = entity.DiaChiThuaDat,
                    //ThoiHanThue = entity.ThoiHanThue,
                    //TuNgayThue = entity.TuNgayThue != null ? entity.TuNgayThue.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    //DenNgayThue = entity.DenNgayThue != null ? entity.DenNgayThue.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    //MucDichSuDung = entity.MucDichSuDung,
                    //HinhThucThue = entity.HinhThucThue,
                };
                if (entity.DsQuyetDinhThueDatChiTiet != null)
                {
                    var listct = new List<QuyetDinhThueDatChiTietViewModel>();
                    foreach (var item in entity.DsQuyetDinhThueDatChiTiet)
                    {
                        var ctVM = new QuyetDinhThueDatChiTietViewModel();
                        ctVM.IdQuyetDinhThueDatChiTiet = item.IdQuyetDinhThueDatChiTiet;
                        ctVM.IdQuyetDinhThueDat = item.IdQuyetDinhThueDat;
                        ctVM.HinhThucThue = item.HinhThucThue;
                        ctVM.TextHinhThucThue = typeof(LoaiQuyetDinhThueDatConstant).GetField(item.HinhThucThue).GetValue(null).ToString();
                        ctVM.ThoiHanThue = item.ThoiHanThue;
                        ctVM.MucDichSuDung = item.MucDichSuDung;
                        ctVM.DienTich = item.DienTich.ToString("N", new CultureInfo("vi-VN"));
                        ctVM.TuNgayThue = item.TuNgayThue != null ? item.TuNgayThue.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "";
                        ctVM.DenNgayThue = item.DenNgayThue != null ? item.DenNgayThue.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "";
                        listct.Add(ctVM);
                    }
                    result.DsQuyetDinhThueDatChiTiet = listct;
                    var listFileViewModel = new List<FileTaiLieuViewModel>();
                    var listFile = _context.FileTaiLieu.Include(x => x.File).Where(x => x.IdLoaiTaiLieu == NhomLoaiTaiLieuConstant.NhomQuyetDinhThueDat && x.IdTaiLieu == entity.IdQuyetDinhThueDat && x.TrangThai != 4).ToList();
                    foreach(var item in listFile)
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
                    
                }
                return new ApiSuccessResult<QuyetDinhThueDatViewModel>() { Data = result };

            }
            else
            {
                return new ApiErrorResult<QuyetDinhThueDatViewModel>("Không tìm thấy dữ liệu");
            }
        }
        public async Task<ApiResult<List<QuyetDinhThueDatViewModel>>> GetListQuyetDinhThueDatChiTiet(int idDoanhNghiep)
        {
            var result = new List<QuyetDinhThueDatViewModel>();
            var data = new List<QuyetDinhThueDat>();

            data = await _context.QuyetDinhThueDat.Include(x => x.DoanhNghiep).Include(x=> x.DsQuyetDinhThueDatChiTiet).Where(x => x.IdDoanhNghiep == idDoanhNghiep).ToListAsync();
            
            foreach (var item in data)
            {
                if(item.DsQuyetDinhThueDatChiTiet.Count == 0)
                {
                    var QuyetDinhThueDat = new QuyetDinhThueDatViewModel
                    {
                        IdQuyetDinhThueDat = item.IdQuyetDinhThueDat,
                        IdDoanhNghiep = item.IdDoanhNghiep,
                        TenDoanhNghiep = item.DoanhNghiep.TenDoanhNghiep,
                        SoQuyetDinhThueDat = item.SoQuyetDinhThueDat,
                        TenQuyetDinhThueDat = item.TenQuyetDinhThueDat,
                        NgayQuyetDinhThueDat = item.NgayQuyetDinhThueDat != null ? item.NgayQuyetDinhThueDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                        SoQuyetDinhGiaoDat = item.SoQuyetDinhGiaoDat,
                        TenQuyetDinhGiaoDat = item.TenQuyetDinhGiaoDat,
                        NgayQuyetDinhGiaoDat = item.NgayQuyetDinhGiaoDat != null ? item.NgayQuyetDinhGiaoDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                        TongDienTich = item.TongDienTich.ToString("N", new CultureInfo("vi-VN")),
                        ThoiHanThue = item.ThoiHanThue,
                        TuNgayThue = item.TuNgayThue != null ? item.TuNgayThue.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                        DenNgayThue = item.DenNgayThue != null ? item.DenNgayThue.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                        MucDichSuDung = item.MucDichSuDung,
                        HinhThucThue = item.HinhThucThue,
                        ViTriThuaDat = item.ViTriThuaDat,
                        DiaChiThuaDat = item.DiaChiThuaDat
                    };
                    result.Add(QuyetDinhThueDat);
                }
                else
                {
                    foreach(var ct in item.DsQuyetDinhThueDatChiTiet)
                    {
                        var QuyetDinhThueDat = new QuyetDinhThueDatViewModel
                        {
                            IdQuyetDinhThueDat = item.IdQuyetDinhThueDat,
                            IdDoanhNghiep = item.IdDoanhNghiep,
                            TenDoanhNghiep = item.DoanhNghiep.TenDoanhNghiep,
                            SoQuyetDinhThueDat = item.SoQuyetDinhThueDat,
                            TenQuyetDinhThueDat = item.TenQuyetDinhThueDat ,
                            NgayQuyetDinhThueDat = item.NgayQuyetDinhThueDat != null ? item.NgayQuyetDinhThueDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                            SoQuyetDinhGiaoDat = item.SoQuyetDinhGiaoDat,
                            TenQuyetDinhGiaoDat = item.TenQuyetDinhGiaoDat,
                            NgayQuyetDinhGiaoDat = item.NgayQuyetDinhGiaoDat != null ? item.NgayQuyetDinhGiaoDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                            ViTriThuaDat = item.ViTriThuaDat,
                            DiaChiThuaDat = item.DiaChiThuaDat,
                            TongDienTich = ct.DienTich.ToString("N", new CultureInfo("vi-VN")),
                            ThoiHanThue = ct.ThoiHanThue,
                            TuNgayThue = ct.TuNgayThue != null ? ct.TuNgayThue.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                            DenNgayThue = ct.DenNgayThue != null ? ct.DenNgayThue.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                            MucDichSuDung = ct.MucDichSuDung,
                            HinhThucThue = ct.HinhThucThue,
                            TextHinhThucThue = typeof(LoaiQuyetDinhThueDatConstant).GetField(ct.HinhThucThue).GetValue(null).ToString(),
                        };
                        result.Add(QuyetDinhThueDat);
                    }
                }         
            }
            return new ApiSuccessResult<List<QuyetDinhThueDatViewModel>>() { Data = result };
        }
    }
}
