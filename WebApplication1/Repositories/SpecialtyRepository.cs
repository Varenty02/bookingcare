using AutoMapper;
using bookingcare.Data;
using bookingcare.Models;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace bookingcare.Repositories
{
    public class SpecialtyRepository : ISpectialtyRepository
    {
        private readonly BookingCareContext _context;
        private readonly IMapper _mapper;

        public SpecialtyRepository(BookingCareContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SpecialtyModel> AddSpecialtyAsync(SpecialtyModel specialty)
        {
            var specialtyData = _mapper.Map<Specialty>(specialty);
            _context.Specialtys!.Add(specialtyData);
            await _context.SaveChangesAsync();


            return _mapper.Map<SpecialtyModel>(specialtyData);
        }

        public async Task DeleteSpecialtyAsync(int id)
        {
            var deleteSpecialty = _context.Specialtys!.SingleOrDefault(x => x.Id == id);
            if (deleteSpecialty != null)
            {
                _context.Specialtys!.Remove(deleteSpecialty);
                await _context.SaveChangesAsync();

            }
        }

        public async Task<List<SpecialtyModel>> GetAllSpecialtiesAsync()
        {
            if (_context.Specialtys == null)
            {
                return null;
            }
            var specialties = await _context.Specialtys.ToListAsync();
            return _mapper.Map<List<SpecialtyModel>>(specialties);
        }

        public async Task<SpecialtyModel> GetSpecialtyByIdAsync(int id)
        {
            if (_context.Specialtys == null)
            {
                return null;

            }
            var specialty = await _context.Specialtys.FindAsync(id);

            if (specialty == null)
            {
                return null;
            }

            return _mapper.Map<SpecialtyModel>(specialty);
        }

        public async Task UpdateSpecialtyAsync(int id, SpecialtyModel specialty)
        {
            var oldSpecialty = _context.Specialtys.SingleOrDefault(s => s.Id == id);
            if (oldSpecialty != null && specialty != null)
            {
                var updateData = _mapper.Map<Specialty>(specialty);
                _context.Specialtys.Update(updateData);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<SpecialtyModel> SpecialtyExists(int id)
        {
            return _mapper.Map<SpecialtyModel>((_context.Specialtys?.Any(e => e.Id == id)).GetValueOrDefault());
        }
    }
}
