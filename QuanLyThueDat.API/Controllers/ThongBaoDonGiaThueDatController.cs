
using Microsoft.AspNetCore.Mvc;
using QuanLyThueDat.Application.Interfaces;
using QuanLyThueDat.Application.Request;
using QuanLyThueDat.Application.ViewModel;

namespace QuanLyThueDat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThongBaoDonGiaThueDatController : ControllerBase
    {
        private readonly IThongBaoDonGiaThueDatService _ThongBaoDonGiaThueDatService;

        public ThongBaoDonGiaThueDatController(IThongBaoDonGiaThueDatService ThongBaoDonGiaThueDatService)
        {
            _ThongBaoDonGiaThueDatService = ThongBaoDonGiaThueDatService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _ThongBaoDonGiaThueDatService.GetAll();
            return Ok(result);
        }
        [HttpPost("InsertUpdate")]
        public async Task<IActionResult> InsertUpdate(ThongBaoDonGiaThueDatRequest req)
        {
            var result = await _ThongBaoDonGiaThueDatService.InsertUpdate(req);
            return Ok(result);
        }
        [HttpGet("GetAllPaging")]
        public async Task<IActionResult> GetAllPaging(int? idDoanhNghiep, string keyword ="", int pageNumber=1, int pageSize=10)
        {
            var result = await _ThongBaoDonGiaThueDatService.GetAllPaging(idDoanhNghiep, keyword, pageNumber, pageSize);
            return Ok(result);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int idThongBaoDonGiaThueDat)
        {
            var result = await _ThongBaoDonGiaThueDatService.Delete(idThongBaoDonGiaThueDat);
            return Ok(result);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int idThongBaoDonGiaThueDat)
        {
            var result = await _ThongBaoDonGiaThueDatService.GetById(idThongBaoDonGiaThueDat);
            return Ok(result);
        }


        [HttpPost("GetAllByRequest")]
        public async Task<IActionResult> GetAllByRequest(ThongBaoDonGiaThueDatRequest request)
        {
            var result = await _ThongBaoDonGiaThueDatService.GetAllByRequest(request);
            return Ok(result);
        }
        
        [HttpGet("CanhBaoThongBaoDonGiaThueDatSapHetHan")]
        public async Task<IActionResult> CanhBaoThongBaoDonGiaThueDatSapHetHan()
        {
            var result = await _ThongBaoDonGiaThueDatService.CanhBaoThongBaoDonGiaThueDatSapHetHan();
            return Ok(result);
        }
    }
}
