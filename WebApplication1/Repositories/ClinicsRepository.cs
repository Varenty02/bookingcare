using AutoMapper;
using bookingcare.Data;
using bookingcare.Models;
using Microsoft.CodeAnalysis;
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

        public async Task DeleteClinicAsync(int id)
        {
            var deleteClinic = _context.Clinics!.SingleOrDefault(x => x.Id == id);
            if (deleteClinic != null)
            {
                _context.Clinics!.Remove(deleteClinic);
                await _context.SaveChangesAsync();

            }
        }

        public async Task<List<ClinicModel>?> GetAllClinicsAsync()
        {
            if (_context.Clinics == null)
            {
                return null;
            }
            var clinics = await _context.Clinics.ToListAsync();
            return _mapper.Map<List<ClinicModel>>(clinics);
        }

        public async Task<ClinicModel?> GetClinicByIdAsync(int id)
        {
            if (_context.Clinics == null)
            {
                return null;

            }
            var clinic = await _context.Clinics.FindAsync(id);

            if (clinic == null)
            {
                return null;
            }

            return _mapper.Map<ClinicModel>(clinic);
        }

        public async Task UpdateClinicAsync(int id, ClinicModel clinic)
        {
            var oldClinic = _context.Clinics!.SingleOrDefault(s => s.Id == id);
            if (oldClinic != null && clinic != null)
            {
                var updateData = _mapper.Map<Clinic>(clinic);
                _context.Clinics!.Update(updateData);
                await _context.SaveChangesAsync();
            }
        }
    }
}
