﻿using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Models;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace WebAPI.Services
{
    public class FileService(MadiContext madiContext)
    {
        public async Task<List<FileRepo>> GetFileList()
        {
            var list = await madiContext.FileRepos.ToListAsync();
            foreach (var item in list)
            {
                item.Path = item.Path.Replace("Files\\", "/");
                item.Path = item.Path.Replace("\\", "/");
            }
            return list;
        }

        public async Task<ServiceResult<FileRepo?>> GetPathById(int id)
        {
            var fileRepo = await madiContext.FileRepos.Where(e => e.Id == id).SingleOrDefaultAsync();
            if (fileRepo == null)
            {
                return ServiceResult<FileRepo?>.Fail("Нет такого файла.", null);
            }

            return ServiceResult<FileRepo?>.Ok("О, есть файл.", fileRepo);
        }

        public async Task<ServiceResult> AddFile(Stream stream, string dirToSave, string filename, string mimeType, bool overwrite = false)
        {
            Directory.CreateDirectory(Path.Combine("wwwroot", "Files", dirToSave));
            string path = Path.Combine("Files", dirToSave, filename);
            string fullPath = Path.Combine("wwwroot", "Files", dirToSave, filename);
            if ((File.Exists(fullPath) is true) && (overwrite is false))
            {
                return ServiceResult.Fail("Файл уже существует, если вы намерены его перезаписать, то используйте другой способ.");
            }
            FileStream fileStream = File.Create(fullPath);
            await stream.CopyToAsync(fileStream);

            var fileRepo = new FileRepo()
            {
                Filename = filename,
                MimeType = mimeType,
                Path = path
            };
            await madiContext.FileRepos.AddAsync(fileRepo);
            await madiContext.SaveChangesAsync();

            fileStream.Dispose();
            stream.Dispose();

            return ServiceResult.Ok("Файл сохранен.");
        }

        public async Task<ServiceResult> DeleteFileById(int id)
        {
            var fileToDel = await madiContext.FileRepos.SingleOrDefaultAsync(e => e.Id == id);
            if (fileToDel is null) 
            {
                return ServiceResult.Fail("Нет такого файла.");
            }

            string finalPath = Path.Combine("wwwroot", fileToDel.Path);
            if (File.Exists(finalPath) is false)
            {
                return ServiceResult.Fail("Несоотв. базы данных с каталогом файлов");
            }

            File.Delete(finalPath);
            madiContext.FileRepos.Remove(fileToDel);
            await madiContext.SaveChangesAsync();
            return ServiceResult.Ok("Файл удален.");

        }
    }
}
