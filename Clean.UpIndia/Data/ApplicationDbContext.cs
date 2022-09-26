
using Clean.UpIndia.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Clean.UpIndia.Data
{
    public class ApplicationDbContext
        : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> Options)
            : base(Options)
        {

        }
       
        public DbSet<Event> Events { get; set; }

        public DbSet<Volunteer> Volunteers { get; set; }

        public DbSet<Response> Responses { get; set; }

        public DbSet<Locality> Localities { get; set; }

        public DbSet<PublicToilet> PublicToilets { get; set; }

       

    }
}
