using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TagManage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        [HttpGet]
        public IActionResult indeks()
        {
            return Ok();
        }
    }
}
