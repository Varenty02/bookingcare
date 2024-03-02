using AutoMapper;
using bookingcare.Data;
using bookingcare.Models;
using bookingcare.Models.MetaData;

namespace bookingcare.Helpers
{
    public class AppAutomapper:Profile
    {
        public AppAutomapper()
        {
            CreateMap<Specialty, SpecialtyModel>().ReverseMap();
            CreateMap<Clinic, ClinicModel>().ReverseMap();
            CreateMap<Status, StatusModel>().ReverseMap();
            CreateMap<Position, PositionModel>().ReverseMap();
            CreateMap<TimeType, TimeTypeModel>().ReverseMap();
            CreateMap<Gender, GenderModel>().ReverseMap();
        }

    }
}
