using Microsoft.EntityFrameworkCore;
using DentalCenter.Models;

namespace DentalCenter.Models
{
    public class DentalCenterDBContext : DbContext
    {
        public DentalCenterDBContext(DbContextOptions<DentalCenterDBContext> options) : base(options) { }
        
        public DbSet<Client> Clients { get; set; }  
        public DbSet<Doctor> Doctors { get; set; } 
        public DbSet<DentalCenter.Models.Service> Service { get; set; } = default!;
        public DbSet<DentalCenter.Models.Contact> Contact { get; set; } = default!;
        public DbSet<DentalCenter.Models.Appointment> Appointment { get; set; } = default!;
    }
}
