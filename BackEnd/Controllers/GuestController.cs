using Azure;
using BackEnd.DataAccess;
using BackEnd.Models.Domains;
using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GuestController : Controller
    {
        private readonly IGuestService guestService;

        public GuestController(IGuestService guestService)
        {
            this.guestService = guestService;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<Guest>>> GetAllGuest()
        {

            var response = await guestService.GetAllGuests();
            return Ok(response);
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<Guest>> GetGuest([FromRoute] string id)
        {
            var response = await guestService.GetGuest(id);
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<Guest>> GetGuestByRoom([FromQuery] string RoomId)
        {
            var response = await this.guestService.GetGuestByRoom(RoomId);
            return Ok(response);
        }


        [HttpPost("[action]")]
        public async Task<ActionResult> AddGuest([FromBody] Guest model)
        {
            var response = await this.guestService.CreateGuest(model);
            return Ok(response);
        }

        [HttpPut("[action]")]
        public async Task<ActionResult> UpdateGuest([FromBody] Guest model)
        {
            var response = await this.guestService.UpdateGuest(model);
            return Ok(response);
        }


        [HttpDelete("[action]/{id}")]
        public async Task<ActionResult> DeleteGuest([FromRoute] string id)
        {
            var response = await this.guestService.DeleteGuest(id);
            return Ok(response);
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteAllGuests()
        {
            var response = await this.guestService.DeleteAllGuests();
            return Ok(response);
        }
    }
}
