using bookingcare.Data;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Data;

namespace bookingcare.Models
{
    public class BookingModel
    {
        public int ScheduleId { get; set; }
        public int StatusId { get; set; }
        public string PatientId { get; set; }
    }
}
