using AutoMapper;
using bookingcare.Models;
using bookingcare.Models.MetaData;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace bookingcare.Repositories
{
    public class MetaDataRepository : IMetaDataRepository
    {
        private readonly BookingCareContext _context;
        private readonly IMapper _mapper;
        public MetaDataRepository(BookingCareContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<GenderModel>> GetAllGendersAsync()
        {
            if (_context.Genders == null)
            {
                return null;
            }
            var genders = await _context.Genders.ToListAsync();
            return _mapper.Map<List<GenderModel>>(genders);
        }

        public async Task<List<PositionModel>> GetAllPositionsAsync()
        {
            if (_context.Positions == null)
            {
                return null;
            }
            var positions = await _context.Positions.ToListAsync();
            return _mapper.Map<List<PositionModel>>(positions);
        }

        public async Task<List<StatusModel>> GetAllStatussAsync()
        {
            if (_context.Statuss == null)
            {
                return null;
            }
            var statuss = await _context.Statuss.ToListAsync();
            return _mapper.Map<List<StatusModel>>(statuss);
        }

        public async Task<List<TimeTypeModel>> GetAllTimeTypesAsync()
        {
            if (_context.TimeTypes == null)
            {
                return null;
            }
            var timeTypes = await _context.TimeTypes.ToListAsync();
            return _mapper.Map<List<TimeTypeModel>>(timeTypes);
        }
    }
}
