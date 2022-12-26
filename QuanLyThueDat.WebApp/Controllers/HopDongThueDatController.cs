using Microsoft.AspNetCore.Mvc;

namespace QuanLyThueDat.WebApp.Controllers
{
    [Route("HopDongThueDat")]
    public class HopDongThueDatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [Route("PopupDetailHopDongThueDat")]
        public ViewResult _PopupDetailHopDongThueDat()
        {
            return View();
        }
        [Route("PopupViewHopDongThueDat")]
        public ViewResult _PopupViewHopDongThueDat()
        {
            return View();
        }
    }
}
