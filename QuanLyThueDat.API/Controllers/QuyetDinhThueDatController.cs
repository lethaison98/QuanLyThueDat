
using Microsoft.AspNetCore.Mvc;
using QuanLyThueDat.Application.Interfaces;
using QuanLyThueDat.Application.Request;
using QuanLyThueDat.Application.ViewModel;

namespace QuanLyThueDat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuyetDinhThueDatController : ControllerBase
    {
        private readonly IQuyetDinhThueDatService _QuyetDinhThueDatService;

        public QuyetDinhThueDatController(IQuyetDinhThueDatService QuyetDinhThueDatService)
        {
            _QuyetDinhThueDatService = QuyetDinhThueDatService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(int? idDoanhNghiep)
        {
            var result = await _QuyetDinhThueDatService.GetAll(idDoanhNghiep);
            return Ok(result);
        }
        [HttpGet("GetListQuyetDinhThueDatChiTiet")]
        public async Task<IActionResult> GetListQuyetDinhThueDatChiTiet(int idDoanhNghiep)
        {
            var result = await _QuyetDinhThueDatService.GetListQuyetDinhThueDatChiTiet(idDoanhNghiep);
            return Ok(result);
        }
        [HttpPost("InsertUpdate")]
        public async Task<IActionResult> InsertUpdate(QuyetDinhThueDatRequest req)
        {
            var result = await _QuyetDinhThueDatService.InsertUpdate(req);
            return Ok(result);
        }
        [HttpGet("GetAllPaging")]
        public async Task<IActionResult> GetAllPaging(int? idDoanhNghiep, string keyword="", int pageNumber=1, int pageSize=10)
        {
            var result = await _QuyetDinhThueDatService.GetAllPaging(idDoanhNghiep, keyword, pageNumber, pageSize);
            return Ok(result);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int idQuyetDinhThueDat)
        {
            var result = await _QuyetDinhThueDatService.Delete(idQuyetDinhThueDat);
            return Ok(result);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int idQuyetDinhThueDat)
        {
            var result = await _QuyetDinhThueDatService.GetById(idQuyetDinhThueDat);
            return Ok(result);
        }
    }
}
