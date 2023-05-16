using Microsoft.AspNetCore.Mvc;
using QuanLyThueDat.WebApp.Models;
using QuanLyThueDat.WebApp.Service;
using System.Diagnostics;

namespace QuanLyThueDat.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IExportWordClient _exportWordClient;
        private readonly IExportExcelClient _exportExcelClient;
        public HomeController(ILogger<HomeController> logger, IExportWordClient exportWordClient, IExportExcelClient exportExcelClient)
        {
            _logger = logger;
            _exportWordClient = exportWordClient;
            _exportExcelClient = exportExcelClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [Route("CreateWordFile")]
        public async Task<IActionResult> _CreateWordFile(int idThongBao, string loaiThongBao)
        {
            var data = await _exportWordClient.CreateWordDocument(idThongBao, loaiThongBao);
            if (data.IsSuccess)
            {
                //var result = File(data.Data, "application/vnd.ms-word", loaiThongBao + ".doc");
                var result = File(data.Data, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", data.Message + ".docx");
                return result;
            }
            return Ok(data);

        }
        [Route("ExportThongBaoTienThueDatHangNam")]
        public async Task<IActionResult> _ExportThongBaoTienThueDatHangNam(int namThongBao)
        {
            var data = await _exportExcelClient.ExportThongBaoTienThueDatHangNam(namThongBao);
            if (data.IsSuccess)
            {
                //var result = File(data.Data, "application/vnd.ms-word", loaiThongBao + ".doc");
                var result = File(data.Data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DanhSachThongBaoTienThueDat.xlsx");
                return result;
            }
            return Ok(data);

        }
        [Route("ExportQuyetDinhMienTienThueDat")]
        public async Task<IActionResult> _ExportQuyetDinhMienTienThueDat(int? idQuyetDinhMienTienThueDat)
        {
            var data = await _exportExcelClient.ExportQuyetDinhMienTienThueDat(idQuyetDinhMienTienThueDat);
            if (data.IsSuccess)
            {
                //var result = File(data.Data, "application/vnd.ms-word", loaiThongBao + ".doc");
                var result = File(data.Data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Bao_Cao_Mien_Tien_Thue_Dat.xlsx");
                return result;
            }
            return Ok(data);

        }
        [Route("ExportBaoCaoDoanhNghiepThueDat")]
        public async Task<IActionResult> _ExportBaoCaoDoanhNghiepThueDat()
        {
            var data = await _exportExcelClient.ExportBaoCaoDoanhNghiepThueDat();
            if (data.IsSuccess)
            {
                //var result = File(data.Data, "application/vnd.ms-word", loaiThongBao + ".doc");
                var result = File(data.Data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Bao_cao_doanh_nghiep_thue_dat.xlsx");
                return result;
            }
            return Ok(data);

        }
    }
}