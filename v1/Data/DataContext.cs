using Microsoft.EntityFrameworkCore;
using v1.Entities;

namespace v1.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
                    
        }

        public DbSet<Project> Project {  get; set; }
    }
}
