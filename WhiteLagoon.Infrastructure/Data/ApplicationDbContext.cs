using Microsoft.EntityFrameworkCore;

namespace WhiteLagoon.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        //constructor : Whatever parameters(DbContextOptions<ApplicationDbContext> options) we pass to the constructor, it will be passed to the base class constructor
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
             
        }
    }
}