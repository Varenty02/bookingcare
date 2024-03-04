using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using bookingcare.Data;
using bookingcare.Repositories;
using bookingcare.Models;
using Microsoft.AspNetCore.Authorization;

namespace bookingcare.Controllers
{
    [Tags("Specialty")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class SpecialtiesController : ControllerBase
    {
        private readonly ISpectialtyRepository _spectialtyRepository;

        public SpecialtiesController(ISpectialtyRepository repo)
        {
            _spectialtyRepository = repo;
        }

        // GET: api/GetAllSpecialties
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<SpecialtyModel>>> GetAllSpecialties()
        {
            try
            {
                return Ok(await _spectialtyRepository.GetAllSpecialtiesAsync());
            }
            catch
            {
                return BadRequest();
            }

        }

        // GET: api/Specialties/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<SpecialtyModel>> GetSpecialtyById(int id)
        {
            try
            {
                var specialty = await _spectialtyRepository.GetSpecialtyByIdAsync(id);
                return specialty == null ? NotFound() : Ok(specialty);
            }
            catch
            {
                return BadRequest();
            }
        }

        // PUT: api/Specialties/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateSpecialty(int id, SpecialtyModel specialtyModel)
        {
            if (_spectialtyRepository.SpecialtyExists(id) == null)
            {
                return NotFound();
            }

            try
            {
                await _spectialtyRepository.UpdateSpecialtyAsync(id, specialtyModel);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }

        // POST: api/Specialties
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<SpecialtyModel>> AddSpecialty(SpecialtyModel specialtyModel)
        {
            try
            {
                var newSpecialty = await _spectialtyRepository.AddSpecialtyAsync(specialtyModel);
                return CreatedAtAction(nameof(GetSpecialtyById), new { id = newSpecialty.Id }, newSpecialty);
            }
            catch
            {
                return BadRequest();
            }
        }

        // DELETE: api/Specialties/5
        [HttpDelete("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSpecialty(int id)
        {
            if (_spectialtyRepository.SpecialtyExists(id) == null)
            {
                return NotFound();
            }
            try
            {
                await _spectialtyRepository.DeleteSpecialtyAsync(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
