using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Data;

namespace bookingcare.Data
{
    public class DoctorClinicSpecialty
    {
        [Key]
        public string Id { get; set; }
        public string DoctorId { get; set; }
        [ForeignKey(nameof(DoctorId))]
        public virtual AppUser Doctor { get; set; }
        public string? ClinicId { get; set; }

        [ForeignKey(nameof(ClinicId))]
        public Clinic Clinic { get; set; }
        public int SpecialtyId { get; set; }

        [ForeignKey(nameof(SpecialtyId))]
        public Specialty Specialty { get; set; }
    }
}
