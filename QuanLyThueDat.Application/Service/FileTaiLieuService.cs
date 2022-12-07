using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using QuanLyThueDat.Application.Common;
using QuanLyThueDat.Application.Interfaces;
using QuanLyThueDat.Application.Request;
using QuanLyThueDat.Application.ViewModel;
using QuanLyThueDat.Data.EF;
using QuanLyThueDat.Data.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QuanLyThueDat.Application.Service
{
    public class FileTaiLieuService : IFileTaiLieuService
    {
        private readonly QuanLyThueDatDbContext _context;
        private readonly IFileService _fileService;
        private readonly IDoanhNghiepService _doanhNghiepService;

        public FileTaiLieuService(QuanLyThueDatDbContext context, IFileService fileService, IDoanhNghiepService doanhNghiepService)
        {
            _context = context;
            _fileService = fileService;
            _doanhNghiepService = doanhNghiepService;
        }

        public async Task<ApiResult<int>> Insert(int idFile, int idTaiLieu, string loaiTaiLieu)
        {
            var result = 0;
            var entity = new FileTaiLieu();
            entity = new FileTaiLieu
            {
                IdFile = idFile,
                IdTaiLieu = idTaiLieu,
                LoaiTaiLieu = loaiTaiLieu,
                NgayTao = DateTime.Now
            };
            _context.FileTaiLieu.Update(entity);
            await _context.SaveChangesAsync();
            result = entity.IdFileTaiLieu;
            return new ApiSuccessResult<int>() { Data = result };
        }

        public async Task<ApiResult<bool>> Delete(int idFileTaiLieu)
        {
            var result = false;
            var data = _context.FileTaiLieu.Include(x => x.File).FirstOrDefault(x => x.IdFile == idFileTaiLieu);
            if (data != null)
            {
                _context.FileTaiLieu.Remove(data);
                await _context.SaveChangesAsync();
                result = true;
                return new ApiSuccessResult<bool>() { Data = result };
            }
            else
            {
                result = false;
                return new ApiErrorResult<bool>() { Data = result };
            }

        }
    }
}
