using Microsoft.EntityFrameworkCore;

namespace cmcsapp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Claim>? Claims { get; set; }

        public DbSet<Lecturer>? Lecturers { get; set; }
    }

    
}

