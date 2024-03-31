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
            var rooms = await this.roomService.GetAllRoom();
            return Ok(rooms);
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
            var rooms = await this.roomService.GetRoomByRoomTypeName(roomTypeName);
            return Ok(rooms);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetRoomByRoomTypeId([FromQuery] string roomTypeId)
        {
            var rooms = await this.roomService.GetRoomByRoomTypeId(roomTypeId);
            return Ok(rooms);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetRoom([FromRoute] string id)
        {
            var room = await this.roomService.GetRoomById(id);
            return Ok(room);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> AddRoom([FromBody] Room model)
        {
                var result = await this.roomService.CreateRoom(model);
                if (result) return Ok(new { succeeded = true, message = "Created" });
                return Ok(new { succeeded = false, message = "Dont found RoomType ID" });
        }

        
        [HttpPut("[action]")]
        public async Task<ActionResult> UpdateRoom([FromBody] Room model)
        {          
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var result = await this.roomService.UpdateRoom(model);
                if (result) return Ok(new { succeeded = true, message = "Updated" });
                return Ok(new { succeeded = false, message = "Failed to update Room! Dont found RoomType ID" });
           
        }


        [HttpDelete("[action]/{id}")]
        public async Task<ActionResult> DeleteRoom([FromRoute] string id)
        {
            var result = await this.roomService.DeleteRoom(id);
            return Ok(result);
        }

        [HttpDelete("[action]")]
        public async Task<ActionResult> DeleteAllRoomTypes()
        {
                var result = await this.roomService.DeleteAllRoom();
                if (result)
                {
                    return Ok(new { succeeded = true, message = "Deleted all records !" });
                }
                return Ok(new { succeeded = false, message = "Failed to delete All RoomTypes!" });           
        }
    }

}
