using DocumentFormat.OpenXml.Packaging;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using QuanLyThueDat.Application.Common.Constant;
using QuanLyThueDat.Application.Interfaces;
using QuanLyThueDat.Application.ViewModel;
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

        public async Task<ApiResult<byte[]>> ExportThongBaoTienThueDatHangNam(int namThongBao)
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
            response = await client.GetAsync("/api/ThongBaoTienThueDat/GetAllPaging?idDoanhNghiep=&nam=" + namThongBao+ "&keyword=&pageIndex=1&pageSize=999999");
            if (response.IsSuccessStatusCode)
            {
                data = JsonConvert.DeserializeObject<ApiResult<PageViewModel<ThongBaoTienThueDatViewModel>>>(await response.Content.ReadAsStringAsync()).Data.Items;
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
                ws.Cells[4, 1].Value = "BÁO CÁO THÔNG BÁO NỘP TIỀN THUÊ ĐẤT NĂM "+ namThongBao + " CỦA CÁC DN THUÊ ĐẤT TẠI KKT VÀ CÁC KCN";
                ws.Cells[5, 9].Value = "Thông báo nộp tiền thuê đất năm " + namThongBao;
                foreach (var obj in data)
                {
                    i++;
                    ws.Cells[6 + i, 1].Value = i.ToString();
                    ws.Cells[6 + i, 2].Value = obj.CoQuanQuanLyThue;
                    ws.Cells[6 + i, 3].Value = obj.TenDoanhNghiep;
                    ws.Cells[6 + i, 4].Value = obj.MaSoThue;
                    ws.Cells[6 + i, 5].Value = obj.DiaChi;
                    ws.Cells[6 + i, 6].Value = obj.SoThongBaoDonGiaThueDat;
                    ws.Cells[6 + i, 7].Value = decimal.Parse(obj.DonGia, new CultureInfo("vi-VN"));
                    ws.Cells[6 + i, 8].Value = obj.ThoiHanDonGiaThueDat;
                    ws.Cells[6 + i, 9].Value = obj.SoThongBaoTienThueDat;
                    ws.Cells[6 + i, 10].Value = decimal.Parse(obj.SoTien, new CultureInfo("vi-VN"));
                    ws.Cells[6 + i, 11].Value = decimal.Parse(obj.SoTienMienGiam, new CultureInfo("vi-VN"));
                    ws.Cells[6 + i, 12].Value = decimal.Parse(obj.SoTienPhaiNop, new CultureInfo("vi-VN"));
                    tongSoTien += decimal.Parse(obj.SoTien, new CultureInfo("vi-VN"));
                    tongSoTienMienGiam += decimal.Parse(obj.SoTienMienGiam, new CultureInfo("vi-VN"));
                    tongSoTienPhaiNop += decimal.Parse(obj.SoTienPhaiNop, new CultureInfo("vi-VN"));
                }
                ws.Cells[7 + i, 9, 7 + i, 12].Style.Font.Bold = true;
                ws.Cells[7 + i, 9].Value = "Tổng cộng";
                ws.Cells[7 + i, 10].Value = tongSoTien;
                ws.Cells[7 + i, 11].Value = tongSoTienMienGiam;
                ws.Cells[7 + i, 12].Value = tongSoTienPhaiNop;
                ws.Cells[7, 1, i + 7, 13].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                ws.Cells[7, 1, i + 7, 13].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                ws.Cells[7, 1, i + 7, 13].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                ws.Cells[7, 1, i + 7, 13].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                ws.Cells.AutoFitColumns();
                result.Data = p.GetAsByteArray();
            }
            return result;
        }
        public async Task<ApiResult<byte[]>> ExportQuyetDinhMienTienThueDat()
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
            response = await client.GetAsync("/api/QuyetDinhMienTienThueDat/GetAllPaging?idDoanhNghiep=&keyword=&pageIndex=1&pageSize=999999");
            if (response.IsSuccessStatusCode)
            {
                data = JsonConvert.DeserializeObject<ApiResult<PageViewModel<QuyetDinhMienTienThueDatViewModel>>>(await response.Content.ReadAsStringAsync()).Data.Items;
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
                ws.Cells[5, 4].Value = "Quyết định miễn tiền thuê đất";
                foreach (var obj in data)
                {
                    i++;
                    ws.Cells[6 + i, 1].Value = i.ToString();
                    ws.Cells[6 + i, 2].Value = obj.TenDoanhNghiep;
                    ws.Cells[6 + i, 3].Value = obj.MaSoThue;
                    ws.Cells[6 + i, 4].Value = obj.SoQuyetDinhMienTienThueDat;
                    ws.Cells[6 + i, 5].Value = obj.ThoiHanMienTienThueDat;
                    ws.Cells[6 + i, 6].Value = "Từ ngày "+ obj.NgayHieuLucMienTienThueDat +" đến ngày "+ obj.NgayHetHieuLucMienTienThueDat ;
                    ws.Cells[6 + i, 7].Value = obj.DienTichMienTienThueDat;
                }
                //ws.Cells[7 + i, 9, 7 + i, 12].Style.Font.Bold = true;
                //ws.Cells[7 + i, 9].Value = "Tổng cộng";
                //ws.Cells[7 + i, 10].Value = tongSoTien;
                //ws.Cells[7 + i, 11].Value = tongSoTienMienGiam;
                //ws.Cells[7 + i, 12].Value = tongSoTienPhaiNop;
                ws.Cells[7, 1, i + 6, 8].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                ws.Cells[7, 1, i + 6, 8].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                ws.Cells[7, 1, i + 6, 8].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                ws.Cells[7, 1, i + 6, 8].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                ws.Cells.AutoFitColumns();
                result.Data = p.GetAsByteArray();
            }
            return result;
        }
    }
}
