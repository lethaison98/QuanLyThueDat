using Microsoft.AspNetCore.Mvc;

namespace QuanLyThueDat.WebApp.Controllers
{
    [Route("ThongBaoTienSuDungDat")]
    public class ThongBaoTienSuDungDatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [Route("PopupDetailThongBaoTienSuDungDat")]
        public ViewResult _PopupDetailThongBaoTienSuDungDat()
        {
            return View();
        }
    }
}
