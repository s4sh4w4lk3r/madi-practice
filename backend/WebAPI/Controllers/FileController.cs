using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [ApiController, Route("filerepo")]
    public class FileController(FileService fileService) : ControllerBase
    {
        [HttpGet, Route("get")]
        public async Task<IActionResult> GetFileById(int id)
        {
            var result = await fileService.GetPathById(id);
            if (result.Success is false || result.Value is null)
            {
                return NotFound(result);
            }

            return File(result.Value.Path, result.Value.MimeType);
        }

        [HttpGet, Route("getall")]
        public async Task<IActionResult> GetFileList() => Ok(await fileService.GetFileList());
    }
}
