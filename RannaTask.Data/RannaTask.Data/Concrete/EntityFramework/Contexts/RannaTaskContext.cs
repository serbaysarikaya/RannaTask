using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RannaTask.Entities.Concrete;

namespace RannaTask.Data.Concrete.EntityFramework.Contexts
{
    public class RannaTaskContext : IdentityDbContext
    {
        public DbSet<Product> Products { get; set; }
        public RannaTaskContext(DbContextOptions<RannaTaskContext> options) : base(options)
        {

        }

    }
}
