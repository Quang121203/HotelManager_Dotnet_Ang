using Azure;
using BackEnd.Models.Domains;
using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class RoomTypeController : ControllerBase
    {

        private readonly IRoomTypeService roomTypeService;

    

        public RoomTypeController(IRoomTypeService roomTypeService)
        {
            this.roomTypeService = roomTypeService;
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetRoomType([FromRoute]string id)
        {
            var response = await this.roomTypeService.GetRoomType(id);
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllRoomType()
        {
            var response = await this.roomTypeService.GetAllRoomType();
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateRoomType([FromBody] RoomType model)
        {
            var response = await this.roomTypeService.CreateRoomType(model);
            return Ok(response);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateRoomType([FromBody] RoomType model)
        {
            var response = await this.roomTypeService.UpdateRoomType(model);
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteRoomType([FromRoute] string id)
        {
            var response = await this.roomTypeService.DeleteRoomType(id);
            return Ok(response);
        }
    }
}
