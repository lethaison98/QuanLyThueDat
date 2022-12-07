using Microsoft.AspNetCore.Http;
using QuanLyThueDat.Application.Request;
using QuanLyThueDat.Application.ViewModel;
using QuanLyThueDat.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Application.Interfaces
{
    public interface IFileTaiLieuService
    {
        public Task<ApiResult<int>> Insert(int idFile, int idTaiLieu, string loaiTaiLieu);
        public Task<ApiResult<bool>> Delete(int idFileTaiLieu);
        //public Task<ApiResult<QuyetDinhMienTienThueDatViewModel>> GetById(int idQuyetDinhMienTienThueDat);
        //public Task<ApiResult<List<QuyetDinhMienTienThueDatViewModel>>> GetAll(int? idDoanhNghiep);
        //public Task<ApiResult<PageViewModel<QuyetDinhMienTienThueDatViewModel>>> GetAllPaging(int? idDoanhNghiep,string keyword, int pageIndex, int pageSize);
    }
}
