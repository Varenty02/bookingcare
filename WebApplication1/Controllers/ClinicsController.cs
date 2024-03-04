using bookingcare.Data;
using bookingcare.Models;
using bookingcare.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bookingcare.Controllers
{
    [Tags("Clinics")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class ClinicsController : ControllerBase
    {
        private readonly IClinicsRepository _clinicsRepository;

        public ClinicsController(IClinicsRepository repo)
        {
            _clinicsRepository = repo;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ClinicModel>>> GetAllClinics()
        {
            try
            {
                return Ok(await _clinicsRepository.GetAllClinicsAsync());
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ClinicModel>> GetClinicById(int id)
        {
            try
            {
                var specialty = await _clinicsRepository.GetClinicByIdAsync(id);
                return specialty == null ? NotFound() : Ok(specialty);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateClinic(int id, ClinicModel clinicModel)
        {
            if (_clinicsRepository.ClinicsExists(id) == null)
            {
                return NotFound();
            }

            try
            {
                await _clinicsRepository.UpdateClinicAsync(id, clinicModel);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<ClinicModel>> AddClinic(ClinicModel clinicModel)
        {
            try
            {
                var newClinic = await _clinicsRepository.AddClinicAsync(clinicModel);
                return CreatedAtAction(nameof(GetClinicById), new { id = newClinic.Id }, newClinic);
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
            if (_clinicsRepository.ClinicsExists(id) == null)
            {
                return NotFound();
            }
            try
            {
                await _clinicsRepository.DeleteClinicAsync(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
