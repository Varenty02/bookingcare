using AutoMapper;
using bookingcare.Data;
using bookingcare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace bookingcare.Repositories
{
    public class DoctorInfoRepository:IDoctorInfoRepository
    {
        private readonly BookingCareContext _context;
        private readonly IMapper _mapper;
        public DoctorInfoRepository(BookingCareContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> SaveDoctorInfo(DoctorInfoCreateModel doctorInfoModel, string doctorId)
        {
            var doctorInfo = new DoctorInfo()
            {
                Price = doctorInfoModel.Price,
                Province = doctorInfoModel.Province,
                Description = doctorInfoModel.Description,
                YearOfEx = doctorInfoModel.YearOfEx ,
                DoctorId = doctorId
            };
            _context.DoctorInfos!.Add(doctorInfo);
            await _context.SaveChangesAsync();
            return doctorInfo.Id;
        }
        public async Task<DoctorInfoModel> GetDoctorInfo(int id)
        {
            var doctorInfo = await _context.DoctorInfos!.FindAsync(id);


            return _mapper.Map<DoctorInfoModel>(doctorInfo);
        }
        public async Task<ActionResult<IEnumerable<DoctorInfoModel>>> GetAllDoctorInfos()
        {
            var listDoctorInfos= await _context.DoctorInfos!.ToListAsync();

            return _mapper.Map<List<DoctorInfoModel>>(listDoctorInfos);
        }
    }
}
