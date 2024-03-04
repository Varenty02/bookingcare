using bookingcare.Models;
using bookingcare.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Security.Claims;

namespace bookingcare.Controllers
{
    [Tags("Schedule")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Doctor")]
    
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleRepository _scheduleRepository;

        public ScheduleController(IScheduleRepository repo)
        {
            _scheduleRepository = repo;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ScheduleModel>>> GetAllSchedule()
        {
            try
            {
                return Ok(await _scheduleRepository.GetAllSchedules());
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ScheduleModel>> GetScheduleById(int id)
        {
            try
            {
                var schedule = await _scheduleRepository.GetScheduleById(id);
                return schedule == null ? NotFound() : Ok(schedule);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateSchedule(int id, ScheduleModel model)
        {
            if (!await _scheduleRepository.ScheduleExists(id) )
            {
                return NotFound();
            }

            try
            {
                var statuscode = await _scheduleRepository.UpdateSchedule(id, model);
                if (statuscode==204) ;
                    return Ok();
                return StatusCode(statuscode);
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSchedule(ScheduleCreateModel model)
        {
            try
            {
                model.CurrentNumber = 0;
                model.DoctorId=this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var newScheduleId = await _scheduleRepository.CreateSchedule(model);
                return CreatedAtAction(nameof(GetScheduleById), new { id = newScheduleId });
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteClinic(int id)
        {
            if (! await _scheduleRepository.ScheduleExists(id) )
            {
                return NotFound();
            }
            try
            {
                var statusCode=await _scheduleRepository.DeleteSchedule(id);
                if(statusCode==204)
                    return Ok();
                else return StatusCode(statusCode);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
