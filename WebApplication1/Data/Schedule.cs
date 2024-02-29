using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Data;

namespace bookingcare.Data
{
    public class Schedule
    {
        [Key]
        public string Id { get; set; }
        public int CurrentNumber { get; set; }
        public int MaxNumber { get; set; }
        public DateTime Date { get; set; }
        public string TimeId { get; set; }
        [ForeignKey(nameof(TimeId))]
        public TimeType TimeType { get; set; }
        public string DoctorId { get; set; }
        [ForeignKey(nameof(DoctorId))]
        public AppUser Doctor { get; set; }
    }
}
