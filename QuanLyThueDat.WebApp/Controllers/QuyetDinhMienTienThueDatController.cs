using Microsoft.AspNetCore.Mvc;
using QuanLyThueDat.WebApp.Service;

namespace QuanLyThueDat.WebApp.Controllers
{
    [Route("QuyetDinhMienTienThueDat")]
    public class QuyetDinhMienTienThueDatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [Route("PopupDetailQuyetDinhMienTienThueDat")]
        public ViewResult _PopupDetailQuyetDinhMienTienThueDat()
        {
            return View();
        }

        [Route("PopupQuyetDinhMienTienThueDat")]
        public ViewResult _PopupQuyetDinhMienTienThueDat()
        {
            return View();
        }
        [Route("PopupViewQuyetDinhMienTienThueDat")]
        public ViewResult _PopupViewQuyetDinhMienTienThueDat()
        {
            return View();
        }
    }
}
