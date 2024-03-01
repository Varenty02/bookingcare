using bookingcare.Models;

namespace bookingcare.Repositories
{
    public interface IClinicsRepository
    {
        public Task<List<ClinicModel>> GetAllClinicsAsync();
        public Task<ClinicModel> GetClinicByIdAsync(int id);
        public Task<ClinicModel> AddClinicAsync(ClinicModel clinic);
        public Task UpdateClinicAsync(int id, ClinicModel clinic);
        public Task DeleteClinicAsync(int id);
        public Task<ClinicModel> ClinicsExists(int id);
    }
}
