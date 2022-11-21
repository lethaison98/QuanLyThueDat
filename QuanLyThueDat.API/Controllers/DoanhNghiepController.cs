
using Microsoft.AspNetCore.Mvc;
using QuanLyThueDat.Application.Interfaces;
using QuanLyThueDat.Application.Request;
using QuanLyThueDat.Application.ViewModel;

namespace QuanLyThueDat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoanhNghiepController : ControllerBase
    {
        private readonly IDoanhNghiepService _doanhNghiepService;

        public DoanhNghiepController(IDoanhNghiepService doanhNghiepService)
        {
            _doanhNghiepService = doanhNghiepService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _doanhNghiepService.GetAll();
            return Ok(result);
        }
        [HttpPost("InsertUpdate")]
        public async Task<IActionResult> InsertUpdate(DoanhNghiepRequest req)
        {
            var result = await _doanhNghiepService.InsertUpdate(req);
            return Ok(result);
        }
        [HttpGet("GetAllPaging")]
        public async Task<IActionResult> GetAllPaging(string keyword="", int pageNumber=1, int pageSize=10)
        {
            var result = await _doanhNghiepService.GetAllPaging(keyword, pageNumber, pageSize);
            return Ok(result);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int idDoanhNghiep)
        {
            var result = await _doanhNghiepService.Delete(idDoanhNghiep);
            return Ok(result);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int idDoanhNghiep)
        {
            var result = await _doanhNghiepService.GetById(idDoanhNghiep);
            return Ok(result);
        }

        [HttpPost("ImportDoanhNghiep")]
        public async Task<IActionResult> ImportDoanhNghiep([FromQuery] IList<IFormFile> files)
        {
            var result = await _doanhNghiepService.ImportDoanhNghiep(files);
            return Ok(result);
        }

    }
}
