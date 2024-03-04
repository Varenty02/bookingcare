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
            modelBuilder.Entity<Booking>().HasKey(e => new {e.PatientId,e.ScheduleId});
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
            builder.Entity<Position>().HasData
                (

                new TimeType() { Id = 1, Value = "None", ValueVie = "Bác sĩ" },
                new TimeType() { Id = 2, Value = "Master", ValueVie = "Thạc sĩ" },
                new TimeType() { Id = 3, Value = "Doctor", ValueVie = "Tiến sĩ" },
                new TimeType() { Id = 4, Value = "Associate Professor", ValueVie = "Phó giáo sư" },
                new TimeType() { Id = 5, Value = "Professor", ValueVie = "Giáo sư" }

                );
            builder.Entity<Gender>().HasData
                (

                new TimeType() { Id = 1, Value = "Male", ValueVie = "Nam" },
                new TimeType() { Id = 2, Value = "Female", ValueVie = "Nữ" },
                new TimeType() { Id = 3, Value = "Other", ValueVie = "Khác" }

                );

        }
        public DbSet<Booking>? Bookings { get; set; }
        public DbSet<Clinic>? Clinics { get; set; }
        //public DbSet<DoctorClinicSpecialty>? DoctorClinicSpecialties { get; set; }
        //public DbSet<History>? Histories { get; set; }
        public DbSet<Specialty>? Specialtys { get; set; }

        //Metadata
        public DbSet<Status>? Statuss { get; set; }
        
        public DbSet<TimeType>? TimeTypes { get; set; }
        public DbSet<Gender>? Genders { get; set; }

        public DbSet<Position>? Positions { get; set; }
        public DbSet<DoctorInfo>? DoctorInfos { get; set; }
        public DbSet<Schedule>? Schedules { get; set; }
    }
}
