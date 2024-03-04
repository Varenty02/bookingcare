using bookingcare.Models;
using bookingcare.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace bookingcare.Controllers
{
    [Tags("DoctorInfos")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles ="Doctor")]
    [ValidateAntiForgeryToken]
    public class DoctorInfoController : ControllerBase
    {
        private readonly IDoctorInfoRepository _doctorInfoRepository;

        public DoctorInfoController(IDoctorInfoRepository repo)
        {
            _doctorInfoRepository = repo;
        }

        [HttpGet]
        [AllowAnonymous]
        
        public async Task<ActionResult<IEnumerable<DoctorInfoModel>>> GetAllDoctorInfo()
        {
            try
            {
                return Ok(await _doctorInfoRepository.GetAllDoctorInfos());
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<DoctorInfoModel>> GetDoctorInfoById(int id)
        {
            try
            {
                var specialty = await _doctorInfoRepository.GetDoctorInfo(id);
                return specialty == null ? NotFound() : Ok(specialty);
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpPost]
        public async Task<ActionResult<DoctorInfoCreateModel>> SaveDoctorInfo(DoctorInfoCreateModel doctorInfoModel)
        {
            try
            {
                var doctorId=this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var id = await _doctorInfoRepository.SaveDoctorInfo(doctorInfoModel, doctorId);
                return CreatedAtAction(nameof(GetDoctorInfoById), new { id }, doctorInfoModel);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
