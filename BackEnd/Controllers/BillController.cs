using BackEnd.DataAccess;
using BackEnd.Models.Domains;
using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class BillController : Controller
    {
       
        private readonly IBillService billService;

        public BillController(IBillService billService)
        {         
            this.billService = billService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllBill()
        {
            var response = await this.billService.GetAllBill();
            return Ok(response);
        }



        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetBillByGuestID([FromRoute] string id)
        {
            var response = await this.billService.GetBillByGuestID(id);
            return Ok(response);
        }



        [HttpPost("[action]")]
        public async Task<IActionResult> AddBill([FromBody] Bill model)
        {
            var response = await this.billService.CreateBill(model);
            return Ok(response);
        }

        [HttpPut("[action]")]
        public async Task<ActionResult> UpdateBill([FromBody] Bill model)
        {
            var response = await this.billService.UpdateBill(model);
            return Ok(response);

        }

    }
}
