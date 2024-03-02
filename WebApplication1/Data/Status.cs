using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bookingcare.Data
{
    public class Status
    {
        [Key]
        public int Id { get; set; }
        public string Value { get;set; }
        public string ValueVie { get;set; }
        //public ICollection<Booking> Bookings { get; set; }  
    }
}
