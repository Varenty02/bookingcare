using System.ComponentModel.DataAnnotations.Schema;
using System.Security;
using WebApplication1.Data;

namespace bookingcare.Data
{
    public class DoctorInfo
    {
        public int Id {  get; set; }
        public int Price { get; set; }
        public string Province { get; set; }
        public string Description { get; set; }
        public int YearOfEx {  get; set; }
        public string DoctorId { get; set; }
        [ForeignKey(nameof(DoctorId))]
        public AppUser Doctor { get; set; }
    }
}
