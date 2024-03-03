using AutoMapper;
using bookingcare.Migrations;
using bookingcare.Models;
using bookingcare.Models.MetaData;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace bookingcare.Repositories
{
    public class MetaDataRepository : IMetaDataRepository
    {
        private readonly BookingCareContext _context;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;
        public MetaDataRepository(BookingCareContext context, IMapper mapper, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _mapper = mapper;
            _roleManager = roleManager;
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
        public async Task<List<RoleModel>> GetAllRolesAsync()
        {
            if (_context.TimeTypes == null)
            {
                return null;
            }
            var roles = await _roleManager.Roles.OrderBy(r => r.Name).ToListAsync();
            var listRoleModel= new List<RoleModel>();
            foreach(var role in roles)
            {
                listRoleModel.Add(new RoleModel() { Name = role.Name,Id = role.Id});
            }
            return _mapper.Map<List<RoleModel>>(listRoleModel);
        }
    }
}
