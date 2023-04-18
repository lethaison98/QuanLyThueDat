
using Microsoft.AspNetCore.Mvc;
using QuanLyThueDat.Application.Interfaces;
using QuanLyThueDat.Application.Request;
using QuanLyThueDat.Application.ViewModel;

namespace QuanLyThueDat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThongBaoTienThueDatController : ControllerBase
    {
        private readonly IThongBaoTienThueDatService _ThongBaoTienThueDatService;

        public ThongBaoTienThueDatController(IThongBaoTienThueDatService ThongBaoTienThueDatService)
        {
            _ThongBaoTienThueDatService = ThongBaoTienThueDatService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _ThongBaoTienThueDatService.GetAll();
            return Ok(result);
        }
        [HttpPost("InsertUpdate")]
        public async Task<IActionResult> InsertUpdate(ThongBaoTienThueDatRequest req)
        {
            var result = await _ThongBaoTienThueDatService.InsertUpdate(req);
            return Ok(result);
        }
        [HttpGet("GetAllPaging")]
        public async Task<IActionResult> GetAllPaging(int? idDoanhNghiep, int? nam, string keyword ="", int pageNumber=1, int pageSize=10 )
        {
            var result = await _ThongBaoTienThueDatService.GetAllPaging(idDoanhNghiep, nam, keyword, pageNumber, pageSize);
            return Ok(result);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int idThongBaoTienThueDat)
        {
            var result = await _ThongBaoTienThueDatService.Delete(idThongBaoTienThueDat);
            return Ok(result);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int idThongBaoTienThueDat)
        {
            var result = await _ThongBaoTienThueDatService.GetById(idThongBaoTienThueDat);
            return Ok(result);
        }
        [HttpGet("GetAllByNam")]
        public async Task<IActionResult> GetAllByNam(int nam)
        {
            var result = await _ThongBaoTienThueDatService.GetAllByNam(nam);
            return Ok(result);
        }
        [HttpPost("GetAllByRequest")]
        public async Task<IActionResult> GetAllByRequest(ThongBaoTienThueDatRequest request)
        {
            var result = await _ThongBaoTienThueDatService.GetAllByRequest(request);
            return Ok(result);
        }
    }
}
