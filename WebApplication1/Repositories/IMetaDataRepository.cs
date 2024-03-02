using bookingcare.Models;
using bookingcare.Models.MetaData;

namespace bookingcare.Repositories
{
    public interface IMetaDataRepository
    {
        public Task<List<StatusModel>> GetAllStatussAsync();
        public Task<List<TimeTypeModel>> GetAllTimeTypesAsync();
        public Task<List<GenderModel>> GetAllGendersAsync();
        public Task<List<PositionModel>> GetAllPositionsAsync();
    }
}
