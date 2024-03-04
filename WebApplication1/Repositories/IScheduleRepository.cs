using bookingcare.Models;

namespace bookingcare.Repositories
{
    public interface IScheduleRepository
    {
        public  Task<int> CreateSchedule(ScheduleCreateModel model);
        public  Task<int> UpdateSchedule(int id, ScheduleModel model);
        public  Task<int> DeleteSchedule(int id);
        public  Task<List<ScheduleModel>> GetAllSchedules();
        public  Task<ScheduleModel> GetScheduleById(int id);
        public Task<bool> ScheduleExists(int id);
    }
}
