using QuanLyThueDat.Application.Request;
using QuanLyThueDat.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Application.Interfaces
{
    public  interface IUserService
    {
        public Task<ApiResult<string>> Authencate(LoginRequest request);
        public Task<ApiResult<bool>> Register(RegisterRequest request);

    }
}
