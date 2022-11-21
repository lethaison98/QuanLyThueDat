using Microsoft.AspNetCore.Mvc;
using QuanLyThueDat.WebApp.Service;

namespace QuanLyThueDat.WebApp.Controllers
{
    [Route("DoanhNghiep")]
    public class DoanhNghiepController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [Route("PopupDetailDoanhnghiep")]
        public ViewResult _PopupDetailDoanhNghiep()
        {
            return View();
        }

        [Route("PopupThongBaoDoanhNghiep")]
        public ViewResult _PopupThongBaoDoanhNghiep()
        {
            return View();
        }

        [Route("PopupImportDoanhNghiep")]
        public ViewResult _PopupImportDoanhNghiep()
        {
            return View();
        }
    }
}
