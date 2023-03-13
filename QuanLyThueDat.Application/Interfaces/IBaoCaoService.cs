using Microsoft.AspNetCore.Http;
using QuanLyThueDat.Application.ViewModel;
using QuanLyThueDat.Application.ViewModel.BaoCaoViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueDat.Application.Interfaces
{
    public interface IBaoCaoService
    {  
        public Task<ApiResult<List<BaoCaoDoanhNghiepThueDatViewModel>>> BaoCaoDoanhNghiepThueDat();
        public Task<ApiResult<List<ThongBaoTienThueDatViewModel>>> BaoCaoTienThueDat(int? nam);
    }
}
