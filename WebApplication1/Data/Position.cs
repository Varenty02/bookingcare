using System.ComponentModel.DataAnnotations;

namespace bookingcare.Data
{
    public class Position
    {
        [Key]
        public int Id { get; set; }
        public string Value { get; set; }
        public string ValueVie { get; set; }
    }
}
