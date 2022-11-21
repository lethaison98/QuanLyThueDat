
using Microsoft.AspNetCore.Mvc;
using QuanLyThueDat.Application.Interfaces;
using QuanLyThueDat.Application.Request;
using QuanLyThueDat.Application.ViewModel;

namespace QuanLyThueDat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HopDongThueDatController : ControllerBase
    {
        private readonly IHopDongThueDatService _HopDongThueDatService;

        public HopDongThueDatController(IHopDongThueDatService HopDongThueDatService)
        {
            _HopDongThueDatService = HopDongThueDatService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _HopDongThueDatService.GetAll();
            return Ok(result);
        }

        [HttpPost("InsertUpdate")]
        public async Task<IActionResult> InsertUpdate(HopDongThueDatRequest req)
        {
            var result = await _HopDongThueDatService.InsertUpdate(req);
            return Ok(result);
        }

        [HttpGet("GetAllPaging")]
        public async Task<IActionResult> GetAllPaging(int? idDoanhNghiep, string keyword="", int pageNumber=1, int pageSize=10)
        {
            var result = await _HopDongThueDatService.GetAllPaging(idDoanhNghiep, keyword, pageNumber, pageSize);
            return Ok(result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int idHopDongThueDat)
        {
            var result = await _HopDongThueDatService.Delete(idHopDongThueDat);
            return Ok(result);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int idHopDongThueDat)
        {
            var result = await _HopDongThueDatService.GetById(idHopDongThueDat);
            return Ok(result);
        }
    }
}
