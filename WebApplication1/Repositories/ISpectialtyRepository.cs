using bookingcare.Models;

namespace bookingcare.Repositories
{
    public interface ISpectialtyRepository
    {
        public Task<List<SpecialtyModel>> GetAllSpecialtiesAsync();
        public Task<SpecialtyModel> GetSpecialtyByIdAsync(int id);
        public Task<SpecialtyModel> AddSpecialtyAsync(SpecialtyModel specialty);
        public Task UpdateSpecialtyAsync(int id, SpecialtyModel specialty);
        public Task DeleteSpecialtyAsync(int id);
        public Task<SpecialtyModel> SpecialtyExists(int id);
    }
}
