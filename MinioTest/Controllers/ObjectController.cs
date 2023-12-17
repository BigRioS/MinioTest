using Microsoft.AspNetCore.Mvc;
using MinioTest.Services.Minio;

namespace MinioTest.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ObjectController : ControllerBase
    {
        private readonly ILogger<ObjectController> _logger;
        private readonly MinioObject _minio;

        public ObjectController(ILogger<ObjectController> logger, MinioObject minio)
        {
            _logger = logger;
            _minio = minio;
        }
        [HttpGet]
        public async Task<ActionResult> Get(string token)
        {
            var result = await _minio.GetObject(token);
            return File(result.Bytes, result.ContentType);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] IFormFile file)
        {
            var result = await _minio.PutObject(file);
            return Ok(new { filename = result });
        }
    }
}