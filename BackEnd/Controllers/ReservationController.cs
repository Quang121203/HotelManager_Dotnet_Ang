using BackEnd.DataAccess;
using BackEnd.Models.Domains;
using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : Controller
    {
        private readonly IReservationService reservationService;

        public ReservationController(IReservationService reservationService)
        {
            this.reservationService = reservationService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllReservations()
        {
            var response = await this.reservationService.GetAllReservation();
            return Ok(response);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetReservation([FromRoute] string id)
        {
            var response = await this.reservationService.GetReservationByID(id);
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<Reservation>> GetReservationsByGuestID([FromQuery] string id)
        {
            var response = await this.reservationService.GetReservationByGuestID(id);
            return Ok(response);
        }


        // [FromRoute] always make bool = false !!!!, so switched to [FromQuery]
        // https://localhost:7232/api/Reservation/GetReservationsByWasConfirm?confirm=true
        [HttpGet("[action]")]
        public async Task<ActionResult<Reservation>> GetReservationsByWasConfirm([FromQuery] bool confirm)
        {
            var response = await this.reservationService.GetReservationByWasConfirm(confirm);
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<Reservation>> CheckIn([FromQuery] string IDReservation)
        {
            var response = await this.reservationService.CheckIn(IDReservation);
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<Reservation>> CheckOut([FromQuery] string IDReservation)
        {
            var response = await this.reservationService.CheckOut(IDReservation);
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<Reservation>> Cancel([FromQuery] string IDReservation)
        {
            var response = await this.reservationService.Cancel(IDReservation);
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<Reservation>> GetReservationByRoom([FromQuery] string IDRoom)
        {
            var reserVation = await this.reservationService.GetReservationByRoom(IDRoom);
            if (reserVation == null)
            {
                return NotFound();
            }
            return Ok(reserVation);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ReserveRooms([FromBody] ReservationVM reservationVM)
        {
            var response = await this.reservationService.ReserveRooms(reservationVM);
            return Ok(response);
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> AddReservation([FromBody] Reservation model)
        {
            var response = await this.reservationService.CreateReservation(model);           
            return Ok(response);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateReservationAsync([FromBody] Reservation model)
        {
            var response = await this.reservationService.UpdateReservation(model);
            return Ok(response);
        }


        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteReservation([FromRoute] string id)
        {
            var response = await this.reservationService.DeleteReservation(id);
            return Ok(response);
        }

    }
}
