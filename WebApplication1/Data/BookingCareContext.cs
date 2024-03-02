using bookingcare.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data
{
    public class BookingCareContext : IdentityDbContext<AppUser>
    {
        public BookingCareContext(DbContextOptions<BookingCareContext> opt) : base(opt)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName!.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }
            SeedValue(modelBuilder);
        }
        private static void SeedValue(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData
                (
                new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
                new IdentityRole() { Name = "User", ConcurrencyStamp = "2", NormalizedName = "User" },
                 new IdentityRole() { Name = "Doctor", ConcurrencyStamp = "3", NormalizedName = "Doctor" }

                );
            builder.Entity<Status>().HasData
                (
                new Status() {Id=1, Value = "New", ValueVie = "Lịch hẹn mới" },
                new Status() { Id = 2, Value = "Confirmed", ValueVie = "Đã xác nhận" },
                new Status() { Id = 3, Value = "Done", ValueVie = "Đã khám xong" },
                new Status() { Id = 4, Value = "Cancel", ValueVie = "Đã hủy" }


                );
            builder.Entity<TimeType>().HasData
                (

                new TimeType() { Id = 1, Value = "8:00 AM - 9:00 AM", ValueVie = "8:00 - 9:00" },
                new TimeType() { Id = 2, Value = "9:00 AM - 10:00 AM", ValueVie = "9:00 - 10:00" },
                new TimeType() { Id = 3, Value = "10:00 AM - 11:00 AM", ValueVie = "10:00 - 11:00" },
                new TimeType() { Id = 4, Value = "11:00 AM - 0:00 PM", ValueVie = "11:00 - 12:00" },
                new TimeType() { Id = 5, Value = "1:00 PM - 2:00 PM", ValueVie = "13:00 - 14:00" },
                new TimeType() { Id = 6, Value = "2:00 PM - 3:00 PM", ValueVie = "14:00 - 15:00" },
                new TimeType() { Id = 7, Value = "3:00 PM - 4:00 PM", ValueVie = "15:00 - 16:00" },
                new TimeType() { Id = 8, Value = "4:00 PM - 5:00 PM", ValueVie = "16:00 - 17:00" }

                );

        }
        //public DbSet<Booking>? Bookings { get; set; }
        public DbSet<Clinic>? Clinics { get; set; }
        //public DbSet<DoctorClinicSpecialty>? DoctorClinicSpecialties { get; set; }
        //public DbSet<History>? Histories { get; set; }
        public DbSet<Specialty>? Specialtys { get; set; }
        public DbSet<Status>? Statuss { get; set; }
        //public DbSet<Schedule>? Schedules { get; set; }
        public DbSet<TimeType>? TimeTypes { get; set; }

    }
}
