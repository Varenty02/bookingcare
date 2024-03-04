using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Data;

namespace bookingcare.Data
{
    public class Booking
    {
        public int ScheduleId { get; set; }
        [ForeignKey(nameof(ScheduleId))]
        public Schedule Schedule { get; set; }
        public int StatusId { get; set; }
        [ForeignKey(nameof(StatusId))]
        public Status Status { get; set; }
        public string PatientId { get; set; }

        [ForeignKey(nameof(PatientId))]

        public  AppUser Patient { get; set; }
    }
}
