
using Microsoft.AspNetCore.Mvc;
using QuanLyThueDat.Application.Interfaces;
using QuanLyThueDat.Application.Request;
using QuanLyThueDat.Application.ViewModel;

namespace QuanLyThueDat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThongBaoGhiThuGhiChiController : ControllerBase
    {
        private readonly IThongBaoGhiThuGhiChiService _ThongBaoGhiThuGhiChiService;

        public ThongBaoGhiThuGhiChiController(IThongBaoGhiThuGhiChiService ThongBaoGhiThuGhiChiService)
        {
            _ThongBaoGhiThuGhiChiService = ThongBaoGhiThuGhiChiService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(int? idDoanhNghiep)
        {
            var result = await _ThongBaoGhiThuGhiChiService.GetAll(idDoanhNghiep);
            return Ok(result);
        }
        [HttpPost("InsertUpdate")]
        public async Task<IActionResult> InsertUpdate(ThongBaoGhiThuGhiChiRequest req)
        {
            var result = await _ThongBaoGhiThuGhiChiService.InsertUpdate(req);
            return Ok(result);
        }
        [HttpGet("GetAllPaging")]
        public async Task<IActionResult> GetAllPaging(int? idDoanhNghiep, string keyword ="", int pageNumber=1, int pageSize=10)
        {
            var result = await _ThongBaoGhiThuGhiChiService.GetAllPaging(idDoanhNghiep, keyword, pageNumber, pageSize);
            return Ok(result);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int idThongBaoGhiThuGhiChi)
        {
            var result = await _ThongBaoGhiThuGhiChiService.Delete(idThongBaoGhiThuGhiChi);
            return Ok(result);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int idThongBaoGhiThuGhiChi)
        {
            var result = await _ThongBaoGhiThuGhiChiService.GetById(idThongBaoGhiThuGhiChi);
            return Ok(result);
        }        
    }
}
