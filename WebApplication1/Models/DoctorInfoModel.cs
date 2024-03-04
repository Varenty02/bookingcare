namespace bookingcare.Models
{
    public class DoctorInfoModel
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public string Province { get; set; }
        public string Description { get; set; }
        public int YearOfEx { get; set; }
        public string DoctorId { get; set; }
    }
    public class DoctorInfoCreateModel
    {
        public int Price { get; set; }
        public string Province { get; set; }
        public string Description { get; set; }
        public int YearOfEx { get; set; }
        
    }
}
