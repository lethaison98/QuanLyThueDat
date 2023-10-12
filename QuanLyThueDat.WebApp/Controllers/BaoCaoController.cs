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
        public async Task<IActionResult> _ExportThongBaoTienThueDatHangNam(int namThongBao, string tuNgay, string denNgay)
        {
            var data = await _exportExcelClient.ExportThongBaoTienThueDatHangNam(namThongBao, tuNgay, denNgay);
            if (data.IsSuccess)
            {
                //var result = File(data.Data, "application/vnd.ms-word", loaiThongBao + ".doc");
                var result = File(data.Data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Báo cáo tiền thuê đất hàng năm.xlsx");
                return result;
            }
            return Ok(data);

        }        
        
        [Route("ExportThongBaoDonGiaThueDat")]
        public async Task<IActionResult> _ExportThongBaoDonGiaThueDat(string tuNgay, string denNgay)
        {
            var data = await _exportExcelClient.ExportThongBaoDonGiaThueDat(tuNgay, denNgay);
            if (data.IsSuccess)
            {
                //var result = File(data.Data, "application/vnd.ms-word", loaiThongBao + ".doc");
                var result = File(data.Data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Báo cáo đơn giá thuê đất.xlsx");
                return result;
            }
            return Ok(data);

        }
        [Route("ExportQuyetDinhMienTienThueDat")]
        public async Task<IActionResult> _ExportQuyetDinhMienTienThueDat(int? idQuyetDinhMienTienThueDat, string tuNgay, string denNgay)
        {
            var data = await _exportExcelClient.ExportQuyetDinhMienTienThueDat(idQuyetDinhMienTienThueDat, tuNgay, denNgay);
            if (data.IsSuccess)
            {
                var result = File(data.Data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Báo cáo miễn tiền thuê đất.xlsx");
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
                var result = File(data.Data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Báo cáo giao đất, cho thuê đất.xlsx");
                return result;
            }
            return Ok(data);

        }
        [Route("ExportBieuLapBo")]
        public async Task<IActionResult> _ExportBieuLapBo()
        {
            var data = await _exportExcelClient.ExportBieuLapBo();
            if (data.IsSuccess)
            {
                var result = File(data.Data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Biểu lập bộ.xlsx");
                return result;
            }
            return Ok(data);

        }
    }
}
