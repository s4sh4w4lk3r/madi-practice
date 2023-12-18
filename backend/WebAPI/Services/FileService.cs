using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Models;

namespace WebAPI.Services
{
    public class FileService(MadiContext madiContext)
    {
        public async Task<List<FileRepo>> GetFileList()
        {
            return await madiContext.FileRepos.ToListAsync();
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
    }
}
