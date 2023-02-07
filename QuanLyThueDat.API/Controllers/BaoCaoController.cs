
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyThueDat.Application.Interfaces;
using QuanLyThueDat.Application.Request;
using QuanLyThueDat.Application.ViewModel;

namespace QuanLyThueDat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaoCaoController : ControllerBase
    {
        private readonly IBaoCaoService _baoCaoService;

        public BaoCaoController(IBaoCaoService baoCaoService)
        {
            _baoCaoService = baoCaoService;
        }
        [HttpGet("BaoCaoDoanhNghiepThueDat")]
        public async Task<IActionResult> BaoCaoDoanhNghiepThueDat()
        {
            var result = await _baoCaoService.BaoCaoDoanhNghiepThueDat();
            return Ok(result);
        }
    }
}
