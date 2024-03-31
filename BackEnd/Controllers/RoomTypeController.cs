using BackEnd.Models.Domains;
using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
            RoomType roomType = await this.roomTypeService.GetRoomType(id);
            if(roomType == null) return NotFound();
            return Ok(roomType);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllRoomType()
        {
            List<RoomType> roomTypes = await this.roomTypeService.GetAllRoomType();
            return Ok(roomTypes);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateRoomType([FromBody] RoomType model)
        {
            bool result = await this.roomTypeService.CreateRoomType(model);
            if (result) return Ok();
            return BadRequest();
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateRoomType([FromBody] RoomType model)
        {
            bool result = await this.roomTypeService.UpdateRoomType(model);
            if (result) return Ok();
            return BadRequest();
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteRoomType([FromRoute] string id)
        {
            bool result = await this.roomTypeService.DeleteRoomType(id);
            if (result) return Ok();
            return BadRequest();
        }
    }
}
