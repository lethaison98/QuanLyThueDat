using Microsoft.AspNetCore.Mvc;

namespace QuanLyThueDat.WebApp.Controllers
{
    [Route("ThongBaoDonGiaThueDat")]
    public class ThongBaoDonGiaThueDatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [Route("PopupDetailThongBaoDonGiaThueDat")]
        public ViewResult _PopupDetailThongBaoDonGiaThueDat()
        {
            return View();
        }
    }
}
