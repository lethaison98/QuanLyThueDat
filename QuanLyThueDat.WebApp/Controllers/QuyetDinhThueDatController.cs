using Microsoft.AspNetCore.Mvc;
using QuanLyThueDat.WebApp.Service;

namespace QuanLyThueDat.WebApp.Controllers
{
    [Route("QuyetDinhThueDat")]
    public class QuyetDinhThueDatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [Route("PopupViewQuyetDinhThueDat")]
        public ViewResult _PopupViewQuyetDinhThueDat()
        {
            return View();
        }
        [Route("PopupDetailQuyetDinhThueDat")]
        public ViewResult _PopupDetailQuyetDinhThueDat()
        {
            return View();
        }

        [Route("PopupQuyetDinhThueDat")]
        public ViewResult _PopupQuyetDinhThueDat()
        {
            return View();
        }

        [Route("PopupDetailHopDongThueLaiDat")]
        public ViewResult _PopupDetailHopDongThueLaiDat()
        {
            return View();
        }
    }
}
