using QuanLyThueDat.Application.Request;
using QuanLyThueDat.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Application.Interfaces
{
    public  interface IUserService
    {
        public Task<ApiResult<UserLoginViewModel>> Authencate(LoginRequest request);
        public Task<ApiResult<bool>> InsertUpdate(UserRequest request);
        public Task<ApiResult<bool>> Register(RegisterRequest request);
        public Task<ApiResult<bool>> InsertRole(RoleRequest request);
        public Task<ApiResult<bool>> InsertRoleClaims(string roleId, List<Claim> listClaims);
        public Task<ApiResult<bool>> InsertUser_Role(string userId, List<string> listRoles);
        public Task<ApiResult<PageViewModel<UserViewModel>>> GetAllPaging(string keyword, int pageIndex, int pageSize);
        public Task<ApiResult<UserViewModel>> GetById(Guid idTaiKhoan);
        public Task<ApiResult<List<UserViewModel>>> GetDsChuyenVienPhuTrachKV();
        public Task<ApiResult<bool>> PhanQuyenChuyenVienPhuTrachKV(PhanQuyenCanBoRequest rq);

    }
}
