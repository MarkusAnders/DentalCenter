using Microsoft.EntityFrameworkCore;

namespace DentalCenter.Models
{
    public class DentalCenterDBContext : DbContext
    {
        public DentalCenterDBContext(DbContextOptions<DentalCenterDBContext> options) : base(options) { }
        
        public DbSet<Client> Clients { get; set; }  
    }
}
