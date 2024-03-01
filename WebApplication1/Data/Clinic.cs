﻿using System.ComponentModel.DataAnnotations;

namespace bookingcare.Data
{
    public class Clinic
    {
        [Key]
        public int Id { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string Image { get;set; }
        public string Name { get; set; }
    }
}
