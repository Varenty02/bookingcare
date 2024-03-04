using bookingcare.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations.Schema;

namespace bookingcare.Models
{
    public class ScheduleModel
    {
        public int Id { get; set; }
        public int CurrentNumber { get; set; }
        public int MaxNumber { get; set; }
        public DateTime Date { get; set; }
        public int TimeId { get; set; }
        public string DoctorId { get; set; }
    }
    public class ScheduleCreateModel
    {
        public int CurrentNumber { get; set; }
        public int MaxNumber { get; set; }
        public DateTime Date { get; set; }
        public int TimeId { get; set; }
        //[BindNever]
        public string DoctorId { get; set; }
    }
}
