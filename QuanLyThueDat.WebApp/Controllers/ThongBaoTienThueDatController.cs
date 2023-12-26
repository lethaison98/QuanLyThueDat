using Microsoft.AspNetCore.Mvc;

namespace QuanLyThueDat.WebApp.Controllers
{
    [Route("ThongBaoTienThueDat")]
    public class ThongBaoTienThueDatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [Route("PopupDetailThongBaoTienThueDat")]
        public ViewResult _PopupDetailThongBaoTienThueDat()
        {
            return View();
        } 
        [Route("PopupDetailThongBaoTraTienMotLan")]
        public ViewResult _PopupDetailThongBaoTraTienMotLan()
        {
            return View();
        }
        [Route("PopupDetailThongBaoTienThueDatDieuChinh")]
        public ViewResult _PopupDetailThongBaoTienThueDatDieuChinh()
        {
            return View();
        }
    }
}
