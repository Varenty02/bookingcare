using AutoMapper;
using bookingcare.Data;
using bookingcare.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace bookingcare.Repositories
{
    public class ClinicsRepository : IClinicsRepository
    {
        private readonly BookingCareContext _context;
        private readonly IMapper _mapper;
        public ClinicsRepository(BookingCareContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ClinicModel> AddClinicAsync(ClinicModel clinic)
        {
            var clinicData = _mapper.Map<Clinic>(clinic);
            _context.Clinics!.Add(clinicData);
            await _context.SaveChangesAsync();


            return _mapper.Map<ClinicModel>(clinicData);
        }

        public async Task<ClinicModel> ClinicsExists(int id)
        {
            return _mapper.Map<ClinicModel>((_context.Clinics?.Any(c => c.Id == id)).GetValueOrDefault());
        }

        public Task DeleteClinicAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ClinicModel>> GetAllClinicsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ClinicModel> GetClinicByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateClinicAsync(int id, ClinicModel clinic)
        {
            throw new NotImplementedException();
        }
    }
}
