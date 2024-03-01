using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Data;

namespace bookingcare.Data
{
    public class Specialty
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
    }
}
