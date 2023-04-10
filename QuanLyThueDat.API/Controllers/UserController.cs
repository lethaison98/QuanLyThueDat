using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyThueDat.Application.Interfaces;
using QuanLyThueDat.Application.Request;
using System.Security.Claims;

namespace QuanLyThueDat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.Authencate(request);
            return Ok(result);
        }
        [HttpPost("InsertUpdate")]
        [AllowAnonymous]
        public async Task<IActionResult> InsertUpdate(UserRequest request)
        {
            var result = await _userService.InsertUpdate(request);
            return Ok(result);
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.Register(request);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        
        [HttpPost("InsertRole")]
        [AllowAnonymous]
        public async Task<IActionResult> InsertRole([FromBody] RoleRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.InsertRole(request);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("InsertRoleClaims")]
        [Authorize]
        public async Task<IActionResult> InsertRoleClaims(string roleId, List<Claim> listClaims)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.InsertRoleClaims(roleId, listClaims);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("GetAllPaging")]
        public async Task<IActionResult> GetAllPaging(string keyword = "", int pageNumber = 1, int pageSize = 10)
        {
            var result = await _userService.GetAllPaging(keyword, pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(string idTaiKhoan)
        {
            var result = await _userService.GetById(new Guid(idTaiKhoan));
            return Ok(result);
        }
        [HttpGet("GetDsChuyenVienPhuTrachKV")]
        public async Task<IActionResult> GetDsChuyenVienPhuTrachKV()
        {
            var result = await _userService.GetDsChuyenVienPhuTrachKV();
            return Ok(result);
        }
        [HttpGet("LayDanhSachQuyetDinhThueDatTheoChuyenVienPhuTrachKV")]
        public async Task<IActionResult> LayDanhSachQuyetDinhThueDatTheoChuyenVienPhuTrachKV(string idCanBo)
        {
            var result = await _userService.LayDanhSachQuyetDinhThueDatTheoChuyenVienPhuTrachKV(idCanBo);
            return Ok(result);
        }

        [HttpPost("PhanQuyenChuyenVienPhuTrachKV")]
        public async Task<IActionResult> PhanQuyenChuyenVienPhuTrachKV(PhanQuyenCanBoRequest rq)
        {
            var result = await _userService.PhanQuyenChuyenVienPhuTrachKV(rq);
            return Ok(result);
        }
        
        [HttpPost("ChangePassByUser")]
        public async Task<IActionResult> ChangePassByUser(ChangePasswordRequest rq)
        {
            var result = await _userService.ChangePassByUser(rq);
            return Ok(result);
        }

    }
}


