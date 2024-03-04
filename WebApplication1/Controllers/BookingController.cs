using bookingcare.Models;
using bookingcare.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace bookingcare.Controllers
{
    [Tags("Booking")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles ="User")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingController(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }
        [HttpGet]
        public async  Task<ActionResult<IEnumerable<BookingModel>>> GetAllBooking() 
        {
            try
            {
                return Ok(await _bookingRepository.GetAllBookings());
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingModel>> GetBookingOfUser(string userId)
        {
            try
            {
                var schedule = await _bookingRepository.GetBookingById(userId);
                return schedule == null ? NotFound() : Ok(schedule);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<IActionResult> RegisterBooking(BookingModel model)
        {
            try
            {
                var statusCode=await _bookingRepository.RegisterBooking(model);
                return StatusCode(statusCode);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("{scheduleId}")]
        public async Task<IActionResult> CancelBooking(int scheduleId)
        {
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var statusCode = await _bookingRepository.CancelBooking(userId,scheduleId);
                return StatusCode(statusCode);
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
