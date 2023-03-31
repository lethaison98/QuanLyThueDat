using Microsoft.AspNetCore.Mvc;

namespace QuanLyThueDat.WebApp.Controllers
{
    [Route("PhanQuyenCanBo")]
    public class PhanQuyenCanBoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [Route("PopupDetailPhanQuyenCanBo")]
        public ViewResult _PopupDetailPhanQuyenCanBo()
        {
            return View();
        }
    }
}
