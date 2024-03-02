using bookingcare.Models;
using bookingcare.Models.MetaData;
using bookingcare.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace bookingcare.Controllers
{
    [Tags("Metadata")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MetaDataController : ControllerBase
    {
        private readonly IMetaDataRepository _metaDataRepository;

        public MetaDataController(IMetaDataRepository repo)
        {
            _metaDataRepository = repo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PositionModel>>> GetAllPositions()
        {
            try
            {
                return Ok(await _metaDataRepository.GetAllPositionsAsync());
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenderModel>>> GetAllGenders()
        {
            try
            {
                return Ok(await _metaDataRepository.GetAllGendersAsync());
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatusModel>>> GetAllStatuss()
        {
            try
            {
                return Ok(await _metaDataRepository.GetAllStatussAsync());
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TimeTypeModel>>> GetAllTimeTypes()
        {
            try
            {
                return Ok(await _metaDataRepository.GetAllTimeTypesAsync());
            }
            catch
            {
                return BadRequest();
            }

        }
    }
}
