using BackEnd.Models.DTOS;
using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginVM model)
        {
            var response = await this.authService.Login(model);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RefeshToken([FromBody] string refeshToken)
        {
            string authorizationHeader = HttpContext.Request.Headers["Authorization"];
            string accessToken = authorizationHeader.Substring("Bearer ".Length);
            var token = await this.authService.RefeshToken(accessToken, refeshToken);
            return Ok(token);

        }
    }
}
