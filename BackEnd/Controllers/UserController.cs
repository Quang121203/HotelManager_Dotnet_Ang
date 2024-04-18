using BackEnd.Models.DTOS;
using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetUser([FromRoute] string id)
        {
            var response = await this.userService.GetUser(id);
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllUser()
        {
            var response = await this.userService.GetAllUser();
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateUser([FromBody] UserVM model)
        {
            var response = await this.userService.CreateUser(model);
            return Ok(response);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateUser([FromBody] UserVM model)
        {
            var response = await this.userService.UpdateUser(model);
            return Ok(response);
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] string id)
        {
            var response = await this.userService.DeleteUser(id);
            return Ok(response);
        }
    }
}
