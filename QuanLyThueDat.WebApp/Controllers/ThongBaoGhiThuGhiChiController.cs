using Microsoft.AspNetCore.Mvc;

namespace QuanLyThueDat.WebApp.Controllers
{
    [Route("ThongBaoGhiThuGhiChi")]
    public class ThongBaoGhiThuGhiChiController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [Route("PopupDetailThongBaoGhiThuGhiChi")]
        public ViewResult _PopupDetailThongBaoGhiThuGhiChi()
        {
            return View();
        }
    }
}
