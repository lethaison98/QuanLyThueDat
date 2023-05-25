using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OfficeOpenXml;
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
    public class DoanhNghiepService : IDoanhNghiepService
    {
        private readonly QuanLyThueDatDbContext _context;
        private readonly IQuyetDinhThueDatService _QuyetDinhThueDatService;
        private readonly IThongBaoTienSuDungDatService _ThongBaoTienSuDungDatService;
        private readonly IHopDongThueDatService _HopDongThueDatService;
        private readonly IQuyetDinhMienTienThueDatService _QuyetDinhMienTienThueDatService;
        private readonly IThongBaoDonGiaThueDatService _ThongBaoDonGiaThueDatService;
        private readonly IThongBaoTienThueDatService _ThongBaoTienThueDatService;
        public IHttpContextAccessor _accessor { get; set; }

        public DoanhNghiepService(QuanLyThueDatDbContext context,
            IQuyetDinhThueDatService QuyetDinhThueDatService,
            IHopDongThueDatService HopDongThueDatService,
            IQuyetDinhMienTienThueDatService QuyetDinhMienTienThueDatService,
            IThongBaoDonGiaThueDatService ThongBaoDonGiaThueDatService,
            IThongBaoTienThueDatService ThongBaoTienThueDatService,
            IThongBaoTienSuDungDatService ThongBaoTienSuDungDatService,
            IHttpContextAccessor HttpContextAccessor )
        {
            _context = context;
            _QuyetDinhThueDatService = QuyetDinhThueDatService;
            _ThongBaoTienSuDungDatService = ThongBaoTienSuDungDatService;
            _HopDongThueDatService = HopDongThueDatService;
            _QuyetDinhMienTienThueDatService = QuyetDinhMienTienThueDatService;
            _ThongBaoDonGiaThueDatService = ThongBaoDonGiaThueDatService;
            _ThongBaoTienThueDatService = ThongBaoTienThueDatService;
            _accessor = HttpContextAccessor;
        }

        public async Task<ApiResult<int>> InsertUpdate(DoanhNghiepRequest rq)
        {
            var claimsIdentity = _accessor.HttpContext.User.Identity as ClaimsIdentity;
            var tenUser = claimsIdentity.FindFirst("HoTen")?.Value;
            var userId = claimsIdentity.FindFirst("UserId")?.Value;
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
                    GhiChu = rq.GhiChu,
                    TenChuong = rq.TenChuong,
                    MaChuong = rq.MaChuong,
                    MaCoQuanQuanLyThu = rq.MaCoQuanQuanLyThu,
                    NgayTao = DateTime.Now,
                    NguoiTao = tenUser,
                    IdNguoiTao = userId
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
                entity.GhiChu = rq.GhiChu;
                entity.TenChuong = rq.TenChuong;
                entity.MaChuong = rq.MaChuong;
                entity.MaCoQuanQuanLyThu = rq.MaCoQuanQuanLyThu;
                entity.NgayCapNhat = DateTime.Now;
                entity.NguoiCapNhat = tenUser;
                entity.IdNguoiCapNhat = userId;
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
                data.IsDeleted = true;
                //_context.DoanhNghiep.Remove(data);
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
            var data = await _context.DoanhNghiep.Where(x=>!x.IsDeleted).ToListAsync();
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
                    GhiChu = item.GhiChu
                };
                result.Add(doanhNghiep);
            }
            return new ApiSuccessResult<List<DoanhNghiepViewModel>>() { Data = result };
        }

        public async Task<ApiResult<PageViewModel<DoanhNghiepViewModel>>> GetAllPaging(string keyword, int pageIndex, int pageSize)
        {
            var query = from a in _context.DoanhNghiep.Where(x => !x.IsDeleted)
                        select a;
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.TenDoanhNghiep.ToLower().Contains(keyword.ToLower()) || x.MaSoThue.ToLower().Contains(keyword.ToLower()));
            }
            var data = await query.OrderByDescending(x => x.IdDoanhNghiep)
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
                    GhiChu = item.GhiChu,
                    TenChuong = item.TenChuong,
                    MaChuong = item.MaChuong,
                    MaCoQuanQuanLyThu = item.MaCoQuanQuanLyThu,

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
                    GhiChu = entity.GhiChu,
                    TenChuong = entity.TenChuong,
                    MaChuong = entity.MaChuong,
                    MaCoQuanQuanLyThu = entity.MaCoQuanQuanLyThu,
                };
                return new ApiSuccessResult<DoanhNghiepViewModel>() { Data = result };

            }
            else
            {
                return new ApiErrorResult<DoanhNghiepViewModel>("Không tìm thấy dữ liệu");
            }
        }
        public async Task<ApiResult<List<string>>> ImportDoanhNghiepTheoBieuLapBo(IList<IFormFile> files)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            var result = new List<string>();
            var ok = 0;
            List<DoanhNghiepViewModel> users = new List<DoanhNghiepViewModel>();
            var listSuccess = new List<ImportDuLieuRequest>();
            var messageSuccess = new List<string>();
            var messageError = new List<string>();
            var messageException = new List<string>();
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
                        var j = 2;
                        try
                        {
                            var imp = new ImportDuLieuRequest();
                            //Doanh nghiệp
                            imp.DoanhNghiepRequest.TenDoanhNghiep = workSheet.Cells[i, j].Value == null ? "" : workSheet.Cells[i, j].Value.ToString();
                            imp.DoanhNghiepRequest.CoQuanQuanLyThue = "Chi cục thuế "+ workSheet.Name;
                            j++;
                            imp.DoanhNghiepRequest.MaSoThue = workSheet.Cells[i, j].Value == null ? "" : workSheet.Cells[i, j].Value.ToString();
                            j++;
                            imp.DoanhNghiepRequest.DiaChi = workSheet.Cells[i, j].Value == null ? "" : workSheet.Cells[i, j].Value.ToString();
                            j++;

                            //Quyết định giao đất UBND tỉnh
                            var quyetDinhThueDatChiTiet = new QuyetDinhThueDatChiTietRequest();
                            var col5 = workSheet.Cells[i, j].Value == null ? "" : workSheet.Cells[i, j].Value.ToString();
                            if (col5 != "")
                            {
                                var arr = col5.Split(new string[] { "ngày" }, StringSplitOptions.None);
                                imp.QuyetDinhThueDatRequest.NgayQuyetDinhGiaoDat = arr[1].ToString().Trim();
                                var soTB = arr[0].Trim().Split(' ');
                                imp.QuyetDinhThueDatRequest.SoQuyetDinhGiaoDat = soTB[soTB.Length - 1].ToString().Trim();
                            }
                            else
                            {
                                imp.ThongBaoDonGiaThueDatRequest.NgayThongBaoDonGiaThueDat = "";
                                imp.ThongBaoDonGiaThueDatRequest.SoThongBaoDonGiaThueDat = "";
                            }
                            j++;

                            //Quyết định thuê đất
                            j += 5;
                            imp.QuyetDinhThueDatRequest.SoQuyetDinhThueDat = workSheet.Cells[i, j].Value == null ? "" : workSheet.Cells[i, j].Value.ToString();
                            j++; //12
                            var col12 = workSheet.Cells[i, j].Value;
                            if (col12 != null)
                            {
                                var date = Convert.ToDateTime(col12, new CultureInfo("vi-VN"));
                                imp.QuyetDinhThueDatRequest.NgayQuyetDinhThueDat = date.ToString("dd/MM/yyyy");
                            }
                            else
                            {
                                imp.QuyetDinhThueDatRequest.NgayQuyetDinhThueDat = "";
                            }
                            j++;
                            //workSheet.Cells[i, 12].Style.Numberformat.Format = "dd/MM/yyyy";
                            //imp.QuyetDinhThueDatRequest.NgayQuyetDinhThueDat = workSheet.Cells[i, 12].Value == null ? "" :  workSheet.Cells[i, 12].Value.ToString().Split(' ')[0];

                            imp.QuyetDinhThueDatRequest.TongDienTich = workSheet.Cells[i, j].Value == null ? 0 : Convert.ToDecimal(workSheet.Cells[i, j].Value);
                            j++;
                            imp.QuyetDinhThueDatRequest.ThoiHanThue = workSheet.Cells[i, j].Value == null ? "" : workSheet.Cells[i, j].Value.ToString();
                            j++;
                            imp.QuyetDinhThueDatRequest.MucDichSuDung = workSheet.Cells[i, j].Value == null ? "" : workSheet.Cells[i, j].Value.ToString();
                            quyetDinhThueDatChiTiet.MucDichSuDung = imp.QuyetDinhThueDatRequest.MucDichSuDung;
                            quyetDinhThueDatChiTiet.ThoiHanThue = imp.QuyetDinhThueDatRequest.ThoiHanThue;
                            quyetDinhThueDatChiTiet.HinhThucThue = "ThueDatTraTienHangNam";
                            quyetDinhThueDatChiTiet.DienTich = imp.QuyetDinhThueDatRequest.TongDienTich;
                            imp.QuyetDinhThueDatRequest.QuyetDinhThueDatChiTiet = new List<QuyetDinhThueDatChiTietRequest>{ quyetDinhThueDatChiTiet };
                            j++;
                            var soThua = (workSheet.Cells[i, j].Value == null ? "" : workSheet.Cells[i, j].Value.ToString());
                            j++;
                            var soTo = (workSheet.Cells[i, j].Value == null ? "" : workSheet.Cells[i, j].Value.ToString());
                            imp.QuyetDinhThueDatRequest.ViTriThuaDat = "Thửa số " + soThua+ ", Tờ bản đồ số " + soTo;
                            j++;
                            imp.QuyetDinhThueDatRequest.DiaChiThuaDat = workSheet.Cells[i, j].Value == null ? "" : workSheet.Cells[i, j].Value.ToString();
                            j++;

                            //Hợp đồng thuê đất
                            imp.HopDongThueDatRequest.SoHopDong = workSheet.Cells[i, j].Value == null ? "" : workSheet.Cells[i, j].Value.ToString();
                            j++;//20
                            var col20 = workSheet.Cells[i, j].Value;
                            if (col20 != null)
                            {
                                var date = Convert.ToDateTime(col20, new CultureInfo("vi-VN"));
                                imp.HopDongThueDatRequest.NgayKyHopDong = date.ToString("dd/MM/yyyy");
                            }
                            else
                            {
                                imp.HopDongThueDatRequest.NgayKyHopDong = "";
                            }
                            j++;
                            //imp.HopDongThueDatRequest.NgayKyHopDong = workSheet.Cells[i, 20].Value == null ? "" : workSheet.Cells[i, 20].Value.ToString();
                            //imp.HopDongThueDatRequest.DienTich = workSheet.Cells[i, 21].Value == null ? 0 : Convert.ToDecimal(workSheet.Cells[i, 21].Value.ToString(), new CultureInfo("vi-VN"));

                            j++;//22
                            // Thông báo đơn giá thuê đất
                            var col22 = workSheet.Cells[i, j].Value == null ? "" : workSheet.Cells[i, j].Value.ToString();
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
                            j++;

                            imp.ThongBaoDonGiaThueDatRequest.DonGia = workSheet.Cells[i, j].Value == null ? 0 : Convert.ToDecimal(workSheet.Cells[i, j].Value);
                            j++;

                            var col24 = workSheet.Cells[i, j].Value == null ? "" : workSheet.Cells[i, j].Value.ToString();
                            if (col24 != "")
                            {
                                var onDinhDonGia = col24.Split(new string[] { "từ", "đến" }, StringSplitOptions.None);
                                imp.ThongBaoDonGiaThueDatRequest.ThoiHanDonGia = onDinhDonGia[0].ToString().Trim();
                                imp.ThongBaoDonGiaThueDatRequest.NgayHieuLucDonGiaThueDat = onDinhDonGia[1].Trim().Split(' ').Last();
                                imp.ThongBaoDonGiaThueDatRequest.NgayHetHieuLucDonGiaThueDat = onDinhDonGia[2].Trim().Split(' ').Last();
                            }
                            imp.ThongBaoDonGiaThueDatRequest.SoQuyetDinhThueDat = imp.QuyetDinhThueDatRequest.SoQuyetDinhThueDat;
                            imp.ThongBaoDonGiaThueDatRequest.NgayQuyetDinhThueDat = imp.QuyetDinhThueDatRequest.NgayQuyetDinhThueDat;
                            imp.ThongBaoDonGiaThueDatRequest.TongDienTich = imp.QuyetDinhThueDatRequest.TongDienTich;
                            imp.ThongBaoDonGiaThueDatRequest.ThoiHanThue = imp.QuyetDinhThueDatRequest.ThoiHanThue;
                            imp.ThongBaoDonGiaThueDatRequest.MucDichSuDung = imp.QuyetDinhThueDatRequest.MucDichSuDung;
                            imp.ThongBaoDonGiaThueDatRequest.ViTriThuaDat = imp.QuyetDinhThueDatRequest.ViTriThuaDat;
                            imp.ThongBaoDonGiaThueDatRequest.DiaChiThuaDat = imp.QuyetDinhThueDatRequest.DiaChiThuaDat;

                            j++;
                            // Quyết định miễn tiền thuê đất
                            var col25 = workSheet.Cells[i, j].Value == null ? "" : workSheet.Cells[i, j].Value.ToString();
                            if (col25 != "")
                            {
                                var arr = col25.Split(new string[] { "ngày" }, StringSplitOptions.None);
                                imp.QuyetDinhMienTienThueDatRequest.NgayQuyetDinhMienTienThueDat = arr[1].ToString().Trim();
                                var soTB = arr[0].Trim().Split(' ');
                                imp.QuyetDinhMienTienThueDatRequest.SoQuyetDinhMienTienThueDat = soTB[soTB.Length - 1].ToString().Trim();
                            }
                            else
                            {
                                imp.QuyetDinhMienTienThueDatRequest.NgayQuyetDinhMienTienThueDat = "";
                                imp.QuyetDinhMienTienThueDatRequest.SoQuyetDinhMienTienThueDat = "";
                            }
                            j++;
                            imp.QuyetDinhMienTienThueDatRequest.ThoiHanMienTienThueDat = workSheet.Cells[i, j].Value == null ? "" : workSheet.Cells[i, j].Value.ToString();
                            j++;

                            var col27 = workSheet.Cells[i, j].Value == null ? "" : workSheet.Cells[i, j].Value.ToString();
                            if (col27 != "")
                            {
                                var ngayMienTien = col27.Split(new string[] { "từ", "đến" }, StringSplitOptions.None);
                                imp.QuyetDinhMienTienThueDatRequest.NgayHieuLucMienTienThueDat = ngayMienTien[0].Trim().Split(' ').Last();
                                imp.QuyetDinhMienTienThueDatRequest.NgayHetHieuLucMienTienThueDat = ngayMienTien[1].Trim().Split(' ').Last();
                            }
                            else
                            {
                                imp.QuyetDinhMienTienThueDatRequest.NgayHieuLucMienTienThueDat = "";
                                imp.QuyetDinhMienTienThueDatRequest.NgayHetHieuLucMienTienThueDat = "";

                            }

                            j++;

                            imp.QuyetDinhMienTienThueDatRequest.DienTichMienTienThueDat = workSheet.Cells[i, j].Value == null ? 0 : Convert.ToDecimal(workSheet.Cells[i, j].Value);
                            j++;
                            j += 3;//32
                            
                            // Thông báo tiền thuê đất 2018
                            var col32 = workSheet.Cells[i, j].Value == null ? "" : workSheet.Cells[i, j].Value.ToString();
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
                            j++;
                            imp.ThongBaoTienThueDat2018Request.Nam = 2018;
                            imp.ThongBaoTienThueDat2018Request.SoTien = workSheet.Cells[i, j].Value == null ? 0 : Convert.ToDecimal(workSheet.Cells[i, j].Value);
                            j++;
                            imp.ThongBaoTienThueDat2018Request.SoTienMienGiam = workSheet.Cells[i, j].Value == null ? 0 : Convert.ToDecimal(workSheet.Cells[i, j].Value);
                            j++;
                            imp.ThongBaoTienThueDat2018Request.SoTienPhaiNop = workSheet.Cells[i, j].Value == null ? 0 : Convert.ToDecimal(workSheet.Cells[i, j].Value);
                            j++;

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
                            var col36 = workSheet.Cells[i, j].Value == null ? "" : workSheet.Cells[i, j].Value.ToString();
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
                            j++;
                            imp.ThongBaoTienThueDat2019Request.SoTien = workSheet.Cells[i, j].Value == null ? 0 : Convert.ToDecimal(workSheet.Cells[i, j].Value);
                            j++;
                            imp.ThongBaoTienThueDat2019Request.SoTienMienGiam = workSheet.Cells[i, j].Value == null ? 0 : Convert.ToDecimal(workSheet.Cells[i, j].Value);
                            j++;
                            imp.ThongBaoTienThueDat2019Request.SoTienPhaiNop = workSheet.Cells[i, j].Value == null ? 0 : Convert.ToDecimal(workSheet.Cells[i, j].Value);
                            j++;
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
                            var col40 = workSheet.Cells[i, j].Value == null ? "" : workSheet.Cells[i, j].Value.ToString();
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
                            j++;
                            imp.ThongBaoTienThueDat2020Request.Nam = 2020;
                            imp.ThongBaoTienThueDat2020Request.SoTien = workSheet.Cells[i, j].Value == null ? 0 : Convert.ToDecimal(workSheet.Cells[i, j].Value);
                            j++;
                            imp.ThongBaoTienThueDat2020Request.SoTienMienGiam = workSheet.Cells[i, j].Value == null ? 0 : Convert.ToDecimal(workSheet.Cells[i, j].Value);
                            j++;
                            imp.ThongBaoTienThueDat2020Request.SoTienPhaiNop = workSheet.Cells[i, j].Value == null ? 0 : Convert.ToDecimal(workSheet.Cells[i, j].Value);
                            j++;
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
                            var col44 = workSheet.Cells[i, j].Value == null ? "" : workSheet.Cells[i, j].Value.ToString();
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
                            j++;
                            imp.ThongBaoTienThueDat2021Request.Nam = 2021;
                            imp.ThongBaoTienThueDat2021Request.SoTien = workSheet.Cells[i, j].Value == null ? 0 : Convert.ToDecimal(workSheet.Cells[i, j].Value);
                            j++;
                            imp.ThongBaoTienThueDat2021Request.SoTienMienGiam = workSheet.Cells[i, j].Value == null ? 0 : Convert.ToDecimal(workSheet.Cells[i, j].Value);
                            j++;
                            imp.ThongBaoTienThueDat2021Request.SoTienPhaiNop = workSheet.Cells[i, j].Value == null ? 0 : Convert.ToDecimal(workSheet.Cells[i, j].Value);
                            j++;
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
                            var col48 = workSheet.Cells[i, j].Value == null ? "" : workSheet.Cells[i, j].Value.ToString();
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
                            j++;
                            imp.ThongBaoTienThueDat2022Request.Nam = 2022;
                            imp.ThongBaoTienThueDat2022Request.SoTien = workSheet.Cells[i, 49].Value == null ? 0 : Convert.ToDecimal(workSheet.Cells[i, 49].Value);
                            j++;
                            imp.ThongBaoTienThueDat2022Request.SoTienMienGiam = workSheet.Cells[i, 50].Value == null ? 0 : Convert.ToDecimal(workSheet.Cells[i, 50].Value);
                            j++;
                            imp.ThongBaoTienThueDat2022Request.SoTienPhaiNop = workSheet.Cells[i, 51].Value == null ? 0 : Convert.ToDecimal(workSheet.Cells[i, 51].Value);
                            j++;

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
                            messageError.Add("Lỗi tại dòng: " + i + " cột " +j);
                            messageException.Add(e.Message);
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
                    var idQuyetDinhThueDat = 0;
                    var idThongBaoDonGia = 0;
                    var checkDN = _context.DoanhNghiep.FirstOrDefault(x => x.MaSoThue == item.DoanhNghiepRequest.MaSoThue);
                    if (checkDN == null)
                    {
                        var saveDoanhNghiep = await InsertUpdate(item.DoanhNghiepRequest);
                        idDoanhNghiep = saveDoanhNghiep.Data;
                        messageSuccess.Add(item.DoanhNghiepRequest.TenDoanhNghiep + " --- Thêm doanh nghiệp thành công --- "+idDoanhNghiep);
                    }
                    else
                    {
                        idDoanhNghiep = checkDN.IdDoanhNghiep;
                    }
                    if (!string.IsNullOrEmpty(item.QuyetDinhThueDatRequest.SoQuyetDinhThueDat))
                    {
                        item.QuyetDinhThueDatRequest.IdDoanhNghiep = idDoanhNghiep;
                        var quyetDinhThueDat = await _QuyetDinhThueDatService.InsertUpdate(item.QuyetDinhThueDatRequest);
                        idQuyetDinhThueDat = quyetDinhThueDat.Data;
                        messageSuccess.Add(item.DoanhNghiepRequest.TenDoanhNghiep + " --- Thêm quyết định thuê đất thành công --- "+ quyetDinhThueDat.Data);
                    }


                    if (!string.IsNullOrEmpty(item.HopDongThueDatRequest.SoHopDong))
                    {
                        item.HopDongThueDatRequest.IdDoanhNghiep = idDoanhNghiep;
                        item.HopDongThueDatRequest.IdQuyetDinhThueDat = idQuyetDinhThueDat;
                        var hopDongThueDat = await _HopDongThueDatService.InsertUpdate(item.HopDongThueDatRequest);
                        messageSuccess.Add(item.DoanhNghiepRequest.TenDoanhNghiep + " --- Thêm hợp đồng thành công --- "+ hopDongThueDat.Data);
                    }
                    if (!string.IsNullOrEmpty(item.QuyetDinhMienTienThueDatRequest.SoQuyetDinhMienTienThueDat))
                    {
                        item.QuyetDinhMienTienThueDatRequest.IdDoanhNghiep = idDoanhNghiep;
                        item.QuyetDinhThueDatRequest.IdQuyetDinhThueDat = idQuyetDinhThueDat;
                        var quyetDinhMienTienThueDat = await _QuyetDinhMienTienThueDatService.InsertUpdate(item.QuyetDinhMienTienThueDatRequest);
                        messageSuccess.Add(item.DoanhNghiepRequest.TenDoanhNghiep + " --- Thêm quyết định miễn tiền thuê đất thành công --- "+ quyetDinhMienTienThueDat.Data);
                    }

                    if (!string.IsNullOrEmpty(item.ThongBaoDonGiaThueDatRequest.SoThongBaoDonGiaThueDat))
                    {
                        item.ThongBaoDonGiaThueDatRequest.IdDoanhNghiep = idDoanhNghiep;
                        item.ThongBaoDonGiaThueDatRequest.IdQuyetDinhThueDat = idQuyetDinhThueDat;
                        var tbDonGiaThueDat = await _ThongBaoDonGiaThueDatService.InsertUpdate(item.ThongBaoDonGiaThueDatRequest);
                        idThongBaoDonGia = tbDonGiaThueDat.Data;
                        messageSuccess.Add(item.DoanhNghiepRequest.TenDoanhNghiep + " --- Thêm thông báo đơn giá thành công --- "+ tbDonGiaThueDat.Data);
                    }
                    if (!string.IsNullOrEmpty(item.ThongBaoTienThueDat2018Request.SoThongBaoTienThueDat))
                    {
                        item.ThongBaoTienThueDat2018Request.IdDoanhNghiep = idDoanhNghiep;
                        var tbTienThueDat = await _ThongBaoTienThueDatService.InsertUpdate(item.ThongBaoTienThueDat2018Request);
                        messageSuccess.Add(item.DoanhNghiepRequest.TenDoanhNghiep+ " --- Thêm thông báo tiền thuê đất 2018 thành công --- "+ tbTienThueDat.Data);

                    }
                    if (!string.IsNullOrEmpty(item.ThongBaoTienThueDat2019Request.SoThongBaoTienThueDat))
                    {
                        item.ThongBaoTienThueDat2019Request.IdDoanhNghiep = idDoanhNghiep;
                        var tbTienThueDat = await _ThongBaoTienThueDatService.InsertUpdate(item.ThongBaoTienThueDat2019Request);
                        messageSuccess.Add(item.DoanhNghiepRequest.TenDoanhNghiep + " --- Thêm thông báo tiền thuê đất 2019 thành công --- "+ tbTienThueDat.Data);

                    }
                    if (!string.IsNullOrEmpty(item.ThongBaoTienThueDat2020Request.SoThongBaoTienThueDat))
                    {
                        item.ThongBaoTienThueDat2020Request.IdDoanhNghiep = idDoanhNghiep;
                        var tbTienThueDat = await _ThongBaoTienThueDatService.InsertUpdate(item.ThongBaoTienThueDat2020Request);
                        messageSuccess.Add(item.DoanhNghiepRequest.TenDoanhNghiep + " --- Thêm thông báo tiền thuê đất 2020 thành công --- "+ tbTienThueDat.Data);

                    }
                    if (!string.IsNullOrEmpty(item.ThongBaoTienThueDat2021Request.SoThongBaoTienThueDat))
                    {
                        item.ThongBaoTienThueDat2021Request.IdDoanhNghiep = idDoanhNghiep;
                        var tbTienThueDat = await _ThongBaoTienThueDatService.InsertUpdate(item.ThongBaoTienThueDat2021Request);
                        messageSuccess.Add(item.DoanhNghiepRequest.TenDoanhNghiep + " --- Thêm thông báo tiền thuê đất 2021 thành công --- "+ tbTienThueDat.Data);
                    }
                    if (!string.IsNullOrEmpty(item.ThongBaoTienThueDat2022Request.SoThongBaoTienThueDat))
                    {
                        item.ThongBaoTienThueDat2022Request.IdDoanhNghiep = idDoanhNghiep;
                        var tbTienThueDat = await _ThongBaoTienThueDatService.InsertUpdate(item.ThongBaoTienThueDat2022Request);
                        messageSuccess.Add(item.DoanhNghiepRequest.TenDoanhNghiep + " --- Thêm thông báo tiền thuê đất 2022 thành công --- "+ tbTienThueDat.Data);

                    }
                    ok++;

                }
                catch (Exception e)
                {
                    messageError.Add(item.DoanhNghiepRequest.TenDoanhNghiep + " lỗi khi lưu vào DB");
                    messageException.Add(e.Message);
                }

            }
            result.Add(ok + " Doanh nghiệp được nhập hoàn chỉnh");
            result.Add("========== DANH SÁCH THÀNH CÔNG ==========");
            result.AddRange(messageSuccess);
            result.Add("========== DANH SÁCH LỖI ==========");
            result.AddRange(messageError);
            result.Add("========== DANH SÁCH EXCEPTION ==========");
            result.AddRange(messageException);
            return new ApiSuccessResult<List<string>>() { Data = result };
        }

        public async Task<ApiResult<List<string>>> ImportDoanhNghiep(IList<IFormFile> files)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            var result = new List<string>();
            var ok = 0;
            List<DoanhNghiepViewModel> users = new List<DoanhNghiepViewModel>();
            var listQuyetDinhThueDat = new List<ImportDuLieuRequest>();
            var listQuyetDinhMienTienThueDat = new List<ImportDuLieuRequest>();
            var listThongBaoTienSuDungDat = new List<ImportDuLieuRequest>();
            var listHopDongThueDat = new List<ImportDuLieuRequest>();
            var listThongBaoDonGiaThueDat = new List<ImportDuLieuRequest>();
            var listThongBaoTienThueDat = new List<ImportDuLieuRequest>();
            var messageSuccess = new List<string>();
            var messageError = new List<string>();
            var messageException = new List<string>();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = new MemoryStream())
            {
                files[0].CopyTo(stream);
                using (var package = new ExcelPackage(stream))
                {
                    var currentSheet = package.Workbook.Worksheets;

                    //Doanh nghiệp, quyết định thuê đất
                    var wsQuyetDinhThueDat = currentSheet[0];
                    var noOfRow = wsQuyetDinhThueDat.Dimension.End.Row;
                    for (int i = 4; i <= wsQuyetDinhThueDat.Dimension.End.Row; i++)
                    {
                        var j = 2;
                        try
                        {
                            var dulieu = new ImportDuLieuRequest();
                            //Doanh nghiệp
                            dulieu.DoanhNghiepRequest.TenDoanhNghiep = wsQuyetDinhThueDat.Cells[i, j].Value == null ? "" : wsQuyetDinhThueDat.Cells[i, j].Value.ToString();
                            j++;
                            dulieu.DoanhNghiepRequest.MaSoThue = wsQuyetDinhThueDat.Cells[i, j].Value == null ? "" : wsQuyetDinhThueDat.Cells[i, j].Value.ToString();
                            j++;
                            dulieu.DoanhNghiepRequest.CoQuanQuanLyThue = wsQuyetDinhThueDat.Cells[i, j].Value == null ? "" : wsQuyetDinhThueDat.Cells[i, j].Value.ToString();
                            j++;
                            dulieu.DoanhNghiepRequest.DiaChi = wsQuyetDinhThueDat.Cells[i, j].Value == null ? "" : wsQuyetDinhThueDat.Cells[i, j].Value.ToString();
                            j++;
                            dulieu.DoanhNghiepRequest.SoDienThoai = wsQuyetDinhThueDat.Cells[i, j].Value == null ? "" : wsQuyetDinhThueDat.Cells[i, j].Value.ToString();
                            j++;
                            dulieu.DoanhNghiepRequest.Email = wsQuyetDinhThueDat.Cells[i, j].Value == null ? "" : wsQuyetDinhThueDat.Cells[i, j].Value.ToString();
                            j++;
                            dulieu.DoanhNghiepRequest.GhiChu = wsQuyetDinhThueDat.Cells[i, j].Value == null ? "" : wsQuyetDinhThueDat.Cells[i, j].Value.ToString();
                            j++;
                            //Quyết định giao đất UBND tỉnh
                            var quyetDinhThueDatChiTiet = new QuyetDinhThueDatChiTietRequest();
                            var col9 = wsQuyetDinhThueDat.Cells[i, j].Value == null ? "" : wsQuyetDinhThueDat.Cells[i, j].Value.ToString();
                            if (col9 != "")
                            {
                                var arr = col9.Split(new string[] { "ngày" }, StringSplitOptions.None);
                                dulieu.QuyetDinhThueDatRequest.NgayQuyetDinhGiaoDat = arr[1].ToString().Trim();
                                var soTB = arr[0].Trim().Split(' ');
                                dulieu.QuyetDinhThueDatRequest.SoQuyetDinhGiaoDat = soTB[soTB.Length - 1].ToString().Trim();
                            }
                            else
                            {
                                dulieu.QuyetDinhThueDatRequest.NgayQuyetDinhGiaoDat = "";
                                dulieu.QuyetDinhThueDatRequest.SoQuyetDinhGiaoDat = "";
                            }
                            j++;

                            //Quyết định thuê đất
                            var col10 = wsQuyetDinhThueDat.Cells[i, j].Value == null ? "" : wsQuyetDinhThueDat.Cells[i, j].Value.ToString();
                            if (col10 != "")
                            {
                                var arr = col10.Split(new string[] { "ngày" }, StringSplitOptions.None);
                                dulieu.QuyetDinhThueDatRequest.NgayQuyetDinhThueDat = Convert.ToDateTime(arr[1].ToString().Trim(), new CultureInfo("vi-VN")).ToString("dd/MM/yyyy"); 
                                var soTB = arr[0].Trim().Split(' ');
                                dulieu.QuyetDinhThueDatRequest.SoQuyetDinhThueDat = soTB[soTB.Length - 1].ToString().Trim();
                            }
                            else
                            {
                                dulieu.QuyetDinhThueDatRequest.SoQuyetDinhThueDat = "";
                                dulieu.QuyetDinhThueDatRequest.NgayQuyetDinhThueDat = "";
                            }
                            j++;
                            dulieu.QuyetDinhThueDatRequest.TenQuyetDinhThueDat = wsQuyetDinhThueDat.Cells[i, j].Value == null ? "" : wsQuyetDinhThueDat.Cells[i, j].Value.ToString();
                            j++;
                            var soThua = (wsQuyetDinhThueDat.Cells[i, j].Value == null ? "" : "Thửa số " + wsQuyetDinhThueDat.Cells[i, j].Value.ToString()) + " ,";
                            j++;
                            var soTo = (wsQuyetDinhThueDat.Cells[i, j].Value == null ? "" : " Tờ bản đồ số " + wsQuyetDinhThueDat.Cells[i, j].Value.ToString());
                            dulieu.QuyetDinhThueDatRequest.ViTriThuaDat =  soThua +  soTo;
                            j++;
                            dulieu.QuyetDinhThueDatRequest.DiaChiThuaDat = wsQuyetDinhThueDat.Cells[i, j].Value == null ? "" : wsQuyetDinhThueDat.Cells[i, j].Value.ToString();
                            j++;
                           
                            dulieu.QuyetDinhThueDatRequest.TongDienTich = wsQuyetDinhThueDat.Cells[i, j].Value == null ? 0 : Convert.ToDecimal(wsQuyetDinhThueDat.Cells[i, j].Value);
                            j++;
                            if (col10.Contains("TLĐ"))
                            {
                                quyetDinhThueDatChiTiet.HinhThucThue = "HopDongThueLaiDat";

                            }
                            else
                            {
                                quyetDinhThueDatChiTiet.HinhThucThue = "ThueDatTraTienHangNam";

                            }
                            j++;
                            quyetDinhThueDatChiTiet.DienTich = wsQuyetDinhThueDat.Cells[i, j].Value == null ? 0 : Convert.ToDecimal(wsQuyetDinhThueDat.Cells[i, j].Value);
                            j++;
                            quyetDinhThueDatChiTiet.MucDichSuDung = wsQuyetDinhThueDat.Cells[i, j].Value == null ? "" : wsQuyetDinhThueDat.Cells[i, j].Value.ToString();
                            j++;
                            quyetDinhThueDatChiTiet.ThoiHanThue = wsQuyetDinhThueDat.Cells[i, j].Value == null ? "" : wsQuyetDinhThueDat.Cells[i, j].Value.ToString();
                            j++;

                            var col20 = wsQuyetDinhThueDat.Cells[i, j].Value;
                            if (col20 != null)
                            {
                                var date = Convert.ToDateTime(col20, new CultureInfo("vi-VN"));
                                quyetDinhThueDatChiTiet.TuNgayThue = date.ToString("dd/MM/yyyy");
                            }
                            else
                            {
                                quyetDinhThueDatChiTiet.TuNgayThue = "";
                            }
                            var col21 = wsQuyetDinhThueDat.Cells[i, j].Value;
                            if (col21 != null)
                            {
                                var date = Convert.ToDateTime(col21, new CultureInfo("vi-VN"));
                                quyetDinhThueDatChiTiet.DenNgayThue = date.ToString("dd/MM/yyyy");
                            }
                            else
                            {
                                quyetDinhThueDatChiTiet.DenNgayThue = "";
                            }
                            dulieu.QuyetDinhThueDatRequest.QuyetDinhThueDatChiTiet = new List<QuyetDinhThueDatChiTietRequest> { quyetDinhThueDatChiTiet };
                            listQuyetDinhThueDat.Add(dulieu);
                        }
                        catch (Exception e)
                        {
                            messageError.Add("Lỗi tại sheet "+ wsQuyetDinhThueDat.Name +", dòng: " + i + " cột " + j);
                            messageException.Add(e.Message);
                            continue;
                        }
                    }

                    //Sheet Quyết định miễn tiền thuê đất
                    var wsQuyetDinhMienTienThueDat = currentSheet[1];
                    for (int i = 4; i <= wsQuyetDinhMienTienThueDat.Dimension.End.Row; i++)
                    {
                        var j = 2;
                        try
                        {
                            var dulieu = new ImportDuLieuRequest();
                            //Doanh nghiệp
                            dulieu.DoanhNghiepRequest.TenDoanhNghiep = wsQuyetDinhMienTienThueDat.Cells[i, j].Value == null ? "" : wsQuyetDinhMienTienThueDat.Cells[i, j].Value.ToString();
                            j++;
                            dulieu.DoanhNghiepRequest.MaSoThue = wsQuyetDinhMienTienThueDat.Cells[i, j].Value == null ? "" : wsQuyetDinhMienTienThueDat.Cells[i, j].Value.ToString();
                            j++;

                          //Quyết định thuê đất
                            var col4 = wsQuyetDinhMienTienThueDat.Cells[i, j].Value == null ? "" : wsQuyetDinhMienTienThueDat.Cells[i, j].Value.ToString();
                            if (col4 != "")
                            {
                                var arr = col4.Split(new string[] { "ngày" }, StringSplitOptions.None);
                                dulieu.QuyetDinhThueDatRequest.NgayQuyetDinhThueDat = arr[1].ToString().Trim();
                                var soTB = arr[0].Trim().Split(' ');
                                dulieu.QuyetDinhThueDatRequest.SoQuyetDinhThueDat = soTB[soTB.Length - 1].ToString().Trim();
                            }
                            else
                            {
                                dulieu.QuyetDinhThueDatRequest.SoQuyetDinhThueDat = "";
                                dulieu.QuyetDinhThueDatRequest.NgayQuyetDinhThueDat = "";
                            }
                            j++;

                            //Quyết định miễn tiền thuê đất  
                            var col5= wsQuyetDinhMienTienThueDat.Cells[i, j].Value == null ? "" : wsQuyetDinhMienTienThueDat.Cells[i, j].Value.ToString();
                            if (col5 != "")
                            {
                                var arr = col5.Split(new string[] { "ngày" }, StringSplitOptions.None);
                                dulieu.QuyetDinhMienTienThueDatRequest.NgayQuyetDinhMienTienThueDat = Convert.ToDateTime(arr[1].ToString().Trim(), new CultureInfo("vi-VN")).ToString("dd/MM/yyyy");
                                var soTB = arr[0].Trim().Split(' ');
                                dulieu.QuyetDinhMienTienThueDatRequest.SoQuyetDinhMienTienThueDat = soTB[soTB.Length - 1].ToString().Trim();
                            }
                            else
                            {
                                dulieu.QuyetDinhMienTienThueDatRequest.SoQuyetDinhMienTienThueDat = "";
                                dulieu.QuyetDinhMienTienThueDatRequest.NgayQuyetDinhMienTienThueDat = "";
                            }
                            j++;
                            dulieu.QuyetDinhMienTienThueDatRequest.TenQuyetDinhMienTienThueDat = wsQuyetDinhMienTienThueDat.Cells[i, j].Value == null ? "" : wsQuyetDinhMienTienThueDat.Cells[i, j].Value.ToString();
                            j++;
                            dulieu.QuyetDinhMienTienThueDatRequest.DienTichMienTienThueDat = wsQuyetDinhMienTienThueDat.Cells[i, j].Value == null ? 0 : Convert.ToDecimal(wsQuyetDinhMienTienThueDat.Cells[i, j].Value);
                            j++;
                            dulieu.QuyetDinhMienTienThueDatRequest.ThoiHanMienTienThueDat = wsQuyetDinhMienTienThueDat.Cells[i, j].Value == null ? "" : wsQuyetDinhMienTienThueDat.Cells[i, j].Value.ToString();
                            j++;

                            var col9 = wsQuyetDinhMienTienThueDat.Cells[i, j].Value;
                            if (col9 != null)
                            {
                                var date = Convert.ToDateTime(col9, new CultureInfo("vi-VN"));
                                dulieu.QuyetDinhMienTienThueDatRequest.NgayHieuLucMienTienThueDat = date.ToString("dd/MM/yyyy");
                            }
                            else
                            {
                                dulieu.QuyetDinhMienTienThueDatRequest.NgayHieuLucMienTienThueDat = "";
                            }
                            j++;
                            var col10 = wsQuyetDinhMienTienThueDat.Cells[i, j].Value;
                            if (col10 != null)
                            {
                                var date = Convert.ToDateTime(col10, new CultureInfo("vi-VN"));
                                dulieu.QuyetDinhMienTienThueDatRequest.NgayHetHieuLucMienTienThueDat = date.ToString("dd/MM/yyyy");
                            }
                            else
                            {
                                dulieu.QuyetDinhMienTienThueDatRequest.NgayHetHieuLucMienTienThueDat = "";
                            }
                            listQuyetDinhMienTienThueDat.Add(dulieu);
                        }
                        catch (Exception e)
                        {
                            messageError.Add("Lỗi tại sheet " + wsQuyetDinhMienTienThueDat.Name + ", dòng: " + i + " cột " + j);
                            messageException.Add(e.Message);
                            continue;
                        }
                    }

                    //Sheet Thông báo tiền sử dụng đất
                    var wsThongBaoTienSuDungDat = currentSheet[2];
                    for (int i = 4; i <= wsThongBaoTienSuDungDat.Dimension.End.Row; i++)
                    {
                        var j = 2;
                        try
                        {
                            var dulieu = new ImportDuLieuRequest();
                            //Doanh nghiệp
                            dulieu.DoanhNghiepRequest.TenDoanhNghiep = wsThongBaoTienSuDungDat.Cells[i, j].Value == null ? "" : wsThongBaoTienSuDungDat.Cells[i, j].Value.ToString();
                            j++;
                            dulieu.DoanhNghiepRequest.MaSoThue = wsThongBaoTienSuDungDat.Cells[i, j].Value == null ? "" : wsThongBaoTienSuDungDat.Cells[i, j].Value.ToString();
                            j++;

                            //Quyết định thuê đất
                            var col4 = wsThongBaoTienSuDungDat.Cells[i, j].Value == null ? "" : wsThongBaoTienSuDungDat.Cells[i, j].Value.ToString();
                            if (col4 != "")
                            {
                                var arr = col4.Split(new string[] { "ngày" }, StringSplitOptions.None);
                                dulieu.QuyetDinhThueDatRequest.NgayQuyetDinhThueDat = arr[1].ToString().Trim();
                                var soTB = arr[0].Trim().Split(' ');
                                dulieu.QuyetDinhThueDatRequest.SoQuyetDinhThueDat = soTB[soTB.Length - 1].ToString().Trim();
                            }
                            else
                            {
                                dulieu.QuyetDinhThueDatRequest.SoQuyetDinhThueDat = "";
                                dulieu.QuyetDinhThueDatRequest.NgayQuyetDinhThueDat = "";
                            }
                            j++;

                            //Thông báo tiền sử dụng đất  
                            var col5 = wsThongBaoTienSuDungDat.Cells[i, j].Value == null ? "" : wsThongBaoTienSuDungDat.Cells[i, j].Value.ToString();
                            if (col5 != "")
                            {
                                var arr = col5.Split(new string[] { "ngày" }, StringSplitOptions.None);
                                dulieu.ThongBaoTienSuDungDatRequest.NgayThongBaoTienSuDungDat = Convert.ToDateTime(arr[1].ToString().Trim(), new CultureInfo("vi-VN")).ToString("dd/MM/yyyy");
                                var soTB = arr[0].Trim().Split(' ');
                                dulieu.ThongBaoTienSuDungDatRequest.SoThongBaoTienSuDungDat = soTB[soTB.Length - 1].ToString().Trim();
                            }
                            else
                            {
                                dulieu.ThongBaoTienSuDungDatRequest.SoThongBaoTienSuDungDat = "";
                                dulieu.ThongBaoTienSuDungDatRequest.NgayThongBaoTienSuDungDat = "";
                            }
                            j++;
                            dulieu.ThongBaoTienSuDungDatRequest.TenThongBaoTienSuDungDat = wsThongBaoTienSuDungDat.Cells[i, j].Value == null ? "" : wsThongBaoTienSuDungDat.Cells[i, j].Value.ToString();
                            j++;
                            dulieu.ThongBaoTienSuDungDatRequest.TongDienTich = wsThongBaoTienSuDungDat.Cells[i, j].Value == null ? 0 : Convert.ToDecimal(wsThongBaoTienSuDungDat.Cells[i, j].Value);
                            j++;
                            dulieu.ThongBaoTienSuDungDatRequest.DienTichKhongPhaiNop = wsThongBaoTienSuDungDat.Cells[i, j].Value == null ? 0 : Convert.ToDecimal(wsThongBaoTienSuDungDat.Cells[i, j].Value);
                            j++;
                            dulieu.ThongBaoTienSuDungDatRequest.DienTichPhaiNop = wsThongBaoTienSuDungDat.Cells[i, j].Value == null ? 0 : Convert.ToDecimal(wsThongBaoTienSuDungDat.Cells[i, j].Value);
                            j++;
                            dulieu.ThongBaoTienSuDungDatRequest.DonGia = wsThongBaoTienSuDungDat.Cells[i, j].Value == null ? 0 : Convert.ToDecimal(wsThongBaoTienSuDungDat.Cells[i, j].Value);
                            j++;
                            dulieu.ThongBaoTienSuDungDatRequest.TongDienTich = wsThongBaoTienSuDungDat.Cells[i, j].Value == null ? 0 : Convert.ToDecimal(wsThongBaoTienSuDungDat.Cells[i, j].Value);
                            j++;
                            dulieu.ThongBaoTienSuDungDatRequest.SoTienMienGiam = wsThongBaoTienSuDungDat.Cells[i, j].Value == null ? 0 : Convert.ToDecimal(wsThongBaoTienSuDungDat.Cells[i, j].Value);
                            j++;
                            dulieu.ThongBaoTienSuDungDatRequest.LyDoMienGiam = wsThongBaoTienSuDungDat.Cells[i, j].Value == null ? "" : wsThongBaoTienSuDungDat.Cells[i, j].Value.ToString();
                            j++;
                            dulieu.ThongBaoTienSuDungDatRequest.SoTienBoiThuongGiaiPhongMatBang = wsThongBaoTienSuDungDat.Cells[i, j].Value == null ? 0 : Convert.ToDecimal(wsThongBaoTienSuDungDat.Cells[i, j].Value);
                            j++;
                            dulieu.ThongBaoTienSuDungDatRequest.LyDoBoiThuongGiaiPhongMatBang = wsThongBaoTienSuDungDat.Cells[i, j].Value == null ? "" : wsThongBaoTienSuDungDat.Cells[i, j].Value.ToString();
                            j++;
                            dulieu.ThongBaoTienSuDungDatRequest.SoTienPhaiNop = wsThongBaoTienSuDungDat.Cells[i, j].Value == null ? 0 : Convert.ToDecimal(wsThongBaoTienSuDungDat.Cells[i, j].Value);
                            j++;
                            dulieu.ThongBaoTienSuDungDatRequest.LanhDaoKyThongBaoTienSuDungDat = wsThongBaoTienSuDungDat.Cells[i, j].Value == null ? "" : wsThongBaoTienSuDungDat.Cells[i, j].Value.ToString();
                            listThongBaoTienSuDungDat.Add(dulieu);
                        }
                        catch (Exception e)
                        {
                            messageError.Add("Lỗi tại sheet " + wsThongBaoTienSuDungDat.Name + ", dòng: " + i + " cột " + j);
                            messageException.Add(e.Message);
                            continue;
                        }
                    }


                    //Sheet HopDongThueDat
                    var wsHopDongThueDat = currentSheet[3];
                    for (int i = 4; i <= wsHopDongThueDat.Dimension.End.Row; i++)
                    {
                        var j = 2;
                        try
                        {
                            var dulieu = new ImportDuLieuRequest();
                            //Doanh nghiệp
                            dulieu.DoanhNghiepRequest.TenDoanhNghiep = wsHopDongThueDat.Cells[i, j].Value == null ? "" : wsHopDongThueDat.Cells[i, j].Value.ToString();
                            j++;
                            dulieu.DoanhNghiepRequest.MaSoThue = wsHopDongThueDat.Cells[i, j].Value == null ? "" : wsHopDongThueDat.Cells[i, j].Value.ToString();
                            j++;

                            //Quyết định thuê đất
                            var col4 = wsHopDongThueDat.Cells[i, j].Value == null ? "" : wsHopDongThueDat.Cells[i, j].Value.ToString();
                            if (col4 != "")
                            {
                                var arr = col4.Split(new string[] { "ngày" }, StringSplitOptions.None);
                                dulieu.QuyetDinhThueDatRequest.NgayQuyetDinhThueDat = arr[1].ToString().Trim();
                                var soTB = arr[0].Trim().Split(' ');
                                dulieu.QuyetDinhThueDatRequest.SoQuyetDinhThueDat = soTB[soTB.Length - 1].ToString().Trim();
                            }
                            else
                            {
                                dulieu.QuyetDinhThueDatRequest.SoQuyetDinhThueDat = "";
                                dulieu.QuyetDinhThueDatRequest.NgayQuyetDinhThueDat = "";
                            }
                            j++;

                            //Hợp đồng thuê đất  
                            var col5 = wsHopDongThueDat.Cells[i, j].Value == null ? "" : wsHopDongThueDat.Cells[i, j].Value.ToString();
                            if (col5 != "")
                            {
                                var arr = col5.Split(new string[] { "ngày" }, StringSplitOptions.None);
                                dulieu.HopDongThueDatRequest.NgayKyHopDong = Convert.ToDateTime(arr[1].ToString().Trim(), new CultureInfo("vi-VN")).ToString("dd/MM/yyyy");
                                var soTB = arr[0].Trim().Split(' ');
                                dulieu.HopDongThueDatRequest.SoHopDong = soTB[soTB.Length - 1].ToString().Trim();
                            }
                            else
                            {
                                dulieu.HopDongThueDatRequest.SoHopDong = "";
                                dulieu.HopDongThueDatRequest.NgayKyHopDong = "";
                            }
                            j++;
                            var col6 = wsHopDongThueDat.Cells[i, j].Value;
                            if (col6 != null)
                            {
                                var date = Convert.ToDateTime(col6, new CultureInfo("vi-VN"));
                                dulieu.HopDongThueDatRequest.NgayHieuLucHopDong = date.ToString("dd/MM/yyyy");
                            }
                            j++;
                            var col7 = wsHopDongThueDat.Cells[i, j].Value;
                            if (col7 != null)
                            {
                                var date = Convert.ToDateTime(col7, new CultureInfo("vi-VN"));
                                dulieu.HopDongThueDatRequest.NgayHetHieuLucHopDong = date.ToString("dd/MM/yyyy");
                            }
                            j++;
                            dulieu.HopDongThueDatRequest.CoQuanKy = wsHopDongThueDat.Cells[i, j].Value == null ? "" : wsHopDongThueDat.Cells[i, j].Value.ToString();
                            j++;
                            dulieu.HopDongThueDatRequest.NguoiKy = wsHopDongThueDat.Cells[i, j].Value == null ? "" : wsHopDongThueDat.Cells[i, j].Value.ToString();
                            listHopDongThueDat.Add(dulieu);
                        }
                        catch (Exception e)
                        {
                            messageError.Add("Lỗi tại sheet " + wsHopDongThueDat.Name + ", dòng: " + i + " cột " + j);
                            messageException.Add(e.Message);
                            continue;
                        }
                    }

                    //Sheet Thông báo đơn giá thuê đất
                    var wsThongBaoDonGiaThueDat = currentSheet[4];
                    for (int i = 4; i <= wsThongBaoDonGiaThueDat.Dimension.End.Row; i++)
                    {
                        var j = 2;
                        try
                        {
                            var dulieu = new ImportDuLieuRequest();
                            //Doanh nghiệp
                            dulieu.DoanhNghiepRequest.TenDoanhNghiep = wsThongBaoDonGiaThueDat.Cells[i, j].Value == null ? "" : wsThongBaoDonGiaThueDat.Cells[i, j].Value.ToString();
                            j++;
                            dulieu.DoanhNghiepRequest.MaSoThue = wsThongBaoDonGiaThueDat.Cells[i, j].Value == null ? "" : wsThongBaoDonGiaThueDat.Cells[i, j].Value.ToString();
                            j++;

                            //Quyết định thuê đất
                            var col4 = wsThongBaoDonGiaThueDat.Cells[i, j].Value == null ? "" : wsThongBaoDonGiaThueDat.Cells[i, j].Value.ToString();
                            if (col4 != "")
                            {
                                var arr = col4.Split(new string[] { "ngày" }, StringSplitOptions.None);
                                dulieu.QuyetDinhThueDatRequest.NgayQuyetDinhThueDat = arr[1].ToString().Trim();
                                var soTB = arr[0].Trim().Split(' ');
                                dulieu.QuyetDinhThueDatRequest.SoQuyetDinhThueDat = soTB[soTB.Length - 1].ToString().Trim();
                            }
                            else
                            {
                                dulieu.QuyetDinhThueDatRequest.SoQuyetDinhThueDat = "";
                                dulieu.QuyetDinhThueDatRequest.NgayQuyetDinhThueDat = "";
                            }
                            j++;

                            //Thông báo đơn giá thuê đất  
                            var col5 = wsThongBaoDonGiaThueDat.Cells[i, j].Value == null ? "" : wsThongBaoDonGiaThueDat.Cells[i, j].Value.ToString();
                            if (col5 != "")
                            {
                                var arr = col5.Split(new string[] { "ngày" }, StringSplitOptions.None);
                                dulieu.ThongBaoDonGiaThueDatRequest.NgayThongBaoDonGiaThueDat = Convert.ToDateTime(arr[1].ToString().Trim(), new CultureInfo("vi-VN")).ToString("dd/MM/yyyy");
                                var soTB = arr[0].Trim().Split(' ');
                                dulieu.ThongBaoDonGiaThueDatRequest.SoThongBaoDonGiaThueDat = soTB[soTB.Length - 1].ToString().Trim();
                            }
                            else
                            {
                                dulieu.ThongBaoDonGiaThueDatRequest.SoThongBaoDonGiaThueDat = "";
                                dulieu.ThongBaoDonGiaThueDatRequest.NgayThongBaoDonGiaThueDat = "";
                            }
                            j++;
                            dulieu.ThongBaoDonGiaThueDatRequest.TenThongBaoDonGiaThueDat = wsThongBaoDonGiaThueDat.Cells[i, j].Value == null ? "" : wsThongBaoDonGiaThueDat.Cells[i, j].Value.ToString();
                            j++;
                            dulieu.ThongBaoDonGiaThueDatRequest.DonGia = wsThongBaoDonGiaThueDat.Cells[i, j].Value == null ? 0 : Convert.ToDecimal(wsThongBaoDonGiaThueDat.Cells[i, j].Value);
                            j++;
                            dulieu.ThongBaoDonGiaThueDatRequest.ThoiHanDonGia = wsThongBaoDonGiaThueDat.Cells[i, j].Value == null ? "" : wsThongBaoDonGiaThueDat.Cells[i, j].Value.ToString();
                            j++;

                            var col9 = wsThongBaoDonGiaThueDat.Cells[i, j].Value;
                            if (col9 != null)
                            {
                                var date = Convert.ToDateTime(col9, new CultureInfo("vi-VN"));
                                dulieu.ThongBaoDonGiaThueDatRequest.NgayHieuLucDonGiaThueDat = date.ToString("dd/MM/yyyy");
                            }
                            j++;
                            var col10 = wsThongBaoDonGiaThueDat.Cells[i, j].Value;
                            if (col10 != null)
                            {
                                var date = Convert.ToDateTime(col10, new CultureInfo("vi-VN"));
                                dulieu.ThongBaoDonGiaThueDatRequest.NgayHetHieuLucDonGiaThueDat = date.ToString("dd/MM/yyyy");
                            }
                            j++;

                            dulieu.ThongBaoDonGiaThueDatRequest.DienTichKhongPhaiNop = wsThongBaoDonGiaThueDat.Cells[i, j].Value == null ? 0 : Convert.ToDecimal(wsThongBaoDonGiaThueDat.Cells[i, j].Value);
                            j++;
                            dulieu.ThongBaoDonGiaThueDatRequest.DienTichPhaiNop = wsThongBaoDonGiaThueDat.Cells[i, j].Value == null ? 0 : Convert.ToDecimal(wsThongBaoDonGiaThueDat.Cells[i, j].Value);
                            j++;
                            dulieu.ThongBaoDonGiaThueDatRequest.LanhDaoKyThongBaoDonGiaThueDat = wsThongBaoDonGiaThueDat.Cells[i, j].Value == null ? "" : wsThongBaoDonGiaThueDat.Cells[i, j].Value.ToString();
                            listThongBaoDonGiaThueDat.Add(dulieu);
                        }
                        catch (Exception e)
                        {
                            messageError.Add("Lỗi tại sheet " + wsThongBaoDonGiaThueDat.Name + ", dòng: " + i + " cột " + j);
                            messageException.Add(e.Message);
                            continue;
                        }
                    }


                    //Sheet Thông báo tiền thuê đất
                    var wsThongBaoTienThueDat = currentSheet[5];
                    for (int i = 4; i <= wsThongBaoTienThueDat.Dimension.End.Row; i++)
                    {
                        var j = 2;
                        try
                        {
                            var dulieu = new ImportDuLieuRequest();
                            //Doanh nghiệp
                            dulieu.DoanhNghiepRequest.TenDoanhNghiep = wsThongBaoTienThueDat.Cells[i, j].Value == null ? "" : wsThongBaoTienThueDat.Cells[i, j].Value.ToString();
                            j++;
                            dulieu.DoanhNghiepRequest.MaSoThue = wsThongBaoTienThueDat.Cells[i, j].Value == null ? "" : wsThongBaoTienThueDat.Cells[i, j].Value.ToString();
                            j++;

                            //Quyết định thuê đất
                            var col4 = wsThongBaoTienThueDat.Cells[i, j].Value == null ? "" : wsThongBaoTienThueDat.Cells[i, j].Value.ToString();
                            if (col4 != "")
                            {
                                var arr = col4.Split(new string[] { "ngày" }, StringSplitOptions.None);
                                dulieu.QuyetDinhThueDatRequest.NgayQuyetDinhThueDat = arr[1].ToString().Trim();
                                var soTB = arr[0].Trim().Split(' ');
                                dulieu.QuyetDinhThueDatRequest.SoQuyetDinhThueDat = soTB[soTB.Length - 1].ToString().Trim();
                            }
                            else
                            {
                                dulieu.QuyetDinhThueDatRequest.SoQuyetDinhThueDat = "";
                                dulieu.QuyetDinhThueDatRequest.NgayQuyetDinhThueDat = "";
                            }
                            j++;
                            dulieu.ThongBaoTienThueDatRequest.Nam = wsThongBaoTienThueDat.Cells[i, j].Value == null ? 0 : Int32.Parse(wsThongBaoTienThueDat.Cells[i, j].Value.ToString());
                            j++;
                            //Thông báo tiền thuê đất  
                            var col6 = wsThongBaoTienThueDat.Cells[i, j].Value == null ? "" : wsThongBaoTienThueDat.Cells[i, j].Value.ToString();
                            if (col6 != "")
                            {
                                var arr = col6.Split(new string[] { "ngày" }, StringSplitOptions.None);
                                dulieu.ThongBaoTienThueDatRequest.NgayThongBaoTienThueDat = Convert.ToDateTime(arr[1].ToString().Trim(), new CultureInfo("vi-VN")).ToString("dd/MM/yyyy");
                                var soTB = arr[0].Trim().Split(' ');
                                dulieu.ThongBaoTienThueDatRequest.SoThongBaoTienThueDat = soTB[soTB.Length - 1].ToString().Trim();
                            }
                            else
                            {
                                dulieu.ThongBaoTienThueDatRequest.SoThongBaoTienThueDat = "";
                                dulieu.ThongBaoTienThueDatRequest.NgayThongBaoTienThueDat = "";
                            }
                            j++;
                            //dulieu.ThongBaoTienThueDatRequest.TenThongBaoTienThueDat = wsThongBaoTienThueDat.Cells[i, j].Value == null ? "" : wsThongBaoTienThueDat.Cells[i, j].Value.ToString();
                            j++;
                            dulieu.ThongBaoTienThueDatRequest.LoaiThongBaoTienThueDat = "ThongBaoTuNamThuHaiTroDi";
                            j++;
                            dulieu.ThongBaoTienThueDatRequest.TongDienTich = wsThongBaoTienThueDat.Cells[i, j].Value == null ? 0 : Convert.ToDecimal(wsThongBaoTienThueDat.Cells[i, j].Value);
                            j++;
                            dulieu.ThongBaoTienThueDatRequest.DienTichKhongPhaiNop = wsThongBaoTienThueDat.Cells[i, j].Value == null ? 0 : Convert.ToDecimal(wsThongBaoTienThueDat.Cells[i, j].Value);
                            j++;
                            dulieu.ThongBaoTienThueDatRequest.DienTichPhaiNop = wsThongBaoTienThueDat.Cells[i, j].Value == null ? 0 : Convert.ToDecimal(wsThongBaoTienThueDat.Cells[i, j].Value);
                            j++;
                            dulieu.ThongBaoTienThueDatRequest.SoTien = wsThongBaoTienThueDat.Cells[i, j].Value == null ? 0 : Convert.ToDecimal(wsThongBaoTienThueDat.Cells[i, j].Value);
                            j++;
                            dulieu.ThongBaoTienThueDatRequest.SoTienMienGiam = wsThongBaoTienThueDat.Cells[i, j].Value == null ? 0 : Convert.ToDecimal(wsThongBaoTienThueDat.Cells[i, j].Value);
                            j++;
                            dulieu.ThongBaoTienThueDatRequest.SoTienPhaiNop = wsThongBaoTienThueDat.Cells[i, j].Value == null ? 0 : Convert.ToDecimal(wsThongBaoTienThueDat.Cells[i, j].Value);
                            j++;
                            dulieu.ThongBaoTienThueDatRequest.LanhDaoKyThongBaoTienThueDat = wsThongBaoTienThueDat.Cells[i, j].Value == null ? "" : wsThongBaoTienThueDat.Cells[i, j].Value.ToString();
                            j++;
                            var col16 = wsThongBaoTienThueDat.Cells[i, j].Value == null ? "" : wsThongBaoTienThueDat.Cells[i, j].Value.ToString();
                            var tbTienThueDatChiTiet = new ThongBaoTienThueDatChiTietRequest();
                            tbTienThueDatChiTiet.Nam = dulieu.ThongBaoTienThueDatRequest.Nam;
                            if (col16 != "")
                            {
                                var arr = col16.Split(new string[] { "ngày" }, StringSplitOptions.None);
                                tbTienThueDatChiTiet.NgayThongBaoDonGiaThueDat = Convert.ToDateTime(arr[1].ToString().Trim(), new CultureInfo("vi-VN")).ToString("dd/MM/yyyy");
                                var soTB = arr[0].Trim().Split(' ');
                                tbTienThueDatChiTiet.SoThongBaoDonGiaThueDat = soTB[soTB.Length - 1].ToString().Trim();
                            }
                            else
                            {
                                tbTienThueDatChiTiet.SoThongBaoDonGiaThueDat = "";
                                tbTienThueDatChiTiet.NgayThongBaoDonGiaThueDat = "";
                            }
                            j++;
                            tbTienThueDatChiTiet.DonGia =  wsThongBaoTienThueDat.Cells[i, j].Value == null ? 0 : Convert.ToDecimal(wsThongBaoTienThueDat.Cells[i, j].Value);
                            j++;
                            tbTienThueDatChiTiet.DienTichPhaiNop = wsThongBaoTienThueDat.Cells[i, j].Value == null ? 0 : Convert.ToDecimal(wsThongBaoTienThueDat.Cells[i, j].Value);
                            j++;
                            tbTienThueDatChiTiet.SoTienPhaiNop = wsThongBaoTienThueDat.Cells[i, j].Value == null ? 0 : Convert.ToDecimal(wsThongBaoTienThueDat.Cells[i, j].Value);
                            j++;
                            var col20 = wsThongBaoTienThueDat.Cells[i, j].Value;
                            if (col20 != null)
                            {
                                var date = Convert.ToDateTime(col20, new CultureInfo("vi-VN"));
                                tbTienThueDatChiTiet.TuNgayTinhTien = date.ToString("dd/MM/yyyy");
                            }
                            else
                            {
                                tbTienThueDatChiTiet.TuNgayTinhTien = "01/01/" + tbTienThueDatChiTiet.Nam.ToString();
                            }
                            j++;
                            var col21 = wsThongBaoTienThueDat.Cells[i, j].Value;
                            if (col21 != null)
                            {
                                var date = Convert.ToDateTime(col21, new CultureInfo("vi-VN"));
                                tbTienThueDatChiTiet.DenNgayTinhTien = date.ToString("dd/MM/yyyy");
                            }
                            else
                            {
                                tbTienThueDatChiTiet.DenNgayTinhTien = "31/12/" + tbTienThueDatChiTiet.Nam.ToString();
                            }
                            dulieu.ThongBaoTienThueDatRequest.ThongBaoTienThueDatChiTiet = new List<ThongBaoTienThueDatChiTietRequest>{tbTienThueDatChiTiet};
                            listThongBaoTienThueDat.Add(dulieu);
                        }
                        catch (Exception e)
                        {
                            messageError.Add("Lỗi tại sheet " + wsThongBaoTienThueDat.Name + ", dòng: " + i + " cột " + j);
                            messageException.Add(e.Message);
                            continue;
                        }
                    }
                }
            }

            // Lưu vào DB
            foreach (var item in listQuyetDinhThueDat)
            {
                try
                {
                    var idDoanhNghiep = 0;
                    var idQuyetDinhThueDat = 0;
                    var checkDN = _context.DoanhNghiep.FirstOrDefault(x => x.MaSoThue == item.DoanhNghiepRequest.MaSoThue);
                    if (checkDN == null)
                    {
                        var saveDoanhNghiep = await InsertUpdate(item.DoanhNghiepRequest);
                        idDoanhNghiep = saveDoanhNghiep.Data;
                        messageSuccess.Add(item.DoanhNghiepRequest.TenDoanhNghiep + " --- Thêm doanh nghiệp thành công --- " + idDoanhNghiep);
                    }
                    else
                    {
                        idDoanhNghiep = checkDN.IdDoanhNghiep;
                    }
                    if (!string.IsNullOrEmpty(item.QuyetDinhThueDatRequest.SoQuyetDinhThueDat))
                    {
                        item.QuyetDinhThueDatRequest.IdDoanhNghiep = idDoanhNghiep;
                        var quyetDinhThueDat = await _QuyetDinhThueDatService.InsertUpdate(item.QuyetDinhThueDatRequest);
                        idQuyetDinhThueDat = quyetDinhThueDat.Data;
                        messageSuccess.Add(item.DoanhNghiepRequest.TenDoanhNghiep + " --- Thêm quyết định thuê đất thành công --- " + quyetDinhThueDat.Data);
                    }
                    ok++;
                }
                catch (Exception e)
                {
                    messageError.Add(item.DoanhNghiepRequest.TenDoanhNghiep + " lỗi khi lưu vào DB");
                    messageException.Add(e.Message);
                }

            }
            foreach (var item in listQuyetDinhMienTienThueDat)
            {
                try
                {
                    var checkDN = _context.DoanhNghiep.FirstOrDefault(x => x.MaSoThue == item.DoanhNghiepRequest.MaSoThue);
                    var checkQdTD = _context.QuyetDinhThueDat.FirstOrDefault(x => x.SoQuyetDinhThueDat == item.QuyetDinhThueDatRequest.SoQuyetDinhThueDat && x.NgayQuyetDinhThueDat == Convert.ToDateTime(item.QuyetDinhThueDatRequest.NgayQuyetDinhThueDat, new CultureInfo("vi-VN")));
                    if (checkDN != null && checkQdTD != null)
                    {
                        if (!string.IsNullOrEmpty(item.QuyetDinhMienTienThueDatRequest.SoQuyetDinhMienTienThueDat))
                        {
                            item.QuyetDinhMienTienThueDatRequest.IdDoanhNghiep = checkDN.IdDoanhNghiep;
                            item.QuyetDinhMienTienThueDatRequest.IdQuyetDinhThueDat = checkQdTD.IdQuyetDinhThueDat;
                            var quyetDinhMienTienThueDat = await _QuyetDinhMienTienThueDatService.InsertUpdate(item.QuyetDinhMienTienThueDatRequest);
                            messageSuccess.Add(item.DoanhNghiepRequest.TenDoanhNghiep + " --- Thêm quyết định miễn tiền thuê đất thành công --- " + quyetDinhMienTienThueDat.Data);
                            ok++;
                        }
                        else
                        {
                            messageError.Add(item.DoanhNghiepRequest.TenDoanhNghiep + " lỗi khi lưu số QĐ miễn tiền thuê đất vào DB");
                        }
                    }
                    else
                    {
                        messageError.Add(item.DoanhNghiepRequest.TenDoanhNghiep + " lỗi không tìm thấy doanh nghiệp/QĐ khi lưu QĐ miễn tiền thuê đất vào DB");
                    }
                }
                catch (Exception e)
                {
                    messageError.Add(item.DoanhNghiepRequest.TenDoanhNghiep + " lỗi khi lưu QĐ miễn tiền thuê đất vào DB");
                    messageException.Add(e.Message);
                }

            }
            foreach (var item in listThongBaoTienSuDungDat)
            {
                try
                {
                    var checkDN = _context.DoanhNghiep.FirstOrDefault(x => x.MaSoThue == item.DoanhNghiepRequest.MaSoThue);
                    var checkQdTD = _context.QuyetDinhThueDat.FirstOrDefault(x => x.SoQuyetDinhThueDat == item.QuyetDinhThueDatRequest.SoQuyetDinhThueDat && x.NgayQuyetDinhThueDat == Convert.ToDateTime(item.QuyetDinhThueDatRequest.NgayQuyetDinhThueDat, new CultureInfo("vi-VN")));
                    if (checkDN != null && checkQdTD != null)
                    {
                        if (!string.IsNullOrEmpty(item.ThongBaoTienSuDungDatRequest.SoThongBaoTienSuDungDat))
                        {
                            item.ThongBaoTienSuDungDatRequest.IdDoanhNghiep = checkDN.IdDoanhNghiep;
                            item.ThongBaoTienSuDungDatRequest.IdQuyetDinhThueDat = checkQdTD.IdQuyetDinhThueDat;
                            var tbTienSuDungDat = await _ThongBaoTienSuDungDatService.InsertUpdate(item.ThongBaoTienSuDungDatRequest);
                            messageSuccess.Add(item.DoanhNghiepRequest.TenDoanhNghiep + " --- Thêm thông báo tiền sử dụng đất thành công --- " + tbTienSuDungDat.Data);
                            ok++;
                        }
                        else
                        {
                            messageError.Add(item.DoanhNghiepRequest.TenDoanhNghiep + " lỗi khi lưu số TB tiền sử dụng đất vào DB");
                        }
                    }
                    else
                    {
                        messageError.Add(item.DoanhNghiepRequest.TenDoanhNghiep + " lỗi không tìm thấy doanh nghiệp/QĐ khi lưu TB tiền sử dụng đất vào DB");
                    }

                }
                catch (Exception e)
                {
                    messageError.Add(item.DoanhNghiepRequest.TenDoanhNghiep + " lỗi khi lưu Tb tiền sử dụng đất vào DB");
                    messageException.Add(e.Message);
                }

            }

            foreach (var item in listHopDongThueDat)
            {
                try
                {
                    var checkDN = _context.DoanhNghiep.FirstOrDefault(x => x.MaSoThue == item.DoanhNghiepRequest.MaSoThue);
                    var checkQdTD = _context.QuyetDinhThueDat.FirstOrDefault(x => x.SoQuyetDinhThueDat == item.QuyetDinhThueDatRequest.SoQuyetDinhThueDat && x.NgayQuyetDinhThueDat == Convert.ToDateTime(item.QuyetDinhThueDatRequest.NgayQuyetDinhThueDat, new CultureInfo("vi-VN")));
                    if (checkDN != null && checkQdTD != null)
                    {
                        if (!string.IsNullOrEmpty(item.HopDongThueDatRequest.SoHopDong) && !item.HopDongThueDatRequest.SoHopDong.Contains("TLĐ"))
                        {
                            item.HopDongThueDatRequest.IdDoanhNghiep = checkDN.IdDoanhNghiep;
                            item.HopDongThueDatRequest.IdQuyetDinhThueDat = checkQdTD.IdQuyetDinhThueDat;
                            var hopDongThueDat = await _HopDongThueDatService.InsertUpdate(item.HopDongThueDatRequest);
                            messageSuccess.Add(item.DoanhNghiepRequest.TenDoanhNghiep + " --- Thêm hợp đồng thuê đất thành công --- " + hopDongThueDat.Data);
                        }
                        else
                        {
                            messageError.Add(item.DoanhNghiepRequest.TenDoanhNghiep + " lỗi khi lưu số hợp đồng vào DB");
                        }
                    }
                    else
                    {
                        messageError.Add(item.DoanhNghiepRequest.TenDoanhNghiep + " lỗi không tìm thấy doanh nghiệp/QĐ khi lưu hợp đồng vào DB");
                    }
                    ok++;
                }
                catch (Exception e)
                {
                    messageError.Add(item.DoanhNghiepRequest.TenDoanhNghiep + " lỗi khi lưu hợp đồng vào DB");
                    messageException.Add(e.Message);
                }

            }
            foreach (var item in listThongBaoDonGiaThueDat)
            {
                try
                {
                    var checkDN = _context.DoanhNghiep.FirstOrDefault(x => x.MaSoThue == item.DoanhNghiepRequest.MaSoThue);
                    var checkQdTD = _context.QuyetDinhThueDat.FirstOrDefault(x => x.SoQuyetDinhThueDat == item.QuyetDinhThueDatRequest.SoQuyetDinhThueDat && x.NgayQuyetDinhThueDat == Convert.ToDateTime(item.QuyetDinhThueDatRequest.NgayQuyetDinhThueDat, new CultureInfo("vi-VN")));
                    if (checkDN != null && checkQdTD != null)
                    {
                        if (!string.IsNullOrEmpty(item.ThongBaoDonGiaThueDatRequest.SoThongBaoDonGiaThueDat))
                        {
                            item.ThongBaoDonGiaThueDatRequest.IdDoanhNghiep = checkDN.IdDoanhNghiep;
                            item.ThongBaoDonGiaThueDatRequest.IdQuyetDinhThueDat = checkQdTD.IdQuyetDinhThueDat;
                            var tbDonGiaThueDat = await _ThongBaoDonGiaThueDatService.InsertUpdate(item.ThongBaoDonGiaThueDatRequest);
                            messageSuccess.Add(item.DoanhNghiepRequest.TenDoanhNghiep + " --- Thêm thông báo đơn giá đất thành công --- " + tbDonGiaThueDat.Data);
                            ok++;
                        }
                        else
                        {
                            messageError.Add(item.DoanhNghiepRequest.TenDoanhNghiep + " lỗi khi lưu số thông báo đơn giá vào DB");
                        }
                    }
                    else
                    {
                        messageError.Add(item.DoanhNghiepRequest.TenDoanhNghiep + " lỗi không tìm thấy doanh nghiệp/QĐ khi lưu thông báo đơn giá vào DB");
                    }

                }
                catch (Exception e)
                {
                    messageError.Add(item.DoanhNghiepRequest.TenDoanhNghiep + " lỗi khi lưu thông báo đơn giá thuê đất vào DB");
                    messageException.Add(e.Message);
                }
            }
            foreach (var item in listThongBaoTienThueDat)
            {
                try
                {
                    var checkDN = _context.DoanhNghiep.FirstOrDefault(x => x.MaSoThue == item.DoanhNghiepRequest.MaSoThue);
                    var checkQdTD = _context.QuyetDinhThueDat.FirstOrDefault(x => x.SoQuyetDinhThueDat == item.QuyetDinhThueDatRequest.SoQuyetDinhThueDat && x.NgayQuyetDinhThueDat == Convert.ToDateTime(item.QuyetDinhThueDatRequest.NgayQuyetDinhThueDat, new CultureInfo("vi-VN")));
                    if (checkDN != null && checkQdTD != null)
                    {
                        if (!string.IsNullOrEmpty(item.ThongBaoTienThueDatRequest.SoThongBaoTienThueDat))
                        {
                            item.ThongBaoTienThueDatRequest.IdDoanhNghiep = checkDN.IdDoanhNghiep;
                            item.ThongBaoTienThueDatRequest.IdQuyetDinhThueDat = checkQdTD.IdQuyetDinhThueDat;
                            var tbTienThueDat = await _ThongBaoTienThueDatService.InsertUpdate(item.ThongBaoTienThueDatRequest);
                            messageSuccess.Add(item.DoanhNghiepRequest.TenDoanhNghiep + " --- Thêm thông báo tiền thuê đất thành công --- " + tbTienThueDat.Data);
                            ok++;
                        }
                        else
                        {
                            messageError.Add(item.DoanhNghiepRequest.TenDoanhNghiep + " lỗi khi lưu số thông báo tiền thuê đất vào DB");
                        }
                    }
                    else
                    {
                        messageError.Add(item.DoanhNghiepRequest.TenDoanhNghiep + " lỗi không tìm thấy doanh nghiệp/QĐ khi lưu thông báo tiền thuê đất vào DB");
                    }

                }
                catch (Exception e)
                {
                    messageError.Add(item.DoanhNghiepRequest.TenDoanhNghiep + " lỗi khi lưu thông báo tiền thuê đất vào DB");
                    messageException.Add(e.Message);
                }
            }
            result.Add(ok + " Doanh nghiệp được nhập hoàn chỉnh");
            result.Add("========== DANH SÁCH THÀNH CÔNG ==========");
            result.AddRange(messageSuccess);
            result.Add("========== DANH SÁCH LỖI ==========");
            result.AddRange(messageError);
            result.Add("========== DANH SÁCH EXCEPTION ==========");
            result.AddRange(messageException);
            return new ApiSuccessResult<List<string>>() { Data = result };
        }

    }
}
