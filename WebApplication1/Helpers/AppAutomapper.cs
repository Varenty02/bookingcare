using AutoMapper;
using bookingcare.Data;
using bookingcare.Models;

namespace bookingcare.Helpers
{
    public class AppAutomapper:Profile
    {
        public AppAutomapper()
        {
            CreateMap<Specialty, SpecialtyModel>().ReverseMap();

        }

    }
}
