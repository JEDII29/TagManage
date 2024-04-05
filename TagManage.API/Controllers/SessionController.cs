using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TagManage.API.Requests;
using TagManage.Domain.Authentication;

namespace TagManage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController(IAuthenticationService authenticationService) : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService = authenticationService;

        [HttpGet("Login")]
        public async Task<IActionResult> Login([FromQuery] LoginRequest loginRequest)
        {
            try
            {
                if (_authenticationService.IsValidUser(loginRequest.Username, loginRequest.Password))
                {
                    var token = await _authenticationService.GenerateAccessToken(loginRequest.Username);
                    return Ok(token);
                }
                return Unauthorized();
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
