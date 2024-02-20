using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QuanLyThueDat.Application.Interfaces;
using QuanLyThueDat.Application.Request;
using QuanLyThueDat.Application.ViewModel;
using QuanLyThueDat.Data.EF;
using QuanLyThueDat.Data.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Application.Service
{
    public class UserService : IUserService
    {
        private readonly QuanLyThueDatDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _config;
        public IHttpContextAccessor _accessor { get; set; }
        public UserService(QuanLyThueDatDbContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            RoleManager<AppRole> roleManager, IConfiguration config, IHttpContextAccessor HttpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
            _accessor = HttpContextAccessor;
        }
        public async Task<ApiResult<bool>> InsertUpdate(UserRequest request)
        {
            if(request.UserId == new Guid())
            {
                var usercheck = _userManager.FindByNameAsync(request.UserName).Result;
                if (usercheck != null)
                {
                    return new ApiErrorResult<bool>("Tài khoản đã tồn tại");
                }
                if (!String.IsNullOrEmpty(request.Email) && await _userManager.FindByEmailAsync(request.Email) != null)
                {
                    return new ApiErrorResult<bool>("Email đã tồn tại");
                }
                var user = new AppUser()
                {
                    Email = request.Email,
                    HoTen = request.HoTen,
                    NgaySinh = DateTime.Now,
                    UserName = request.UserName,
                    PhoneNumber = request.PhoneNumber
                };
                var result = await _userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    var u = await _userManager.FindByNameAsync(request.UserName);
                    await _userManager.AddToRolesAsync(u, request.DsRole);
                    return new ApiSuccessResult<bool>();
                }
            }
            else
            {
                var users = await _userManager.FindByIdAsync(request.UserId.ToString());
                if(users.UserName != request.UserName)
                {
                    var usercheck = _userManager.FindByNameAsync(request.UserName).Result;
                    if (usercheck != null)
                    {
                        return new ApiErrorResult<bool>("Tài khoản đã tồn tại");
                    }
                }
                if(!String.IsNullOrEmpty(request.Email) && users.Email != request.Email)
                {
                    var usercheck = await _userManager.FindByEmailAsync(request.Email);
                    if (usercheck != null)
                    {
                        return new ApiErrorResult<bool>("Email đã tồn tại");
                    }
                }

                users.Email = request.Email;
                users.HoTen = request.HoTen;
                users.NgaySinh = DateTime.Now;
                users.UserName = request.UserName;
                users.PhoneNumber = request.PhoneNumber;
                var result = await _userManager.UpdateAsync(users);
                if (result.Succeeded)
                {
                    var currentRoles = await _userManager.GetRolesAsync(users);
                    var u = await _userManager.FindByNameAsync(request.UserName);
                    await _userManager.RemoveFromRolesAsync(users, currentRoles);
                    await _userManager.AddToRolesAsync(u, request.DsRole);
                    return new ApiSuccessResult<bool>();
                }
            }

            return new ApiErrorResult<bool>("Đăng ký không thành công");
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
        public async Task<ApiResult<PageViewModel<UserViewModel>>> GetAllPaging(string keyword, int pageIndex, int pageSize)
        {
            var query = from a in _context.AppUser
                        select a ;
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => (x.HoTen.ToLower().Contains(keyword.ToLower())) || (x.UserName.ToLower().Contains(keyword.ToLower())));
            }

            var data = query.OrderByDescending(x => x.UserName)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToList();
            var listItem = new List<UserViewModel>();
            foreach (var entity in data)
            {
                var user = new UserViewModel
                {
                    UserId = entity.Id,
                    HoTen = entity.HoTen,   
                    DonVi = entity.DonVi,
                    UserName = entity.UserName, 
                    PhoneNumber = entity.PhoneNumber,
                    Email = entity.Email,
                };
                var listRole = await _userManager.GetRolesAsync(entity);
                user.DsRole = listRole.ToList();          
                listItem.Add(user);
            }
            var result = new PageViewModel<UserViewModel>()
            {
                Items = listItem,
                PageIndex = pageIndex,
                TotalRecord = query.Count(),
                PageSize = pageSize
            };
            return new ApiSuccessResult<PageViewModel<UserViewModel>>() { Data = result };
        }
        public async Task<ApiResult<UserViewModel>> GetById(Guid idTaiKhoan)
        {
            var result = new UserViewModel();
            var entity = _context.AppUser.FirstOrDefault(x => x.Id == idTaiKhoan);
            if (entity != null)
            {
                result = new UserViewModel
                {
                    UserId = entity.Id,
                    HoTen = entity.HoTen,
                    DonVi = entity.DonVi,
                    UserName = entity.UserName,
                    PhoneNumber = entity.PhoneNumber,
                    Email = entity.Email,
                };
                var listRole = await _userManager.GetRolesAsync(entity);
                result.DsRole = listRole.ToList();
                return new ApiSuccessResult<UserViewModel>() { Data = result };

            }
            else
            {
                return new ApiErrorResult<UserViewModel>("Không tìm thấy dữ liệu");
            }
        }
        public async Task<ApiResult<List<UserViewModel>>> GetDsChuyenVienPhuTrachKV()
        {
            var result = new List<UserViewModel>();
            var listQH = (from user in _context.AppUser
                          join userRole in _context.UserRoles on user.Id equals userRole.UserId
                          join role in _context.AppRole on userRole.RoleId equals role.Id
                          where role.Name == "CHUYENVIENPHUTRACHKV"
                          select new UserViewModel
                          {
                              UserId = user.Id,
                              UserName = user.UserName,
                              HoTen = user.HoTen,
                          }).Distinct().ToList();
            result = listQH;
            return new ApiSuccessResult<List<UserViewModel>>() { Data = result };
        }
        public async Task<ApiResult<bool>> PhanQuyenChuyenVienPhuTrachKV(PhanQuyenCanBoRequest rq)
        {
            try
            {
                foreach (var id in rq.IdCanBo)
                {
                    var canBo = await _userManager.FindByIdAsync(id);
                    if (canBo != null)
                    {
                        var listOld = _context.CanBo_QuyetDinhThueDat.Where(x => x.IdCanBoQuanLy == id);
                        _context.CanBo_QuyetDinhThueDat.RemoveRange(listOld);
                        var listNew = new List<CanBo_QuyetDinhThueDat>();
                        foreach (var qd in rq.IdQuyetDinhThueDat)
                        {
                            var cb_qd = new CanBo_QuyetDinhThueDat
                            {
                                IdCanBoQuanLy = id,
                                IdQuyetDinhThueDat = qd,
                                CanBo = canBo.HoTen
                            };
                            listNew.Add(cb_qd);
                        }
                        _context.CanBo_QuyetDinhThueDat.AddRange(listNew);
                        await _context.SaveChangesAsync();
                    }
                }
                return new ApiSuccessResult<bool>();
            }catch (Exception ex)
            {
                return new ApiErrorResult<bool>("Cập nhật không thành công");

            }

        }
        public async Task<ApiResult<List<QuyetDinhThueDatViewModel>>> LayDanhSachQuyetDinhThueDatTheoChuyenVienPhuTrachKV(string idCanBo)
        {
            try
            {
                var result = new List<QuyetDinhThueDatViewModel>();
                var user = await _userManager.FindByIdAsync(idCanBo);
                if (user != null)
                {
                    result = (from cb in _context.AppUser
                                  join cb_qd in _context.CanBo_QuyetDinhThueDat on cb.Id.ToString() equals cb_qd.IdCanBoQuanLy
                                  join qd in _context.QuyetDinhThueDat on cb_qd.IdQuyetDinhThueDat equals qd.IdQuyetDinhThueDat
                                  where cb.Id.ToString() == idCanBo
                                  select new QuyetDinhThueDatViewModel
                                  {
                                      IdQuyetDinhThueDat = qd.IdQuyetDinhThueDat,
                                      TenDoanhNghiep = qd.DoanhNghiep.TenDoanhNghiep,
                                      SoQuyetDinhThueDat = qd.SoQuyetDinhThueDat,
                                      NgayQuyetDinhThueDat = qd.NgayQuyetDinhThueDat != null ? qd.NgayQuyetDinhThueDat.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                                  }).Distinct().ToList();
                }
                return new ApiSuccessResult<List<QuyetDinhThueDatViewModel>>() { Data = result };
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<List<QuyetDinhThueDatViewModel>>(ex.Message);

            }

        }
        public async Task<ApiResult<bool>> ChangePassByUser(ChangePasswordRequest request)
        {
            try
            {
                var claimsIdentity = _accessor.HttpContext.User.Identity as ClaimsIdentity;
                var userId = claimsIdentity.FindFirst("UserId")?.Value;
                var user = await _userManager.FindByIdAsync(userId);
                var x = new object();
                if (request.NewPassword != null && request.OldPassword != null)
                {
                    var changePasswordResult = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
                    if (changePasswordResult.Succeeded)
                    {
                        await _userManager.UpdateAsync(user);
                        return new ApiSuccessResult<bool>() { };
                    }
                    else
                    {
                        return new ApiErrorResult<bool>(changePasswordResult.Errors.First().Description) { };
                    }
                }
                return new ApiErrorResult<bool>();
            }catch (Exception ex)
            {
                return new ApiErrorResult<bool>(ex.Message);

            }

        }
        public async Task<ApiResult<bool>> ResetPassword(ChangePasswordRequest request)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(request.OldPassword);
                var x = new object();
                if (request.NewPassword != null && request.OldPassword != null)
                {
                    var removePass = await _userManager.RemovePasswordAsync(user);
                    var changePasswordResult = await _userManager.AddPasswordAsync(user, request.NewPassword);
                    if (changePasswordResult.Succeeded)
                    {
                        await _userManager.UpdateAsync(user);
                        return new ApiSuccessResult<bool>() { };
                    }
                    else
                    {
                        return new ApiErrorResult<bool>(changePasswordResult.Errors.First().Description) { };
                    }
                }
                return new ApiErrorResult<bool>();
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<bool>(ex.Message);

            }
        }
    }
}
