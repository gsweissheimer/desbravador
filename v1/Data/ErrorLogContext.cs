using Microsoft.EntityFrameworkCore;
using v1.Models;
using v1.Repository;

namespace v1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ErrorLog> ErrorLogs { get; set; }
    }
}
