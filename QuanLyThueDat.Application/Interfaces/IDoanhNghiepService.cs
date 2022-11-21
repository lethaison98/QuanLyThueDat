using Microsoft.AspNetCore.Http;
using QuanLyThueDat.Application.Request;
using QuanLyThueDat.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Application.Interfaces
{
    public interface IDoanhNghiepService
    {
        public Task<ApiResult<int>> InsertUpdate(DoanhNghiepRequest rq);
        public Task<ApiResult<bool>> Delete(int idDoanhNghiep);
        public Task<ApiResult<DoanhNghiepViewModel>> GetById(int idDoanhNghiep);
        public Task<ApiResult<List<DoanhNghiepViewModel>>> GetAll();
        public Task<ApiResult<PageViewModel<DoanhNghiepViewModel>>> GetAllPaging(string keyword, int pageIndex, int pageSize);
        public Task<ApiResult<List<string>>> ImportDoanhNghiep(IList<IFormFile> files);
    }
}
