using bookingcare.Data;
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
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }
        }
        public DbSet<Booking>? Bookings { get; set; }
        public DbSet<Clinic>? Clinics { get; set; }
        public DbSet<DoctorClinicSpecialty>? DoctorClinicSpecialtys { get; set; }
        public DbSet<History>? Historys { get; set; }
        public DbSet<Specialty>? Specialtys { get; set; }
        public DbSet<Status>? Statuss { get; set; }
        public DbSet<Schedule>? Schedules { get; set; }
        public DbSet<TimeType>? TimeTypes { get; set; }

    }
}
