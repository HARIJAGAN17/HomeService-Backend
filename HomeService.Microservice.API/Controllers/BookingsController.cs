using HomeService.Microservice.API.Contracts;
using HomeService.Microservice.API.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeService.Microservice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingRepo _bookingRepo;
        public BookingsController(IBookingRepo bookingRepo)
        {
            _bookingRepo = bookingRepo;
        }

        [HttpGet]
        [Authorize(Roles ="Provider,Customer")]
        public async Task<IActionResult> GetBookings()
        {
            return Ok(await _bookingRepo.GetALlBooking());
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> AddBooking([FromBody] AddBooking booking)
        {
            return Ok( await _bookingRepo.AddBooking(booking));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Provider,Customer")]
        public async Task<IActionResult> DeleteBooking([FromRoute] int id)
        {
           var isDeleted = await _bookingRepo.DeleteBooking(id);
            if (isDeleted == true)
            {
                return Ok(new { response = "deleted" });
            }
            return Ok(new { response = "notexist" });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Provider")]

        public async Task<IActionResult> UpdateBookingStatus([FromRoute]int id,[FromBody]UpdateStatusDto updateStatusDto)
        {
            var isUpdated = await _bookingRepo.UpdateBooking(id, updateStatusDto);
            if (isUpdated != null)
            {
                return Ok(new { response = "updated" });
            }
            return Ok(new { response = "notexist" });
        }

    }
}
