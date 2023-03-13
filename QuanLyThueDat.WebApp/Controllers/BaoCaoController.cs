using Microsoft.AspNetCore.Mvc;
using QuanLyThueDat.WebApp.Service;

namespace QuanLyThueDat.WebApp.Controllers
{
    [Route("BaoCao")]
    public class BaoCaoController : Controller
    {
        private readonly IExportExcelClient _exportExcelClient;
        public BaoCaoController(IExportExcelClient exportExcelClient)
        {
            _exportExcelClient = exportExcelClient;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Route("ExportThongBaoTienThueDatHangNam")]
        public async Task<IActionResult> _ExportThongBaoTienThueDatHangNam(int namThongBao)
        {
            var data = await _exportExcelClient.ExportThongBaoTienThueDatHangNam(namThongBao);
            if (data.IsSuccess)
            {
                //var result = File(data.Data, "application/vnd.ms-word", loaiThongBao + ".doc");
                var result = File(data.Data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DanhSachThongBaoTienThueDat.xlsx");
                return result;
            }
            return Ok(data);

        }
        [Route("ExportQuyetDinhMienTienThueDat")]
        public async Task<IActionResult> _ExportQuyetDinhMienTienThueDat(int? idQuyetDinhMienTienThueDat)
        {
            var data = await _exportExcelClient.ExportQuyetDinhMienTienThueDat(idQuyetDinhMienTienThueDat);
            if (data.IsSuccess)
            {
                //var result = File(data.Data, "application/vnd.ms-word", loaiThongBao + ".doc");
                var result = File(data.Data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Bao_Cao_Mien_Tien_Thue_Dat.xlsx");
                return result;
            }
            return Ok(data);

        }
        [Route("ExportBaoCaoDoanhNghiepThueDat")]
        public async Task<IActionResult> _ExportBaoCaoDoanhNghiepThueDat()
        {
            var data = await _exportExcelClient.ExportBaoCaoDoanhNghiepThueDat();
            if (data.IsSuccess)
            {
                //var result = File(data.Data, "application/vnd.ms-word", loaiThongBao + ".doc");
                var result = File(data.Data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Bao_cao_doanh_nghiep_thue_dat.xlsx");
                return result;
            }
            return Ok(data);

        }
    }
}
