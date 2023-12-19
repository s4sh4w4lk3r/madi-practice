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

            return File(result.Value.Path, result.Value.MimeType, fileDownloadName: result.Value.Filename);
        }

        [HttpGet, Route("getall")]
        public async Task<IActionResult> GetFileList() => Ok(await fileService.GetFileList());

        [HttpPost, Route("add")]
        public async Task<IActionResult> AddFile(IFormFile formFile, string dirToSave = "")
        {
            string mimeType = formFile.ContentType;
            string fileName = formFile.FileName;
            var stream = formFile.OpenReadStream();
            var result = await fileService.AddFile(stream, dirToSave, fileName, mimeType);
            if (result.Success is false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
