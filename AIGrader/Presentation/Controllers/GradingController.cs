using Application.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradingController : ControllerBase
    {
        public IGraderService _graderService { get; set; }

        public GradingController(IGraderService graderService)
        {
            _graderService = graderService;
        }

        //[HttpPost("grading")]
        //public async Task<IActionResult> Grading(string assignment, IFormFile formFile)
        //{
        //    var result = await _graderService.Grade(assignment, formFile);
        //    return Ok(result);
        //}

        [HttpPost]
        public async Task<IActionResult> Grading(string assignment, List<IFormFile> formFiles)
        {
            var result = await _graderService.Grade(assignment, files: formFiles);
            return Ok(result);
        }

        [HttpPost("{problemId}")]
        [Authorize]
        public async Task<IActionResult> Grading(int problemId ,string assignment, List<IFormFile> formFiles)
        {
            var result = await _graderService.Grade(assignment, problemId ,files: formFiles);
            return Ok(result);
        }
    }
}
