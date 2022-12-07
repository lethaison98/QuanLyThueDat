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
    public class FileService : IFileService
    {
        private readonly QuanLyThueDatDbContext _context;

        public FileService(QuanLyThueDatDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<int>> Insert(FileUploadRequest req)
        {
            var entity = new Files();
            var result = 0;
            if(req.IdDoanhNghiep != 0)
            {
                var doanhNghiep = await _context.DoanhNghiep.FirstOrDefaultAsync(x => x.IdDoanhNghiep == req.IdDoanhNghiep);
                if (doanhNghiep != null)
                {
                    var path = Path.Combine("TaiLieu", doanhNghiep.MaSoThue);
                    string filePath = await UploadFile(req.File, path);
                    if (!String.IsNullOrEmpty(filePath))
                    {
                        string fileName = req.File.FileName;
                        entity = new Files
                        {
                            LinkFile = filePath,
                            TenFile = fileName,
                            NgayTao = DateTime.Now
                        };
                    }
                    _context.Files.Update(entity);
                    await _context.SaveChangesAsync();
                    result = entity.IdFile;
                }
            }
            else
            {
                return new ApiErrorResult<int>("Không tồn tại doanh nghiệp");
            }
            return new ApiSuccessResult<int>() { Data = result };
        }
        public async static Task<string> UploadFile(IFormFile file, string path)
        {
            try
            {
                var folderName = "UploadFile";
                var pathToDB = Path.Combine(folderName, path);

                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                var fileNameOutput = fileName;
                var extension = Path.GetExtension(fileName).ToLower();

                if (!Directory.Exists(pathToDB))
                {
                    Directory.CreateDirectory(pathToDB);
                }

                var newFileName = DateTime.Now.TimeOfDay.TotalMilliseconds.ToString()+"_" + RemoveSpecialCharacters(CommonUtils.RemoveSign4VietnameseString(fileName)).Replace("-", "");
                var newFilePath = Path.Combine(pathToDB, newFileName);
                using (Stream fileStream = new FileStream(newFilePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                return Path.Combine(pathToDB, newFileName);

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        private static string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
        }
        public async Task<ApiResult<bool>> Delete(int idFile)
        {
            var result = false;
            var data = _context.Files.FirstOrDefault(x => x.IdFile == idFile);
            if (data != null)
            {
                _context.Files.Remove(data);
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
