using DocumentFormat.OpenXml.Packaging;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using QuanLyThueDat.Application.Common.Constant;
using QuanLyThueDat.Application.Interfaces;
using QuanLyThueDat.Application.ViewModel;
using QuanLyThueDat.Application.ViewModel.BaoCaoViewModel;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace QuanLyThueDat.WebApp.Service
{
    public class ExportExcelClient : IExportExcelClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public ExportExcelClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<ApiResult<byte[]>> ExportThongBaoTienThueDatHangNam(int namThongBao, int? idQuanHuyen, string keyword, string tuNgay, string denNgay)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            var pathFileTemplate = "";
            var data = new List<ThongBaoTienThueDatViewModel>();
            var response = new HttpResponseMessage();
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };

            var client = new HttpClient(handler);
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            pathFileTemplate = "Assets/Template/MauBaoCaoThongBaoTienThueDat.xlsx";
            response = await client.GetAsync("/api/BaoCao/BaoCaoTienThueDat?&nam=" + namThongBao + "&idQuanHuyen="+idQuanHuyen+"&keyword="+keyword+"&tuNgay=" + tuNgay + "&denNgay=" + denNgay);
            if (response.IsSuccessStatusCode)
            {
                data = JsonConvert.DeserializeObject<ApiResult<List<ThongBaoTienThueDatViewModel>>>(await response.Content.ReadAsStringAsync()).Data;
            }

            var result = new ApiSuccessResult<byte[]>();
            var file = new FileInfo(pathFileTemplate);
            var p = new ExcelPackage(file);
            var wb = p.Workbook;
            var ws = wb.Worksheets.FirstOrDefault();
            p.Compression = CompressionLevel.Default;
            if (ws != null)
            {
                ExcelWorksheetView wv = ws.View;
                wv.ZoomScale = 100;
                wv.RightToLeft = false;
                ws.PrinterSettings.Orientation = eOrientation.Landscape;
                ws.Cells.AutoFitColumns();
                var i = 0;
                decimal tongSoTien = 0;
                decimal tongSoTienMienGiam = 0;
                decimal tongSoTienPhaiNop = 0;
                ws.Cells[4, 1].Value = "BÁO CÁO THÔNG BÁO NỘP TIỀN THUÊ ĐẤT NĂM " + namThongBao + " CỦA CÁC DN THUÊ ĐẤT TẠI KKT VÀ CÁC KCN";
                ws.Cells[5, 10].Value = "Thông báo nộp tiền thuê đất năm " + namThongBao;
                foreach (var obj in data)
                {
                    i++;
                    ws.Cells[6 + i, 1].Value = i.ToString();
                    ws.Cells[6 + i, 2].Value = obj.CoQuanQuanLyThue;
                    ws.Cells[6 + i, 3].Value = obj.TenDoanhNghiep;
                    ws.Cells[6 + i, 4].Value = obj.MaSoThue;
                    ws.Cells[6 + i, 5].Value = obj.DiaChi;
                    ws.Cells[6 + i, 6].Value = obj.DiaChiThuaDat;
                    ws.Cells[6 + i, 7].Value = obj.SoThongBaoDonGiaThueDat;
                    ws.Cells[6 + i, 8].Value = decimal.Parse(obj.DonGia, new CultureInfo("vi-VN"));
                    ws.Cells[6 + i, 9].Value = obj.ThoiHanDonGiaThueDat;
                    ws.Cells[6 + i, 10].Value = obj.SoThongBaoTienThueDat;
                    ws.Cells[6 + i, 11].Value = obj.NgayThongBaoTienThueDat;
                    ws.Cells[6 + i, 12].Value = decimal.Parse(obj.SoTien, new CultureInfo("vi-VN"));
                    ws.Cells[6 + i, 13].Value = decimal.Parse(obj.SoTienMienGiam, new CultureInfo("vi-VN"));
                    ws.Cells[6 + i, 14].Value = decimal.Parse(obj.SoTienPhaiNop, new CultureInfo("vi-VN"));
                    ws.Cells[6 + i, 15].Value = obj.GhiChu;
                    tongSoTien += decimal.Parse(obj.SoTien, new CultureInfo("vi-VN"));
                    tongSoTienMienGiam += decimal.Parse(obj.SoTienMienGiam, new CultureInfo("vi-VN"));
                    tongSoTienPhaiNop += decimal.Parse(obj.SoTienPhaiNop, new CultureInfo("vi-VN"));
                }
                ws.Cells[7 + i, 11, 7 + i, 14].Style.Font.Bold = true;
                ws.Cells[7 + i, 11].Value = "Tổng cộng";
                //ws.Cells[7 + i, 11].Value = tongSoTien;
                //ws.Cells[7 + i, 12].Value = tongSoTienMienGiam;
                //ws.Cells[7 + i, 13].Value = tongSoTienPhaiNop;
                ws.Cells[7 + i, 12].Formula = "=SUM(" + ws.Cells[7, 12].Address + ":" + ws.Cells[6 + i, 12].Address + ")";
                ws.Cells[7 + i, 13].Formula = "=SUM(" + ws.Cells[7, 13].Address + ":" + ws.Cells[6 + i, 13].Address + ")"; ;
                ws.Cells[7 + i, 14].Formula = "=SUM(" + ws.Cells[7, 14].Address + ":" + ws.Cells[6 + i, 14].Address + ")"; ;
                ws.Cells[7, 1, i + 7, 15].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                ws.Cells[7, 1, i + 7, 15].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                ws.Cells[7, 1, i + 7, 15].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                ws.Cells[7, 1, i + 7, 15].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                ws.Cells[7, 10, i + 7, 15].Style.Numberformat.Format = "#,##0.00";
                ws.Cells[7, 7, i + 7, 7].Style.Numberformat.Format = "#,##0.00";
                ws.Cells.AutoFitColumns();
                result.Data = p.GetAsByteArray();
            }
            return result;
        }
        public async Task<ApiResult<byte[]>> ExportThongBaoDonGiaThueDat(int? idQuanHuyen, string keyword, string tuNgay, string denNgay)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            var pathFileTemplate = "";
            var data = new List<ThongBaoDonGiaThueDatViewModel>();
            var response = new HttpResponseMessage();
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };

            var client = new HttpClient(handler);
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            pathFileTemplate = "Assets/Template/MauBaoCaoThongBaoDonGiaThueDat.xlsx";
            response = await client.GetAsync("/api/BaoCao/BaoCaoDonGiaThueDat?idQuanHuyen= " + idQuanHuyen + "&keyword=" + keyword + "&tuNgay=" + tuNgay + "&denNgay=" + denNgay);
            if (response.IsSuccessStatusCode)
            {
                data = JsonConvert.DeserializeObject<ApiResult<List<ThongBaoDonGiaThueDatViewModel>>>(await response.Content.ReadAsStringAsync()).Data;
            }

            var result = new ApiSuccessResult<byte[]>();
            var file = new FileInfo(pathFileTemplate);
            var p = new ExcelPackage(file);
            var wb = p.Workbook;
            var ws = wb.Worksheets.FirstOrDefault();
            p.Compression = CompressionLevel.Default;
            if (ws != null)
            {
                ExcelWorksheetView wv = ws.View;
                wv.ZoomScale = 100;
                wv.RightToLeft = false;
                ws.PrinterSettings.Orientation = eOrientation.Landscape;
                ws.Cells.AutoFitColumns();
                var i = 0;
                ws.Cells[4, 1].Value = "BÁO CÁO THÔNG BÁO ĐƠN GIÁ THUÊ ĐẤT CỦA CÁC DN THUÊ ĐẤT TẠI KKT VÀ CÁC KCN";
                foreach (var obj in data)
                {
                    i++;
                    ws.Cells[6 + i, 1].Value = i.ToString();
                    ws.Cells[6 + i, 2].Value = obj.CoQuanQuanLyThue;
                    ws.Cells[6 + i, 3].Value = obj.TenDoanhNghiep;
                    ws.Cells[6 + i, 4].Value = obj.MaSoThue;
                    ws.Cells[6 + i, 5].Value = obj.DiaChi;
                    ws.Cells[6 + i, 6].Value = obj.SoThongBaoDonGiaThueDat;
                    ws.Cells[6 + i, 7].Value = obj.NgayThongBaoDonGiaThueDat;
                    ws.Cells[6 + i, 8].Value = decimal.Parse(obj.DonGia, new CultureInfo("vi-VN"));
                    ws.Cells[6 + i, 9].Value = obj.ThoiHanDonGia;
                }
                ws.Cells[6, 1, i + 6, 9].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                ws.Cells[6, 1, i + 6, 9].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                ws.Cells[6, 1, i + 6, 9].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                ws.Cells[6, 1, i + 6, 9].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                ws.Cells[6, 8, i + 6, 8].Style.Numberformat.Format = "#,##0.00";
                ws.Cells.AutoFitColumns();

                result.Data = p.GetAsByteArray();
            }
            return result;
        }
        public async Task<ApiResult<byte[]>> ExportQuyetDinhMienTienThueDat(int? idQuyetDinhMienTienThueDat, int? idQuanHuyen, string keyword,  string tuNgay, string denNgay)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            var pathFileTemplate = "";
            var data = new List<QuyetDinhMienTienThueDatViewModel>();
            var response = new HttpResponseMessage();
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };

            var client = new HttpClient(handler);
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            pathFileTemplate = "Assets/Template/MauBaoCaoQuyetDinhMienTienThueDat.xlsx";
            if (idQuyetDinhMienTienThueDat == null)
            {
                response = await client.GetAsync("/api/BaoCao/BaoCaoMienTienThueDat?idQuanHuyen= "+idQuanHuyen+"&keyword="+keyword+"&tuNgay=" + tuNgay + "&denNgay=" + denNgay);
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<ApiResult<List<QuyetDinhMienTienThueDatViewModel>>>(await response.Content.ReadAsStringAsync()).Data;
                }
            }
            else
            {
                response = await client.GetAsync("/api/QuyetDinhMienTienThueDat/GetById?idQuyetDinhMienTienThueDat=" + idQuyetDinhMienTienThueDat);
                if (response.IsSuccessStatusCode)
                {
                    data.Add(JsonConvert.DeserializeObject<ApiResult<QuyetDinhMienTienThueDatViewModel>>(await response.Content.ReadAsStringAsync()).Data);
                }
            }
            var result = new ApiSuccessResult<byte[]>();
            var file = new FileInfo(pathFileTemplate);
            var p = new ExcelPackage(file);
            var wb = p.Workbook;
            var ws = wb.Worksheets.FirstOrDefault();
            p.Compression = CompressionLevel.Default;
            if (ws != null)
            {
                ExcelWorksheetView wv = ws.View;
                wv.ZoomScale = 100;
                wv.RightToLeft = false;
                ws.PrinterSettings.Orientation = eOrientation.Landscape;
                ws.Cells.AutoFitColumns();
                var i = 0;
                decimal tongSoTien = 0;
                decimal tongSoTienMienGiam = 0;
                decimal tongSoTienPhaiNop = 0;
                ws.Cells[4, 1].Value = "BÁO CÁO MIỄN TIỀN THUÊ ĐẤT CỦA CÁC DN THUÊ ĐẤT TẠI KKT VÀ CÁC KCN";
                ws.Cells[5, 6].Value = "Quyết định miễn tiền thuê đất";
                foreach (var obj in data)
                {
                    i++;
                    ws.Cells[6 + i, 1].Value = i.ToString();
                    ws.Cells[6 + i, 2].Value = obj.TenDoanhNghiep;
                    ws.Cells[6 + i, 3].Value = obj.MaSoThue;
                    ws.Cells[6 + i, 4].Value = obj.DiaChiThuaDat;
                    ws.Cells[6 + i, 5].Value = obj.ViTriThuaDat;
                    ws.Cells[6 + i, 6].Value = obj.SoQuyetDinhMienTienThueDat + " ngày " + obj.NgayQuyetDinhMienTienThueDat;
                    ws.Cells[6 + i, 7].Value = obj.ThoiHanMienTienThueDat;
                    ws.Cells[6 + i, 8].Value = "Từ ngày " + obj.NgayHieuLucMienTienThueDat + " đến ngày " + obj.NgayHetHieuLucMienTienThueDat;
                    ws.Cells[6 + i, 9].Value = decimal.Parse(obj.DienTichMienTienThueDat, new CultureInfo("vi-VN"));
                    ws.Cells[6 + i, 10].Value = decimal.Parse(obj.SoTienMienGiamTrongMotNam, new CultureInfo("vi-VN"));
                    ws.Cells[6 + i, 11].Value = decimal.Parse(obj.TongSoTienMienGiam, new CultureInfo("vi-VN"));
                    ws.Cells[6 + i, 12].Value = obj.GhiChu;
                }
                //ws.Cells[7 + i, 9, 7 + i, 12].Style.Font.Bold = true;
                //ws.Cells[7 + i, 9].Value = "Tổng cộng";
                //ws.Cells[7 + i, 10].Value = tongSoTien;
                //ws.Cells[7 + i, 11].Value = tongSoTienMienGiam;
                //ws.Cells[7 + i, 12].Value = tongSoTienPhaiNop;
                ws.Cells[7 + i, 2].Value = "Tổng cộng";
                ws.Cells[7 + i, 9].Formula = "=SUM(" + ws.Cells[7, 9].Address + ":" + ws.Cells[6 + i, 9].Address + ")";
                ws.Cells[7 + i, 10].Formula = "=SUM(" + ws.Cells[7, 10].Address + ":" + ws.Cells[6 + i, 10].Address + ")"; ;
                ws.Cells[7 + i, 11].Formula = "=SUM(" + ws.Cells[7, 11].Address + ":" + ws.Cells[6 + i, 11].Address + ")"; ;
                ws.Cells[6, 1, i + 6, 12].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                ws.Cells[6, 1, i + 6, 12].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                ws.Cells[6, 1, i + 6, 12].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                ws.Cells[6, 1, i + 6, 12].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                ws.Cells[7, 9, i + 7, 11].Style.Numberformat.Format = "#,##0.00";
                ws.Cells.AutoFitColumns();
                result.Data = p.GetAsByteArray();
            }
            return result;
        }

        public async Task<ApiResult<byte[]>> ExportBaoCaoDoanhNghiepThueDat()
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            var pathFileTemplate = "";
            var data = new List<BaoCaoDoanhNghiepThueDatViewModel>();
            var response = new HttpResponseMessage();
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };

            var client = new HttpClient(handler);
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            pathFileTemplate = "Assets/Template/MauBaoCaoDoanhNghiepThueDat.xlsx";
            response = await client.GetAsync("/api/BaoCao/BaoCaoDoanhNghiepThueDat");
            if (response.IsSuccessStatusCode)
            {
                data = JsonConvert.DeserializeObject<ApiResult<List<BaoCaoDoanhNghiepThueDatViewModel>>>(await response.Content.ReadAsStringAsync()).Data;
            }

            var result = new ApiSuccessResult<byte[]>();
            var file = new FileInfo(pathFileTemplate);
            var p = new ExcelPackage(file);
            var wb = p.Workbook;
            var ws = wb.Worksheets.FirstOrDefault();
            p.Compression = CompressionLevel.Default;
            if (ws != null)
            {
                ExcelWorksheetView wv = ws.View;
                wv.ZoomScale = 100;
                wv.RightToLeft = false;
                ws.PrinterSettings.Orientation = eOrientation.Landscape;
                ws.Cells.AutoFitColumns();
                var i = 0;
                //decimal tongSoTien = 0;
                //decimal tongSoTienMienGiam = 0;
                //decimal tongSoTienPhaiNop = 0;
                foreach (var obj in data)
                {
                    i++;
                    ws.Cells[6 + i, 1].Value = i.ToString();
                    ws.Cells[6 + i, 2].Value = obj.DoanhNghiepViewModel.CoQuanQuanLyThue;
                    ws.Cells[6 + i, 3].Value = obj.DoanhNghiepViewModel.TenDoanhNghiep;
                    ws.Cells[6 + i, 4].Value = obj.DoanhNghiepViewModel.MaSoThue;
                    ws.Cells[6 + i, 5].Value = obj.DoanhNghiepViewModel.DiaChi;
                    ws.Cells[6 + i, 6].Value = obj.QuyetDinhThueDatViewModel.SoQuyetDinhGiaoDat;
                    ws.Cells[6 + i, 7].Value = obj.QuyetDinhThueDatViewModel.NgayQuyetDinhGiaoDat;

                    ws.Cells[6 + i, 8].Value = obj.QuyetDinhGiaoLaiDatViewModel.SoQuyetDinh;
                    ws.Cells[6 + i, 9].Value = obj.QuyetDinhGiaoLaiDatViewModel.DienTichPhaiNop != null ? decimal.Parse(obj.QuyetDinhGiaoLaiDatViewModel.DienTichPhaiNop, new CultureInfo("vi-VN")) : "";
                    ws.Cells[6 + i, 10].Value = obj.QuyetDinhGiaoLaiDatViewModel.DienTichKhongPhaiNop != null ? decimal.Parse(obj.QuyetDinhGiaoLaiDatViewModel.DienTichKhongPhaiNop, new CultureInfo("vi-VN")) : "";
                    ws.Cells[6 + i, 11].Value = obj.QuyetDinhGiaoLaiDatViewModel.TongDienTich != null ? decimal.Parse(obj.QuyetDinhGiaoLaiDatViewModel.TongDienTich, new CultureInfo("vi-VN")) : "";

                    ws.Cells[6 + i, 13].Value = obj.QuyetDinhThueDatViewModel.SoQuyetDinhThueDat;
                    ws.Cells[6 + i, 14].Value = obj.QuyetDinhThueDatViewModel.NgayQuyetDinhThueDat;
                    ws.Cells[6 + i, 15].Value = obj.QuyetDinhThueDatViewModel.TongDienTich != null ? decimal.Parse(obj.QuyetDinhThueDatViewModel.TongDienTich, new CultureInfo("vi-VN")) : "";
                    ws.Cells[6 + i, 16].Value = obj.QuyetDinhThueDatViewModel.ThoiHanThue + obj.QuyetDinhThueDatViewModel.TuNgayThue != "" ? " từ ngày " + obj.QuyetDinhThueDatViewModel.TuNgayThue : "" + obj.QuyetDinhThueDatViewModel.DenNgayThue != "" ? " đến ngày " + obj.QuyetDinhThueDatViewModel.DenNgayThue : "";
                    ws.Cells[6 + i, 17].Value = obj.QuyetDinhThueDatViewModel.MucDichSuDung;
                    ws.Cells[6 + i, 18].Value = obj.QuyetDinhThueDatViewModel.ViTriThuaDat;
                    ws.Cells[6 + i, 19].Value = obj.QuyetDinhThueDatViewModel.DiaChiThuaDat;
                    ws.Cells[6 + i, 20].Value = obj.HopDongThueDatViewModel.SoHopDong;
                    ws.Cells[6 + i, 21].Value = obj.HopDongThueDatViewModel.NgayKyHopDong;
                    ws.Cells[6 + i, 22].Value = obj.QuyetDinhThueDatViewModel.TongDienTich != null ? decimal.Parse(obj.QuyetDinhThueDatViewModel.TongDienTich, new CultureInfo("vi-VN")) : "";
                    ws.Cells[6 + i, 23].Value = obj.ThongBaoDonGiaThueDatViewModel.SoThongBaoDonGiaThueDat;
                    ws.Cells[6 + i, 24].Value = obj.ThongBaoDonGiaThueDatViewModel.NgayThongBaoDonGiaThueDat;
                    ws.Cells[6 + i, 25].Value = obj.ThongBaoDonGiaThueDatViewModel.DonGia;
                    ws.Cells[6 + i, 26].Value = obj.ThongBaoDonGiaThueDatViewModel.ThoiHanDonGia;
                    ws.Cells[6 + i, 27].Value = obj.DoanhNghiepViewModel.GhiChu;
                }
                ws.Cells[7 + i, 9, 7 + i, 27].Style.Font.Bold = true;
                //ws.Cells[7 + i, 9].Value = "Tổng cộng";
                //ws.Cells[7 + i, 10].Value = tongSoTien;
                //ws.Cells[7 + i, 11].Value = tongSoTienMienGiam;
                //ws.Cells[7 + i, 12].Value = tongSoTienPhaiNop;
                ws.Cells[7 + i, 2].Value = "Tổng cộng";
                ws.Cells[7 + i, 9].Formula = "=SUM(" + ws.Cells[7, 9].Address + ":" + ws.Cells[6 + i, 9].Address + ")";
                ws.Cells[7 + i, 10].Formula = "=SUM(" + ws.Cells[7, 10].Address + ":" + ws.Cells[6 + i, 10].Address + ")"; ;
                ws.Cells[7 + i, 11].Formula = "=SUM(" + ws.Cells[7, 11].Address + ":" + ws.Cells[6 + i, 11].Address + ")"; ;
                ws.Cells[7 + i, 15].Formula = "=SUM(" + ws.Cells[7, 15].Address + ":" + ws.Cells[6 + i, 15].Address + ")"; ;
                ws.Cells[7 + i, 22].Formula = "=SUM(" + ws.Cells[7, 22].Address + ":" + ws.Cells[6 + i, 22].Address + ")"; ;

                ws.Cells[7, 1, i + 6, 27].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                ws.Cells[7, 1, i + 6, 27].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                ws.Cells[7, 1, i + 6, 27].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                ws.Cells[7, 1, i + 6, 27].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                ws.Cells[7, 9, i + 7, 11].Style.Numberformat.Format = "#,##0.00";
                ws.Cells[7, 15, i + 7, 15].Style.Numberformat.Format = "#,##0.00";
                ws.Cells[7, 22, i + 7, 22].Style.Numberformat.Format = "#,##0.00";
                ws.Cells.AutoFitColumns();
                ws.Column(5).Width = 40;
                ws.Column(17).Width = 30;
                ws.Column(18).Width = 30;
                ws.Column(19).Width = 30;
                result.Data = p.GetAsByteArray();
            }
            return result;
        }

        public async Task<ApiResult<byte[]>> ExportBieuLapBo()
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            var pathFileTemplate = "";
            var data = new List<BaoCaoDoanhNghiepThueDatViewModel>();
            var response = new HttpResponseMessage();
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };

            var client = new HttpClient(handler);
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            pathFileTemplate = "Assets/Template/MauBieuLapBo.xlsx";
            response = await client.GetAsync("/api/BaoCao/BaoCaoBieuLapBo");
            if (response.IsSuccessStatusCode)
            {
                data = JsonConvert.DeserializeObject<ApiResult<List<BaoCaoDoanhNghiepThueDatViewModel>>>(await response.Content.ReadAsStringAsync()).Data;
            }

            var result = new ApiSuccessResult<byte[]>();
            var file = new FileInfo(pathFileTemplate);
            var p = new ExcelPackage(file);
            var wb = p.Workbook;
            var ws = wb.Worksheets.FirstOrDefault();
            p.Compression = CompressionLevel.Default;
            if (ws != null)
            {
                ExcelWorksheetView wv = ws.View;
                wv.ZoomScale = 100;
                wv.RightToLeft = false;
                ws.PrinterSettings.Orientation = eOrientation.Landscape;
                ws.Cells.AutoFitColumns();
                var i = 0;
                //decimal tongSoTien = 0;
                //decimal tongSoTienMienGiam = 0;
                //decimal tongSoTienPhaiNop = 0;
                var maxCol = 28;
                foreach (var obj in data)
                {
                    i++;
                    ws.Cells[6 + i, 1].Value = i.ToString();
                    ws.Cells[6 + i, 2].Value = obj.DoanhNghiepViewModel.CoQuanQuanLyThue;
                    ws.Cells[6 + i, 3].Value = obj.DoanhNghiepViewModel.TenDoanhNghiep;
                    ws.Cells[6 + i, 4].Value = obj.DoanhNghiepViewModel.MaSoThue;
                    ws.Cells[6 + i, 5].Value = obj.DoanhNghiepViewModel.DiaChi;
                    ws.Cells[6 + i, 6].Value = obj.QuyetDinhThueDatViewModel.SoQuyetDinhGiaoDat;
                    ws.Cells[6 + i, 7].Value = obj.QuyetDinhThueDatViewModel.NgayQuyetDinhGiaoDat;

                    ws.Cells[6 + i, 8].Value = obj.QuyetDinhGiaoLaiDatViewModel.SoQuyetDinh;
                    ws.Cells[6 + i, 9].Value = obj.QuyetDinhGiaoLaiDatViewModel.DienTichPhaiNop != null ? decimal.Parse(obj.QuyetDinhGiaoLaiDatViewModel.DienTichPhaiNop, new CultureInfo("vi-VN")) : "";
                    ws.Cells[6 + i, 10].Value = obj.QuyetDinhGiaoLaiDatViewModel.DienTichKhongPhaiNop != null ? decimal.Parse(obj.QuyetDinhGiaoLaiDatViewModel.DienTichKhongPhaiNop, new CultureInfo("vi-VN")) : "";
                    ws.Cells[6 + i, 11].Value = obj.QuyetDinhGiaoLaiDatViewModel.TongDienTich != null ? decimal.Parse(obj.QuyetDinhGiaoLaiDatViewModel.TongDienTich, new CultureInfo("vi-VN")) : "";

                    ws.Cells[6 + i, 13].Value = obj.QuyetDinhThueDatViewModel.SoQuyetDinhThueDat;
                    ws.Cells[6 + i, 14].Value = obj.QuyetDinhThueDatViewModel.NgayQuyetDinhThueDat;
                    ws.Cells[6 + i, 15].Value = obj.QuyetDinhThueDatViewModel.TongDienTich != null ? decimal.Parse(obj.QuyetDinhThueDatViewModel.TongDienTich, new CultureInfo("vi-VN")) : "";
                    ws.Cells[6 + i, 16].Value = obj.QuyetDinhThueDatViewModel.ThoiHanThue + obj.QuyetDinhThueDatViewModel.TuNgayThue != "" ? " từ ngày " + obj.QuyetDinhThueDatViewModel.TuNgayThue : "" + obj.QuyetDinhThueDatViewModel.DenNgayThue != "" ? " đến ngày " + obj.QuyetDinhThueDatViewModel.DenNgayThue : "";
                    ws.Cells[6 + i, 17].Value = obj.QuyetDinhThueDatViewModel.MucDichSuDung;
                    ws.Cells[6 + i, 18].Value = obj.QuyetDinhThueDatViewModel.ViTriThuaDat;
                    ws.Cells[6 + i, 19].Value = obj.QuyetDinhThueDatViewModel.DiaChiThuaDat;
                    ws.Cells[6 + i, 20].Value = obj.HopDongThueDatViewModel.SoHopDong;
                    ws.Cells[6 + i, 21].Value = obj.HopDongThueDatViewModel.NgayKyHopDong;
                    ws.Cells[6 + i, 22].Value = obj.QuyetDinhThueDatViewModel.TongDienTich != null ? decimal.Parse(obj.QuyetDinhThueDatViewModel.TongDienTich, new CultureInfo("vi-VN")) : "";
                    ws.Cells[6 + i, 23].Value = obj.QuyetDinhMienTienThueDatViewModel.SoQuyetDinhMienTienThueDat;
                    ws.Cells[6 + i, 24].Value = obj.QuyetDinhMienTienThueDatViewModel.NgayQuyetDinhMienTienThueDat;
                    ws.Cells[6 + i, 25].Value = obj.QuyetDinhMienTienThueDatViewModel.ThoiHanMienTienThueDat + (!String.IsNullOrEmpty(obj.QuyetDinhMienTienThueDatViewModel.NgayHieuLucMienTienThueDat) ? " từ ngày " + obj.QuyetDinhMienTienThueDatViewModel.NgayHieuLucMienTienThueDat : "")
                                                + (!String.IsNullOrEmpty(obj.QuyetDinhMienTienThueDatViewModel.NgayHetHieuLucMienTienThueDat) ? " đến ngày " + obj.QuyetDinhMienTienThueDatViewModel.NgayHetHieuLucMienTienThueDat : "");
                    ws.Cells[6 + i, 26].Value = obj.QuyetDinhMienTienThueDatViewModel.DienTichMienTienThueDat != null ? decimal.Parse(obj.QuyetDinhMienTienThueDatViewModel.DienTichMienTienThueDat, new CultureInfo("vi-VN")) : "";

                    ws.Cells[6 + i, 27].Value = obj.ThongBaoDonGiaThueDatViewModel.SoThongBaoDonGiaThueDat;
                    ws.Cells[6 + i, 28].Value = obj.ThongBaoDonGiaThueDatViewModel.NgayThongBaoDonGiaThueDat;
                    ws.Cells[6 + i, 29].Value = obj.ThongBaoDonGiaThueDatViewModel.DonGia;
                    ws.Cells[6 + i, 30].Value = obj.ThongBaoDonGiaThueDatViewModel.ThoiHanDonGia;
                    ws.Cells[6 + i, 31].Value = obj.DoanhNghiepViewModel.GhiChu;
                    foreach (var tbtd in obj.DsThongBaoTienThueDatViewModel)
                    {
                        // bắt đầu từ cột số 32 ứng với số thông báo của năm 2018. Mỗi năm sẽ có 4 cột
                        var col = 32 + (tbtd.Nam - 2018) * 4;
                        if (col + 3 > maxCol) maxCol = col + 3;
                        ws.Cells[6 + i, col].Value = tbtd.SoThongBaoTienThueDat + " ngày " + tbtd.NgayThongBaoTienThueDat;
                        ws.Cells[6 + i, col + 1].Value = decimal.Parse(tbtd.SoTien, new CultureInfo("vi-VN"));
                        ws.Cells[6 + i, col + 2].Value = decimal.Parse(tbtd.SoTienMienGiam, new CultureInfo("vi-VN"));
                        ws.Cells[6 + i, col + 3].Value = decimal.Parse(tbtd.SoTienPhaiNop, new CultureInfo("vi-VN"));
                    }
                }
                // Tính số năm để hiển thị tên cột
                for (int c = 32; c <= maxCol; c += 4)
                {
                    int nam = (c - 32) / 4 + 2018;
                    ws.Cells[5, c, 5, c + 3].Merge = true;
                    ws.Cells[5, c, 5, c + 3].Value = "Thông báo tiền thuê đất năm " + nam;

                    ws.Cells[6, c, 6, c].Value = "Số thông báo";
                    ws.Cells[6, c + 1].Value = "Số tiền";
                    ws.Cells[6, c + 2].Value = "Số tiền miễn giảm";
                    ws.Cells[6, c + 3].Value = "Số tiền phải nộp";
                }
                ws.Cells[5, 32, 6, maxCol].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[5, 32, 6, maxCol].Style.Font.Bold = true;

                ws.Cells[7 + i, 9, 7 + i, maxCol].Style.Font.Bold = true;

                ws.Cells[7 + i, 2].Value = "Tổng cộng";
                ws.Cells[7 + i, 9].Formula = "=SUM(" + ws.Cells[7, 9].Address + ":" + ws.Cells[6 + i, 9].Address + ")";
                ws.Cells[7 + i, 10].Formula = "=SUM(" + ws.Cells[7, 10].Address + ":" + ws.Cells[6 + i, 10].Address + ")"; ;
                ws.Cells[7 + i, 11].Formula = "=SUM(" + ws.Cells[7, 11].Address + ":" + ws.Cells[6 + i, 11].Address + ")"; ;
                ws.Cells[7 + i, 15].Formula = "=SUM(" + ws.Cells[7, 15].Address + ":" + ws.Cells[6 + i, 15].Address + ")"; ;
                ws.Cells[7 + i, 22].Formula = "=SUM(" + ws.Cells[7, 22].Address + ":" + ws.Cells[6 + i, 22].Address + ")"; ;
                ws.Cells[7 + i, 26].Formula = "=SUM(" + ws.Cells[7, 26].Address + ":" + ws.Cells[6 + i, 26].Address + ")"; ;

                ws.Cells[5, 1, i + 6, maxCol].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                ws.Cells[5, 1, i + 6, maxCol].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                ws.Cells[5, 1, i + 6, maxCol].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                ws.Cells[5, 1, i + 6, maxCol].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                ws.Cells[7, 9, i + 7, 11].Style.Numberformat.Format = "#,##0.00";
                ws.Cells[7, 15, i + 7, 15].Style.Numberformat.Format = "#,##0.00";
                ws.Cells[7, 22, i + 7, 22].Style.Numberformat.Format = "#,##0.00";
                ws.Cells.AutoFitColumns();
                ws.Column(5).Width = 40;
                ws.Column(17).Width = 30;
                ws.Column(18).Width = 30;
                ws.Column(19).Width = 30;
                result.Data = p.GetAsByteArray();
            }
            return result;
        }
    }
}
