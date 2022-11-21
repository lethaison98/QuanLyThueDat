using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
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
    public class DoanhNghiepService : IDoanhNghiepService
    {
        private readonly QuanLyThueDatDbContext _context;
        private readonly IQuyetDinhThueDatService _QuyetDinhThueDatService;
        private readonly IHopDongThueDatService _HopDongThueDatService;
        private readonly IQuyetDinhMienTienThueDatService _QuyetDinhMienTienThueDatService;
        private readonly IThongBaoDonGiaThueDatService _ThongBaoDonGiaThueDatService;
        private readonly IThongBaoTienThueDatService _ThongBaoTienThueDatService;

        public DoanhNghiepService(QuanLyThueDatDbContext context,
            IQuyetDinhThueDatService QuyetDinhThueDatService,
            IHopDongThueDatService HopDongThueDatService,
            IQuyetDinhMienTienThueDatService QuyetDinhMienTienThueDatService,
            IThongBaoDonGiaThueDatService ThongBaoDonGiaThueDatService,
            IThongBaoTienThueDatService ThongBaoTienThueDatService)
        {
            _context = context;
            _QuyetDinhThueDatService = QuyetDinhThueDatService;
            _HopDongThueDatService = HopDongThueDatService;
            _QuyetDinhMienTienThueDatService = QuyetDinhMienTienThueDatService;
            _ThongBaoDonGiaThueDatService = ThongBaoDonGiaThueDatService;
            _ThongBaoTienThueDatService = ThongBaoTienThueDatService;
        }

        public async Task<ApiResult<int>> InsertUpdate(DoanhNghiepRequest rq)
        {
            var result = 0;
            var entity = _context.DoanhNghiep.FirstOrDefault(x => x.IdDoanhNghiep == rq.IdDoanhNghiep);
            if (entity == null)
            {
                entity = new DoanhNghiep()
                {
                    TenDoanhNghiep = rq.TenDoanhNghiep,
                    DiaChi = rq.DiaChi,
                    SoDienThoai = rq.SoDienThoai,
                    Email = rq.Email,
                    TenNguoiDaiDien = rq.TenNguoiDaiDien,
                    CoQuanQuanLyThue = rq.CoQuanQuanLyThue,
                    MaSoThue = rq.MaSoThue,
                    NgayCap = string.IsNullOrEmpty(rq.NgayCap) ? null : DateTime.Parse(rq.NgayCap, new CultureInfo("vi-VN")),
                    NoiCap = rq.NoiCap,
                };
            }
            else
            {
                entity.IdDoanhNghiep = rq.IdDoanhNghiep;
                entity.TenDoanhNghiep = rq.TenDoanhNghiep;
                entity.DiaChi = rq.DiaChi;
                entity.SoDienThoai = rq.SoDienThoai;
                entity.Email = rq.Email;
                entity.TenNguoiDaiDien = rq.TenNguoiDaiDien;
                entity.CoQuanQuanLyThue = rq.CoQuanQuanLyThue;
                entity.MaSoThue = rq.MaSoThue;
                entity.NgayCap = string.IsNullOrEmpty(rq.NgayCap) ? null : DateTime.Parse(rq.NgayCap, new CultureInfo("vi-VN"));
                entity.NoiCap = rq.NoiCap;
            }

            _context.DoanhNghiep.Update(entity);
            await _context.SaveChangesAsync();
            result = entity.IdDoanhNghiep;
            return new ApiSuccessResult<int>() { Data = result };
        }

        public async Task<ApiResult<bool>> Delete(int idDoanhNghiep)
        {
            var result = false;
            var data = _context.DoanhNghiep.FirstOrDefault(x => x.IdDoanhNghiep == idDoanhNghiep);
            if (data != null)
            {
                _context.DoanhNghiep.Remove(data);
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

        public async Task<ApiResult<List<DoanhNghiepViewModel>>> GetAll()
        {
            var result = new List<DoanhNghiepViewModel>();
            var data = await _context.DoanhNghiep.ToListAsync();
            foreach (var item in data)
            {
                var doanhNghiep = new DoanhNghiepViewModel
                {
                    IdDoanhNghiep = item.IdDoanhNghiep,
                    TenDoanhNghiep = item.TenDoanhNghiep,
                    DiaChi = item.DiaChi,
                    SoDienThoai = item.SoDienThoai,
                    Email = item.Email,
                    TenNguoiDaiDien = item.TenNguoiDaiDien,
                    CoQuanQuanLyThue = item.CoQuanQuanLyThue,
                    MaSoThue = item.MaSoThue,
                    NgayCap = item.NgayCap != null ? item.NgayCap.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    NoiCap = item.NoiCap,
                };
                result.Add(doanhNghiep);
            }
            return new ApiSuccessResult<List<DoanhNghiepViewModel>>() { Data = result };
        }

        public async Task<ApiResult<PageViewModel<DoanhNghiepViewModel>>> GetAllPaging(string keyword, int pageIndex, int pageSize)
        {
            var query = from a in _context.DoanhNghiep
                        select a;
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.TenDoanhNghiep.ToLower().Contains(keyword.ToLower()) || x.MaSoThue.ToLower().Contains(keyword.ToLower()));
            }
            var data = await query.OrderByDescending(x => x.TenDoanhNghiep)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();
            var listItem = new List<DoanhNghiepViewModel>();
            foreach (var item in data)
            {
                var doanhNghiep = new DoanhNghiepViewModel
                {
                    IdDoanhNghiep = item.IdDoanhNghiep,
                    TenDoanhNghiep = item.TenDoanhNghiep,
                    DiaChi = item.DiaChi,
                    SoDienThoai = item.SoDienThoai,
                    Email = item.Email,
                    TenNguoiDaiDien = item.TenNguoiDaiDien,
                    CoQuanQuanLyThue = item.CoQuanQuanLyThue,
                    MaSoThue = item.MaSoThue,
                    NgayCap = item.NgayCap != null ? item.NgayCap.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    NoiCap = item.NoiCap,
                };
                listItem.Add(doanhNghiep);
            }
            var result = new PageViewModel<DoanhNghiepViewModel>()
            {
                Items = listItem,
                PageIndex = pageIndex,
                TotalRecord = query.Count(),
                PageSize = pageSize
            };
            return new ApiSuccessResult<PageViewModel<DoanhNghiepViewModel>>() { Data = result };
        }

        public async Task<ApiResult<DoanhNghiepViewModel>> GetById(int idDoanhNghiep)
        {
            var result = new DoanhNghiepViewModel();
            var entity = await _context.DoanhNghiep.FirstOrDefaultAsync(x => x.IdDoanhNghiep == idDoanhNghiep);
            if (entity != null)
            {
                result = new DoanhNghiepViewModel
                {
                    IdDoanhNghiep = entity.IdDoanhNghiep,
                    TenDoanhNghiep = entity.TenDoanhNghiep,
                    DiaChi = entity.DiaChi,
                    SoDienThoai = entity.SoDienThoai,
                    Email = entity.Email,
                    TenNguoiDaiDien = entity.TenNguoiDaiDien,
                    CoQuanQuanLyThue = entity.CoQuanQuanLyThue,
                    MaSoThue = entity.MaSoThue,
                    NgayCap = entity.NgayCap != null ? entity.NgayCap.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                    NoiCap = entity.NoiCap,
                };
                return new ApiSuccessResult<DoanhNghiepViewModel>() { Data = result };

            }
            else
            {
                return new ApiErrorResult<DoanhNghiepViewModel>("Không tìm thấy dữ liệu");
            }
        }
        public async Task<ApiResult<List<string>>> ImportDoanhNghiep(IList<IFormFile> files)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            var result = new List<string>();
            var ok = 0;
            List<DoanhNghiepViewModel> users = new List<DoanhNghiepViewModel>();
            var listSuccess = new List<ImportDoanhNghiepRequest>();
            var listError = new List<string>();
            var listException = new List<string>();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = new MemoryStream())
            {
                files[0].CopyTo(stream);
                using (var package = new ExcelPackage(stream))
                {
                    var currentSheet = package.Workbook.Worksheets;
                    var workSheet = currentSheet[0];
                    var noOfRow = workSheet.Dimension.End.Row;
                    for (int i = 4; i <= workSheet.Dimension.End.Row; i++)
                    {
                        try
                        {
                            var imp = new ImportDoanhNghiepRequest();
                            //Doanh nghiệp
                            imp.DoanhNghiepRequest.TenDoanhNghiep = workSheet.Cells[i, 2].Value == null ? "" : workSheet.Cells[i, 2].Value.ToString();
                            imp.DoanhNghiepRequest.MaSoThue = workSheet.Cells[i, 3].Value == null ? "" : workSheet.Cells[i, 3].Value.ToString();
                            imp.DoanhNghiepRequest.DiaChi = workSheet.Cells[i, 4].Value == null ? "" : workSheet.Cells[i, 4].Value.ToString();

                            //Quyết định thuê đất
                            imp.QuyetDinhThueDatRequest.SoQuyetDinhThueDat = workSheet.Cells[i, 11].Value == null ? "" : workSheet.Cells[i, 11].Value.ToString();
                            var col12 = workSheet.Cells[i, 12].Value;
                            if(col12 != null)
                            {
                                var date = Convert.ToDateTime(col12, new CultureInfo("vi-VN"));
                                imp.QuyetDinhThueDatRequest.NgayQuyetDinhThueDat = date.ToString("dd/MM/yyyy");
                            }
                            else
                            {
                                imp.QuyetDinhThueDatRequest.NgayQuyetDinhThueDat = "";
                            }
                            //workSheet.Cells[i, 12].Style.Numberformat.Format = "dd/MM/yyyy";
                            //imp.QuyetDinhThueDatRequest.NgayQuyetDinhThueDat = workSheet.Cells[i, 12].Value == null ? "" :  workSheet.Cells[i, 12].Value.ToString().Split(' ')[0];
                            imp.QuyetDinhThueDatRequest.TongDienTich = workSheet.Cells[i, 13].Value == null ? 0 : Convert.ToDecimal(workSheet.Cells[i, 13].Value);
                            imp.QuyetDinhThueDatRequest.ThoiHanThue = workSheet.Cells[i, 14].Value == null ? "" : workSheet.Cells[i, 14].Value.ToString();
                            imp.QuyetDinhThueDatRequest.MucDichSuDung = workSheet.Cells[i, 15].Value == null ? "" : workSheet.Cells[i, 15].Value.ToString();
                            imp.QuyetDinhThueDatRequest.ViTriThuaDat = "Thửa số " + (workSheet.Cells[i, 16].Value == null ? "" : workSheet.Cells[i, 16].Value.ToString()) + ", Tờ bản đồ số " + (workSheet.Cells[i, 17].Value == null ? "" : workSheet.Cells[i, 17].Value.ToString());
                            imp.QuyetDinhThueDatRequest.DiaChiThuaDat = workSheet.Cells[i, 18].Value == null ? "" : workSheet.Cells[i, 18].Value.ToString();

                            //Hợp đồng thuê đất
                            imp.HopDongThueDatRequest.SoHopDong = workSheet.Cells[i, 19].Value == null ? "" : workSheet.Cells[i, 19].Value.ToString();
                            var col20 = workSheet.Cells[i, 12].Value;
                            if (col20 != null)
                            {
                                var date = Convert.ToDateTime(col20, new CultureInfo("vi-VN"));
                                imp.HopDongThueDatRequest.NgayKyHopDong = date.ToString("dd/MM/yyyy");
                            }
                            else
                            {
                                imp.HopDongThueDatRequest.NgayKyHopDong = "";
                            }
                            //imp.HopDongThueDatRequest.NgayKyHopDong = workSheet.Cells[i, 20].Value == null ? "" : workSheet.Cells[i, 20].Value.ToString();
                            //imp.HopDongThueDatRequest.DienTich = workSheet.Cells[i, 21].Value == null ? 0 : Convert.ToDecimal(workSheet.Cells[i, 21].Value.ToString(), new CultureInfo("vi-VN"));

                            // Thông báo đơn giá thuê đất
                            var col22 = workSheet.Cells[i, 22].Value == null ? "" : workSheet.Cells[i, 22].Value.ToString();
                            if (col22 != "")
                            {
                                var arr = col22.Split(new string[] { "ngày" }, StringSplitOptions.None);
                                imp.ThongBaoDonGiaThueDatRequest.NgayThongBaoDonGiaThueDat = arr[1].ToString().Trim();
                                var soTB = arr[0].Trim().Split(' ');
                                imp.ThongBaoDonGiaThueDatRequest.SoThongBaoDonGiaThueDat = soTB[soTB.Length - 1].ToString().Trim();
                            }
                            else
                            {
                                imp.ThongBaoDonGiaThueDatRequest.NgayThongBaoDonGiaThueDat = "";
                                imp.ThongBaoDonGiaThueDatRequest.SoThongBaoDonGiaThueDat = "";
                            }
                            imp.ThongBaoDonGiaThueDatRequest.DonGia = workSheet.Cells[i, 23].Value == null ? 0 : Convert.ToDecimal(workSheet.Cells[i, 23].Value);
                            var col24 = workSheet.Cells[i, 24].Value == null ? "" : workSheet.Cells[i, 24].Value.ToString();
                            var onDinhDonGia = col24.Split(new string[] { "từ", "đến" }, StringSplitOptions.None);
                            imp.ThongBaoDonGiaThueDatRequest.ThoiHanDonGia = onDinhDonGia[0].ToString().Trim();
                            imp.ThongBaoDonGiaThueDatRequest.NgayHieuLucDonGiaThueDat = onDinhDonGia[1].Trim().Split(' ').Last();
                            imp.ThongBaoDonGiaThueDatRequest.NgayHetHieuLucDonGiaThueDat = onDinhDonGia[2].Trim().Split(' ').Last();

                            imp.ThongBaoDonGiaThueDatRequest.SoQuyetDinhThueDat = imp.QuyetDinhThueDatRequest.SoQuyetDinhThueDat;
                            imp.ThongBaoDonGiaThueDatRequest.NgayQuyetDinhThueDat = imp.QuyetDinhThueDatRequest.NgayQuyetDinhThueDat;
                            imp.ThongBaoDonGiaThueDatRequest.TongDienTich = imp.QuyetDinhThueDatRequest.TongDienTich;
                            imp.ThongBaoDonGiaThueDatRequest.ThoiHanThue = imp.QuyetDinhThueDatRequest.ThoiHanThue;
                            imp.ThongBaoDonGiaThueDatRequest.MucDichSuDung = imp.QuyetDinhThueDatRequest.MucDichSuDung;
                            imp.ThongBaoDonGiaThueDatRequest.ViTriThuaDat = imp.QuyetDinhThueDatRequest.ViTriThuaDat;
                            imp.ThongBaoDonGiaThueDatRequest.DiaChiThuaDat = imp.QuyetDinhThueDatRequest.DiaChiThuaDat;

                            // Thông báo tiền thuê đất 2018
                            var col32 = workSheet.Cells[i, 32].Value == null ? "" : workSheet.Cells[i, 32].Value.ToString();
                            if (col32 != "")
                            {
                                var arr = col32.Split(new string[] { "ngày" }, StringSplitOptions.None);
                                imp.ThongBaoTienThueDat2018Request.NgayThongBaoTienThueDat = arr[1].ToString().Trim();
                                var soTB = arr[0].Trim().Split(' ');
                                imp.ThongBaoTienThueDat2018Request.SoThongBaoTienThueDat = soTB[soTB.Length - 1].ToString().Trim();
                            }
                            else
                            {
                                imp.ThongBaoTienThueDat2018Request.NgayThongBaoTienThueDat = "";
                                imp.ThongBaoTienThueDat2018Request.SoThongBaoTienThueDat = "";
                            }
                            imp.ThongBaoTienThueDat2018Request.Nam = 2018;
                            imp.ThongBaoTienThueDat2018Request.SoTien = workSheet.Cells[i, 33].Value == null ? 0 : Convert.ToDecimal(workSheet.Cells[i, 33].Value);
                            imp.ThongBaoTienThueDat2018Request.SoTienMienGiam = workSheet.Cells[i, 34].Value == null ? 0 : Convert.ToDecimal(workSheet.Cells[i, 34].Value);
                            imp.ThongBaoTienThueDat2018Request.SoTienPhaiNop = workSheet.Cells[i, 35].Value == null ? 0 : Convert.ToDecimal(workSheet.Cells[i, 35].Value);

                            imp.ThongBaoTienThueDat2018Request.SoQuyetDinhThueDat = imp.QuyetDinhThueDatRequest.SoQuyetDinhThueDat;
                            imp.ThongBaoTienThueDat2018Request.NgayQuyetDinhThueDat = imp.QuyetDinhThueDatRequest.NgayQuyetDinhThueDat;
                            imp.ThongBaoTienThueDat2018Request.TongDienTich = imp.QuyetDinhThueDatRequest.TongDienTich;
                            imp.ThongBaoTienThueDat2018Request.ThoiHanThue = imp.QuyetDinhThueDatRequest.ThoiHanThue;
                            imp.ThongBaoTienThueDat2018Request.MucDichSuDung = imp.QuyetDinhThueDatRequest.MucDichSuDung;
                            imp.ThongBaoTienThueDat2018Request.ViTriThuaDat = imp.QuyetDinhThueDatRequest.ViTriThuaDat;
                            imp.ThongBaoTienThueDat2018Request.DiaChiThuaDat = imp.QuyetDinhThueDatRequest.DiaChiThuaDat;

                            imp.ThongBaoTienThueDat2018Request.NgayThongBaoDonGiaThueDat = imp.ThongBaoDonGiaThueDatRequest.NgayThongBaoDonGiaThueDat;
                            imp.ThongBaoTienThueDat2018Request.SoThongBaoDonGiaThueDat = imp.ThongBaoDonGiaThueDatRequest.SoThongBaoDonGiaThueDat;
                            imp.ThongBaoTienThueDat2018Request.DonGia = imp.ThongBaoDonGiaThueDatRequest.DonGia;

                            // Thông báo tiền thuê đất 2019
                            var col36 = workSheet.Cells[i, 36].Value == null ? "" : workSheet.Cells[i, 36].Value.ToString();
                            if (col36 != "")
                            {
                                var arr = col36.Split(new string[] { "ngày" }, StringSplitOptions.None);
                                imp.ThongBaoTienThueDat2019Request.NgayThongBaoTienThueDat = arr[1].ToString().Trim();
                                var soTB = arr[0].Trim().Split(' ');
                                imp.ThongBaoTienThueDat2019Request.SoThongBaoTienThueDat = soTB[soTB.Length - 1].ToString().Trim();
                            }
                            else
                            {
                                imp.ThongBaoTienThueDat2019Request.NgayThongBaoTienThueDat = "";
                                imp.ThongBaoTienThueDat2019Request.SoThongBaoTienThueDat = "";
                            }
                            imp.ThongBaoTienThueDat2019Request.Nam = 2019;
                            imp.ThongBaoTienThueDat2019Request.SoTien = workSheet.Cells[i, 37].Value == null ? 0 : Convert.ToDecimal(workSheet.Cells[i, 37].Value);
                            imp.ThongBaoTienThueDat2019Request.SoTienMienGiam = workSheet.Cells[i, 38].Value == null ? 0 : Convert.ToDecimal(workSheet.Cells[i, 38].Value);
                            imp.ThongBaoTienThueDat2019Request.SoTienPhaiNop = workSheet.Cells[i, 39].Value == null ? 0 : Convert.ToDecimal(workSheet.Cells[i, 39].Value);

                            imp.ThongBaoTienThueDat2019Request.SoQuyetDinhThueDat = imp.QuyetDinhThueDatRequest.SoQuyetDinhThueDat;
                            imp.ThongBaoTienThueDat2019Request.NgayQuyetDinhThueDat = imp.QuyetDinhThueDatRequest.NgayQuyetDinhThueDat;
                            imp.ThongBaoTienThueDat2019Request.TongDienTich = imp.QuyetDinhThueDatRequest.TongDienTich;
                            imp.ThongBaoTienThueDat2019Request.ThoiHanThue = imp.QuyetDinhThueDatRequest.ThoiHanThue;
                            imp.ThongBaoTienThueDat2019Request.MucDichSuDung = imp.QuyetDinhThueDatRequest.MucDichSuDung;
                            imp.ThongBaoTienThueDat2019Request.ViTriThuaDat = imp.QuyetDinhThueDatRequest.ViTriThuaDat;
                            imp.ThongBaoTienThueDat2019Request.DiaChiThuaDat = imp.QuyetDinhThueDatRequest.DiaChiThuaDat;

                            imp.ThongBaoTienThueDat2019Request.NgayThongBaoDonGiaThueDat = imp.ThongBaoDonGiaThueDatRequest.NgayThongBaoDonGiaThueDat;
                            imp.ThongBaoTienThueDat2019Request.SoThongBaoDonGiaThueDat = imp.ThongBaoDonGiaThueDatRequest.SoThongBaoDonGiaThueDat;
                            imp.ThongBaoTienThueDat2019Request.DonGia = imp.ThongBaoDonGiaThueDatRequest.DonGia;
                            // Thông báo tiền thuê đất 2020
                            var col40 = workSheet.Cells[i, 40].Value == null ? "" : workSheet.Cells[i, 40].Value.ToString();
                            if (col40 != "")
                            {
                                var arr = col40.Split(new string[] { "ngày" }, StringSplitOptions.None);
                                imp.ThongBaoTienThueDat2020Request.NgayThongBaoTienThueDat = arr[1].ToString().Trim();
                                var soTB = arr[0].Trim().Split(' ');
                                imp.ThongBaoTienThueDat2020Request.SoThongBaoTienThueDat = soTB[soTB.Length - 1].ToString().Trim();
                            }
                            else
                            {
                                imp.ThongBaoTienThueDat2020Request.NgayThongBaoTienThueDat = "";
                                imp.ThongBaoTienThueDat2020Request.SoThongBaoTienThueDat = "";
                            }
                            imp.ThongBaoTienThueDat2020Request.Nam = 2020;
                            imp.ThongBaoTienThueDat2020Request.SoTien = workSheet.Cells[i, 41].Value == null ? 0 : Convert.ToDecimal(workSheet.Cells[i, 41].Value);
                            imp.ThongBaoTienThueDat2020Request.SoTienMienGiam = workSheet.Cells[i, 42].Value == null ? 0 : Convert.ToDecimal(workSheet.Cells[i, 42].Value);
                            imp.ThongBaoTienThueDat2020Request.SoTienPhaiNop = workSheet.Cells[i, 43].Value == null ? 0 : Convert.ToDecimal(workSheet.Cells[i, 43].Value);

                            imp.ThongBaoTienThueDat2020Request.SoQuyetDinhThueDat = imp.QuyetDinhThueDatRequest.SoQuyetDinhThueDat;
                            imp.ThongBaoTienThueDat2020Request.NgayQuyetDinhThueDat = imp.QuyetDinhThueDatRequest.NgayQuyetDinhThueDat;
                            imp.ThongBaoTienThueDat2020Request.TongDienTich = imp.QuyetDinhThueDatRequest.TongDienTich;
                            imp.ThongBaoTienThueDat2020Request.ThoiHanThue = imp.QuyetDinhThueDatRequest.ThoiHanThue;
                            imp.ThongBaoTienThueDat2020Request.MucDichSuDung = imp.QuyetDinhThueDatRequest.MucDichSuDung;
                            imp.ThongBaoTienThueDat2020Request.ViTriThuaDat = imp.QuyetDinhThueDatRequest.ViTriThuaDat;
                            imp.ThongBaoTienThueDat2020Request.DiaChiThuaDat = imp.QuyetDinhThueDatRequest.DiaChiThuaDat;

                            imp.ThongBaoTienThueDat2020Request.NgayThongBaoDonGiaThueDat = imp.ThongBaoDonGiaThueDatRequest.NgayThongBaoDonGiaThueDat;
                            imp.ThongBaoTienThueDat2020Request.SoThongBaoDonGiaThueDat = imp.ThongBaoDonGiaThueDatRequest.SoThongBaoDonGiaThueDat;
                            imp.ThongBaoTienThueDat2020Request.DonGia = imp.ThongBaoDonGiaThueDatRequest.DonGia;

                            // Thông báo tiền thuê đất 2021
                            var col44 = workSheet.Cells[i, 44].Value == null ? "" : workSheet.Cells[i, 44].Value.ToString();
                            if (col44 != "")
                            {
                                var arr = col44.Split(new string[] { "ngày" }, StringSplitOptions.None);
                                imp.ThongBaoTienThueDat2021Request.NgayThongBaoTienThueDat = arr[1].ToString().Trim();
                                var soTB = arr[0].Trim().Split(' ');
                                imp.ThongBaoTienThueDat2021Request.SoThongBaoTienThueDat = soTB[soTB.Length - 1].ToString().Trim();
                            }
                            else
                            {
                                imp.ThongBaoTienThueDat2021Request.NgayThongBaoTienThueDat = "";
                                imp.ThongBaoTienThueDat2021Request.SoThongBaoTienThueDat = "";
                            }
                            imp.ThongBaoTienThueDat2021Request.Nam = 2021;
                            imp.ThongBaoTienThueDat2021Request.SoTien = workSheet.Cells[i, 45].Value == null ? 0 : Convert.ToDecimal(workSheet.Cells[i, 45].Value);
                            imp.ThongBaoTienThueDat2021Request.SoTienMienGiam = workSheet.Cells[i, 46].Value == null ? 0 : Convert.ToDecimal(workSheet.Cells[i, 46].Value);
                            imp.ThongBaoTienThueDat2021Request.SoTienPhaiNop = workSheet.Cells[i, 47].Value == null ? 0 : Convert.ToDecimal(workSheet.Cells[i, 47].Value);

                            imp.ThongBaoTienThueDat2021Request.SoQuyetDinhThueDat = imp.QuyetDinhThueDatRequest.SoQuyetDinhThueDat;
                            imp.ThongBaoTienThueDat2021Request.NgayQuyetDinhThueDat = imp.QuyetDinhThueDatRequest.NgayQuyetDinhThueDat;
                            imp.ThongBaoTienThueDat2021Request.TongDienTich = imp.QuyetDinhThueDatRequest.TongDienTich;
                            imp.ThongBaoTienThueDat2021Request.ThoiHanThue = imp.QuyetDinhThueDatRequest.ThoiHanThue;
                            imp.ThongBaoTienThueDat2021Request.MucDichSuDung = imp.QuyetDinhThueDatRequest.MucDichSuDung;
                            imp.ThongBaoTienThueDat2021Request.ViTriThuaDat = imp.QuyetDinhThueDatRequest.ViTriThuaDat;
                            imp.ThongBaoTienThueDat2021Request.DiaChiThuaDat = imp.QuyetDinhThueDatRequest.DiaChiThuaDat;

                            imp.ThongBaoTienThueDat2021Request.NgayThongBaoDonGiaThueDat = imp.ThongBaoDonGiaThueDatRequest.NgayThongBaoDonGiaThueDat;
                            imp.ThongBaoTienThueDat2021Request.SoThongBaoDonGiaThueDat = imp.ThongBaoDonGiaThueDatRequest.SoThongBaoDonGiaThueDat;
                            imp.ThongBaoTienThueDat2021Request.DonGia = imp.ThongBaoDonGiaThueDatRequest.DonGia;

                            // Thông báo tiền thuê đất 2022
                            var col48 = workSheet.Cells[i, 48].Value == null ? "" : workSheet.Cells[i, 48].Value.ToString();
                            if (col48 != "")
                            {
                                var arr = col48.Split(new string[] { "ngày" }, StringSplitOptions.None);
                                imp.ThongBaoTienThueDat2022Request.NgayThongBaoTienThueDat = arr[1].ToString().Trim();
                                var soTB = arr[0].Trim().Split(' ');
                                imp.ThongBaoTienThueDat2022Request.SoThongBaoTienThueDat = soTB[soTB.Length - 1].ToString().Trim();
                            }
                            else
                            {
                                imp.ThongBaoTienThueDat2022Request.NgayThongBaoTienThueDat = "";
                                imp.ThongBaoTienThueDat2022Request.SoThongBaoTienThueDat = "";
                            }
                            imp.ThongBaoTienThueDat2022Request.Nam = 2022;
                            imp.ThongBaoTienThueDat2022Request.SoTien = workSheet.Cells[i, 49].Value == null ? 0 : Convert.ToDecimal(workSheet.Cells[i, 49].Value);
                            imp.ThongBaoTienThueDat2022Request.SoTienMienGiam = workSheet.Cells[i, 50].Value == null ? 0 : Convert.ToDecimal(workSheet.Cells[i, 50].Value);
                            imp.ThongBaoTienThueDat2022Request.SoTienPhaiNop = workSheet.Cells[i, 51].Value == null ? 0 : Convert.ToDecimal(workSheet.Cells[i, 51].Value);

                            imp.ThongBaoTienThueDat2022Request.SoQuyetDinhThueDat = imp.QuyetDinhThueDatRequest.SoQuyetDinhThueDat;
                            imp.ThongBaoTienThueDat2022Request.NgayQuyetDinhThueDat = imp.QuyetDinhThueDatRequest.NgayQuyetDinhThueDat;
                            imp.ThongBaoTienThueDat2022Request.TongDienTich = imp.QuyetDinhThueDatRequest.TongDienTich;
                            imp.ThongBaoTienThueDat2022Request.ThoiHanThue = imp.QuyetDinhThueDatRequest.ThoiHanThue;
                            imp.ThongBaoTienThueDat2022Request.MucDichSuDung = imp.QuyetDinhThueDatRequest.MucDichSuDung;
                            imp.ThongBaoTienThueDat2022Request.ViTriThuaDat = imp.QuyetDinhThueDatRequest.ViTriThuaDat;
                            imp.ThongBaoTienThueDat2022Request.DiaChiThuaDat = imp.QuyetDinhThueDatRequest.DiaChiThuaDat;

                            imp.ThongBaoTienThueDat2022Request.NgayThongBaoDonGiaThueDat = imp.ThongBaoDonGiaThueDatRequest.NgayThongBaoDonGiaThueDat;
                            imp.ThongBaoTienThueDat2022Request.SoThongBaoDonGiaThueDat = imp.ThongBaoDonGiaThueDatRequest.SoThongBaoDonGiaThueDat;
                            imp.ThongBaoTienThueDat2022Request.DonGia = imp.ThongBaoDonGiaThueDatRequest.DonGia;
                            listSuccess.Add(imp);
                        }
                        catch (Exception e)
                        {
                            listError.Add("Lỗi tại dòng: " + i);
                            listException.Add(e.Message);
                            continue;
                        }
                    }
                }
            }

            // Lưu vào DB
            foreach (var item in listSuccess)
            {
                try
                {
                    var idDoanhNghiep = 0;
                    var checkDN = _context.DoanhNghiep.FirstOrDefault(x => x.MaSoThue == item.DoanhNghiepRequest.MaSoThue);
                    if (checkDN == null)
                    {
                        var saveDoanhNghiep = await InsertUpdate(item.DoanhNghiepRequest);
                        idDoanhNghiep = saveDoanhNghiep.Data;
                        listException.Add("Thêm doanh nghiệp thành công---"+item.DoanhNghiepRequest.TenDoanhNghiep);
                    }
                    else
                    {
                        idDoanhNghiep = checkDN.IdDoanhNghiep;
                    }
                    item.QuyetDinhThueDatRequest.IdDoanhNghiep = idDoanhNghiep;
                    var quyetDinhThueDat = await _QuyetDinhThueDatService.InsertUpdate(item.QuyetDinhThueDatRequest);
                    listException.Add("Thêm quyết định thuê đất thành công---" + item.DoanhNghiepRequest.TenDoanhNghiep);

                    item.HopDongThueDatRequest.IdDoanhNghiep = idDoanhNghiep;
                    var hopDongThueDat = await _HopDongThueDatService.InsertUpdate(item.HopDongThueDatRequest);
                    listException.Add("Thêm hợp đồng thành công---" + item.DoanhNghiepRequest.TenDoanhNghiep);

                    item.ThongBaoDonGiaThueDatRequest.IdDoanhNghiep = idDoanhNghiep;
                    var tbDonGiaThueDat = await _ThongBaoDonGiaThueDatService.InsertUpdate(item.ThongBaoDonGiaThueDatRequest);
                    listException.Add("Thêm thông báo đơn giá thành công---" + item.DoanhNghiepRequest.TenDoanhNghiep);

                    if (!string.IsNullOrEmpty(item.ThongBaoTienThueDat2018Request.SoThongBaoTienThueDat))
                    {
                        item.ThongBaoTienThueDat2018Request.IdDoanhNghiep = idDoanhNghiep;
                        var tbTienThueDat = await _ThongBaoTienThueDatService.InsertUpdate(item.ThongBaoTienThueDat2018Request);
                        listException.Add("Thêm tiền 2018 thành công---" + item.DoanhNghiepRequest.TenDoanhNghiep);

                    }
                    if (!string.IsNullOrEmpty(item.ThongBaoTienThueDat2019Request.SoThongBaoTienThueDat))
                    {
                        item.ThongBaoTienThueDat2019Request.IdDoanhNghiep = idDoanhNghiep;
                        var tbTienThueDat = await _ThongBaoTienThueDatService.InsertUpdate(item.ThongBaoTienThueDat2019Request);
                        listException.Add("Thêm tiền 2019 thành công---" + item.DoanhNghiepRequest.TenDoanhNghiep);

                    }
                    if (!string.IsNullOrEmpty(item.ThongBaoTienThueDat2020Request.SoThongBaoTienThueDat))
                    {
                        item.ThongBaoTienThueDat2020Request.IdDoanhNghiep = idDoanhNghiep;
                        var tbTienThueDat = await _ThongBaoTienThueDatService.InsertUpdate(item.ThongBaoTienThueDat2020Request);
                        listException.Add("Thêm tiền 2020 thành công---" + item.DoanhNghiepRequest.TenDoanhNghiep);

                    }
                    if (!string.IsNullOrEmpty(item.ThongBaoTienThueDat2021Request.SoThongBaoTienThueDat))
                    {
                        item.ThongBaoTienThueDat2021Request.IdDoanhNghiep = idDoanhNghiep;
                        var tbTienThueDat = await _ThongBaoTienThueDatService.InsertUpdate(item.ThongBaoTienThueDat2021Request);
                        listException.Add("Thêm tiền 2021 thành công---" + item.DoanhNghiepRequest.TenDoanhNghiep);
                        listException.Add(item.ThongBaoTienThueDat2022Request.SoThongBaoTienThueDat +"----"+ item.ThongBaoTienThueDat2022Request.NgayThongBaoTienThueDat + "----" + item.ThongBaoTienThueDat2022Request.SoTien + "----" + item.ThongBaoTienThueDat2022Request.SoTienMienGiam + "----" + item.ThongBaoTienThueDat2022Request.SoTienPhaiNop);
                    }
                    if (!string.IsNullOrEmpty(item.ThongBaoTienThueDat2022Request.SoThongBaoTienThueDat))
                    {
                        item.ThongBaoTienThueDat2022Request.IdDoanhNghiep = idDoanhNghiep;
                        var tbTienThueDat = await _ThongBaoTienThueDatService.InsertUpdate(item.ThongBaoTienThueDat2022Request);
                        listException.Add("Thêm tiền 2022 thành công---" + item.DoanhNghiepRequest.TenDoanhNghiep);

                    }
                    ok++;

                }
                catch (Exception e)
                {
                    listError.Add(item.DoanhNghiepRequest.TenDoanhNghiep + " lỗi khi lưu vào DB");
                    listException.Add(e.Message);
                }

            }
            result.Add(ok + " Doanh nghiệp được nhập hoàn chỉnh");
            result.AddRange(listError);
            result.AddRange(listException);
            return new ApiSuccessResult<List<string>>() { Data = result };
        }
    }
}
