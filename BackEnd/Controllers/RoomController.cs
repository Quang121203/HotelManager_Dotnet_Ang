using Azure;
using BackEnd.DataAccess;
using BackEnd.Models.Domains;
using BackEnd.Services.Implements;
using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController : Controller
    {
        private readonly IRoomService roomService;
        private readonly IRoomTypeService roomTypeService;
 

        public RoomController(IRoomService roomService, IRoomTypeService roomTypeService)
        {
            this.roomService = roomService;
            this.roomTypeService = roomTypeService;
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
        public async Task<IActionResult> GetRoomNotReser([FromBody] ReservationVM model)
        {
            RoomType rt = await this.roomTypeService.GetRoomType(model.RoomTypeId);
            List<Room> rooms = await this.roomService.GetRoomNotReser(model.StartTime, model.EndTime, rt);
            return Ok(rooms);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddRoom([FromBody] Room model)
        {
            var response = await this.roomService.CreateRoom(model);
            return Ok(response);
        }

        
        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateRoom([FromBody] Room model)
        {          
            var response = await this.roomService.UpdateRoom(model);
            return Ok(response);
        }


        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteRoom([FromRoute] string id)
        {
            var response = await this.roomService.DeleteRoom(id);
            return Ok(response);
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteAllRoomTypes()
        {
            var response = await this.roomService.DeleteAllRoom();
            return Ok(response);      
        }
    }

}
