﻿
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
        [HttpGet("BaoCaoTienThueDat")]
        public async Task<IActionResult> BaoCaoTienThueDat(int? nam, string tuNgay, string denNgay)
        {
            var result = await _baoCaoService.BaoCaoTienThueDat(nam, tuNgay, denNgay);
            return Ok(result);
        }
        [HttpGet("BaoCaoMienTienThueDat")]
        public async Task<IActionResult> BaoCaoMienTienThueDat(string tuNgay, string denNgay)
        {
            var result = await _baoCaoService.BaoCaoMienTienThueDat(tuNgay, denNgay);
            return Ok(result);
        } 
        [HttpGet("BaoCaoDonGiaThueDat")]
        public async Task<IActionResult> BaoCaoDonGiaThueDat(string tuNgay, string denNgay)
        {
            var result = await _baoCaoService.BaoCaoDonGiaThueDat(tuNgay, denNgay);
            return Ok(result);
        }
        [HttpGet("BaoCaoBieuLapBo")]
        public async Task<IActionResult> BaoCaoBieuLapBo()
        {
            var result = await _baoCaoService.BaoCaoBieuLapBo();
            return Ok(result);
        }
    }
}
