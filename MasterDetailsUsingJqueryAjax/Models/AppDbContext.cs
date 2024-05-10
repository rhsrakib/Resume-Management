using Microsoft.EntityFrameworkCore;

namespace MasterDetailsUsingJqueryAjax.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<Experiance> Experiances { get; set; }
        public DbSet<Designation> Designations { get; set; }
    }
}
