using bookingcare.Models;
using Microsoft.AspNetCore.Mvc;

namespace bookingcare.Repositories
{
    public interface IDoctorInfoRepository
    {
        public  Task<int> SaveDoctorInfo(DoctorInfoCreateModel doctorInfoModel,string doctorId);
        public  Task<DoctorInfoModel> GetDoctorInfo(int id);
        public  Task<ActionResult<IEnumerable<DoctorInfoModel>>> GetAllDoctorInfos();
    }
}
