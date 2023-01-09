
using Microsoft.AspNetCore.Mvc;
using QuanLyThueDat.Application.Interfaces;
using QuanLyThueDat.Application.Request;
using QuanLyThueDat.Application.ViewModel;

namespace QuanLyThueDat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThongBaoTienSuDungDatController : ControllerBase
    {
        private readonly IThongBaoTienSuDungDatService _ThongBaoTienSuDungDatService;

        public ThongBaoTienSuDungDatController(IThongBaoTienSuDungDatService ThongBaoTienSuDungDatService)
        {
            _ThongBaoTienSuDungDatService = ThongBaoTienSuDungDatService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _ThongBaoTienSuDungDatService.GetAll();
            return Ok(result);
        }
        [HttpPost("InsertUpdate")]
        public async Task<IActionResult> InsertUpdate(ThongBaoTienSuDungDatRequest req)
        {
            var result = await _ThongBaoTienSuDungDatService.InsertUpdate(req);
            return Ok(result);
        }
        [HttpGet("GetAllPaging")]
        public async Task<IActionResult> GetAllPaging(int? idDoanhNghiep, string keyword ="", int pageNumber=1, int pageSize=10)
        {
            var result = await _ThongBaoTienSuDungDatService.GetAllPaging(idDoanhNghiep, keyword, pageNumber, pageSize);
            return Ok(result);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int idThongBaoTienSuDungDat)
        {
            var result = await _ThongBaoTienSuDungDatService.Delete(idThongBaoTienSuDungDat);
            return Ok(result);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int idThongBaoTienSuDungDat)
        {
            var result = await _ThongBaoTienSuDungDatService.GetById(idThongBaoTienSuDungDat);
            return Ok(result);
        }


        [HttpPost("GetAllByRequest")]
        public async Task<IActionResult> GetAllByRequest(ThongBaoTienSuDungDatRequest request)
        {
            var result = await _ThongBaoTienSuDungDatService.GetAllByRequest(request);
            return Ok(result);
        }
        
    }
}
