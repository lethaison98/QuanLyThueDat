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
            IHttpContextAccessor HttpContextAccessor )
        {
            _context = context;
            _QuyetDinhThueDatService = QuyetDinhThueDatService;
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
                    GhiChu = item.GhiChu
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
                    GhiChu = item.GhiChu
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
                            var imp = new ImportDoanhNghiepRequest();
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
                        messageSuccess.Add(item.DoanhNghiepRequest.TenDoanhNghiep + " --- Thêm quyết định thuê đất thành công --- "+ quyetDinhThueDat.Data);
                    }

                    if (!string.IsNullOrEmpty(item.HopDongThueDatRequest.SoHopDong))
                    {
                        item.HopDongThueDatRequest.IdDoanhNghiep = idDoanhNghiep;
                        var hopDongThueDat = await _HopDongThueDatService.InsertUpdate(item.HopDongThueDatRequest);
                        messageSuccess.Add(item.DoanhNghiepRequest.TenDoanhNghiep + " --- Thêm hợp đồng thành công --- "+ hopDongThueDat.Data);
                    }
                    if (!string.IsNullOrEmpty(item.QuyetDinhMienTienThueDatRequest.SoQuyetDinhMienTienThueDat))
                    {
                        item.QuyetDinhMienTienThueDatRequest.IdDoanhNghiep = idDoanhNghiep;
                        var quyetDinhMienTienThueDat = await _QuyetDinhMienTienThueDatService.InsertUpdate(item.QuyetDinhMienTienThueDatRequest);
                        messageSuccess.Add(item.DoanhNghiepRequest.TenDoanhNghiep + " --- Thêm quyết định miễn tiền thuê đất thành công --- "+ quyetDinhMienTienThueDat.Data);
                    }

                    if (!string.IsNullOrEmpty(item.ThongBaoDonGiaThueDatRequest.SoThongBaoDonGiaThueDat))
                    {
                        item.ThongBaoDonGiaThueDatRequest.IdDoanhNghiep = idDoanhNghiep;
                        var tbDonGiaThueDat = await _ThongBaoDonGiaThueDatService.InsertUpdate(item.ThongBaoDonGiaThueDatRequest);
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
    }
}
