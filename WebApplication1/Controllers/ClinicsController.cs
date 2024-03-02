using bookingcare.Data;
using bookingcare.Models;
using bookingcare.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace bookingcare.Controllers
{
    public class ClinicsController : ControllerBase
    {
        private readonly IClinicsRepository _clinicsRepository;

        public ClinicsController(IClinicsRepository repo)
        {
            _clinicsRepository = repo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClinicModel>>> GetAllSpecialties()
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
        public async Task<ActionResult<ClinicModel>> AddSpecialty(ClinicModel clinicModel)
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
        public async Task<IActionResult> DeleteSpecialty(int id)
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
