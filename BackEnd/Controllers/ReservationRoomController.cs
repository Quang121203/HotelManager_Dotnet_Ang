using BackEnd.DataAccess;
using BackEnd.Models.Domains;
using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReservationRoomController : Controller
    {
        private readonly IReservationRoomService reservationRoomService;

        public ReservationRoomController(IReservationRoomService reservationRoomService)
        {
            this.reservationRoomService = reservationRoomService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllReservationRoom()
        {

            var response = await this.reservationRoomService.GetAllReservationRoom();
            return Ok(response);
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetReservationRoomByReservationID([FromQuery] string id)
        {
            var response = await this.reservationRoomService.GetAllReservationRoomByReservationID(id);
            return Ok(response);
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> AddReservationRoom([FromBody] ReservationRoom model)
        {
            var response = await this.reservationRoomService.CreateReservationRoom(model);
            return Ok(response);
        }



        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteAllReservationRoom()
        {
            var response = await this.reservationRoomService.DeleteAllReservationRoom();
            return Ok(response);

        }
    }
}
