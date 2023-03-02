using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QuanLyThueDat.Application.Interfaces;
using QuanLyThueDat.Application.Request;
using QuanLyThueDat.Application.ViewModel;
using QuanLyThueDat.Data.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Application.Service
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _config;
        public IHttpContextAccessor _accessor { get; set; }
        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            RoleManager<AppRole> roleManager, IConfiguration config, IHttpContextAccessor HttpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
            _accessor = HttpContextAccessor;
        }

        public async Task<ApiResult<UserLoginViewModel>> Authencate(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null) return new ApiErrorResult<UserLoginViewModel>("Tài khoản không tồn tại"); ;

            var login = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
            if (!login.Succeeded)
            {
                return new ApiErrorResult<UserLoginViewModel>("Đăng nhập không thành công");
            }
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new[]
            {
                new Claim("UserName", user.UserName.ToString()),
                new Claim("UserId", user.Id.ToString()),
                //new Claim("ip", ipAddress),
                new Claim("HoTen", user.HoTen.ToString()),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);
            var result = new UserLoginViewModel();
            result.Token = new JwtSecurityTokenHandler().WriteToken(token);
            result.UserName = user.UserName;
            result.HoTen = user.HoTen;
            result.Roles = string.Join(';', roles);
            return new ApiSuccessResult<UserLoginViewModel>(result);
        }
        public async Task<ApiResult<bool>> Register(RegisterRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user != null)
            {
                return new ApiErrorResult<bool>("Tài khoản đã tồn tại");
            }
            if (await _userManager.FindByEmailAsync(request.Email) != null)
            {
                return new ApiErrorResult<bool>("Email đã tồn tại");
            }

            user = new AppUser()
            {
                NgaySinh = request.NgaySinh,
                Email = request.Email,
                HoTen = request.HoTen,
                DonVi = request.DonVi,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Đăng ký không thành công");
        }
        public async Task<ApiResult<bool>> InsertRole(RoleRequest rq)
        {
            var role = new AppRole()
            {
                Name = rq.Name,
                NormalizedName = rq.NormalizedName,
                MoTa = rq.MoTa,
            };
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Cập nhật không thành công");
        }
        public async Task<ApiResult<bool>> InsertUser_Role(string userId, List<string> listRoleId)
        {
            var claimsIdentity = _accessor.HttpContext.User.Identity as ClaimsIdentity;
            var user = await _userManager.FindByNameAsync(userId);
            var result = await _userManager.AddToRolesAsync(user, listRoleId);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Cập nhật không thành công");
        }
        public async Task<ApiResult<bool>> InsertRoleClaims(string roleId, List<Claim> listClaims)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(roleId);
                foreach (var claim in listClaims)
                {
                    await _roleManager.AddClaimAsync(role, claim);
                }

                return new ApiSuccessResult<bool>();
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<bool>("Cập nhật không thành công");
            }
        }
    }
}
