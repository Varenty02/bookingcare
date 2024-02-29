using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Data;

namespace bookingcare.Data
{
    public class Booking
    {
        [Key]
        public string Id { get; set; }

        public string StatusId { get; set; }
        [ForeignKey(nameof(StatusId))]
        public Status Status { get; set; }
        public string DoctorId { get; set; }
        [ForeignKey(nameof(DoctorId))]
        public virtual AppUser Doctor { get; set; }
        public string? PatientId { get; set; }
        
        [InverseProperty("Bookings")]
        [ForeignKey(nameof(PatientId))]
        
        public virtual AppUser Patient { get; set; }
        public DateTime Date { get; set; }
        public string TimeId { get; set; }
        [ForeignKey(nameof(TimeId))]
        public TimeType TimeType { get; set; }
    }
}
