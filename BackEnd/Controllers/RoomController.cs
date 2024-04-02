using Azure;
using BackEnd.Models.Domains;
using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController : Controller
    {
        private readonly IRoomService roomService;

        public RoomController(IRoomService roomService)
        {
            this.roomService = roomService;
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllRoom()
        {
            var response = await this.roomService.GetAllRoom();
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetRoomIsValid([FromQuery] bool status)
        {

            var rooms = await this.roomService.GetRoomByIsAvailable(status);
            return Ok(rooms);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetRoomByRoomTypeName([FromQuery] string roomTypeName)
        {
            var response = await this.roomService.GetRoomByRoomTypeName(roomTypeName);
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetRoomByRoomTypeId([FromQuery] string roomTypeId)
        {
            var response = await this.roomService.GetRoomByRoomTypeId(roomTypeId);
            return Ok(response);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetRoom([FromRoute] string id)
        {
            var response = await this.roomService.GetRoomById(id);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> AddRoom([FromBody] Room model)
        {
            var response = await this.roomService.CreateRoom(model);
            return Ok(response);
        }

        
        [HttpPut("[action]")]
        public async Task<ActionResult> UpdateRoom([FromBody] Room model)
        {          
            var response = await this.roomService.UpdateRoom(model);
            return Ok(response);
        }


        [HttpDelete("[action]/{id}")]
        public async Task<ActionResult> DeleteRoom([FromRoute] string id)
        {
            var response = await this.roomService.DeleteRoom(id);
            return Ok(response);
        }

        [HttpDelete("[action]")]
        public async Task<ActionResult> DeleteAllRoomTypes()
        {
            var response = await this.roomService.DeleteAllRoom();
            return Ok(response);      
        }
    }

}
