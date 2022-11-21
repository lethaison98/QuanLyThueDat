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
        public HomeController(ILogger<HomeController> logger, IExportWordClient exportWordClient) 
        {
            _logger = logger;
            _exportWordClient = exportWordClient;   
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
                var result = File(data.Data, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", loaiThongBao + ".docx");
                return result;
            }
            return Ok(data);

        }
    }
}