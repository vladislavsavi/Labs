using Labs.Models;
using Microsoft.EntityFrameworkCore;

namespace Labs
{
    public class AppContext: DbContext
    {   
        public DbSet<Customer> Customers { get; set; }
        public AppContext(DbContextOptions<AppContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
