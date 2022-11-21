
using Microsoft.AspNetCore.Mvc;
using QuanLyThueDat.Application.Interfaces;
using QuanLyThueDat.Application.Request;
using QuanLyThueDat.Application.ViewModel;

namespace QuanLyThueDat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuyetDinhMienTienThueDatController : ControllerBase
    {
        private readonly IQuyetDinhMienTienThueDatService _QuyetDinhMienTienThueDatService;

        public QuyetDinhMienTienThueDatController(IQuyetDinhMienTienThueDatService QuyetDinhMienTienThueDatService)
        {
            _QuyetDinhMienTienThueDatService = QuyetDinhMienTienThueDatService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(int? idDoanhNghiep)
        {
            var result = await _QuyetDinhMienTienThueDatService.GetAll(idDoanhNghiep);
            return Ok(result);
        }
        [HttpPost("InsertUpdate")]
        public async Task<IActionResult> InsertUpdate(QuyetDinhMienTienThueDatRequest req)
        {
            var result = await _QuyetDinhMienTienThueDatService.InsertUpdate(req);
            return Ok(result);
        }
        [HttpGet("GetAllPaging")]
        public async Task<IActionResult> GetAllPaging(int? idDoanhNghiep, string keyword="", int pageNumber=1, int pageSize=10)
        {
            var result = await _QuyetDinhMienTienThueDatService.GetAllPaging(idDoanhNghiep, keyword, pageNumber, pageSize);
            return Ok(result);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int idQuyetDinhMienTienThueDat)
        {
            var result = await _QuyetDinhMienTienThueDatService.Delete(idQuyetDinhMienTienThueDat);
            return Ok(result);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int idQuyetDinhMienTienThueDat)
        {
            var result = await _QuyetDinhMienTienThueDatService.GetById(idQuyetDinhMienTienThueDat);
            return Ok(result);
        }
        [HttpGet("CanhBaoQuyetDinhMienTienThueDatSapHetHan")]
        public async Task<IActionResult> CanhBaoQuyetDinhMienTienThueDatSapHetHan()
        {
            var result = await _QuyetDinhMienTienThueDatService.CanhBaoQuyetDinhMienTienThueDatSapHetHan();
            return Ok(result);
        }
    }
}
