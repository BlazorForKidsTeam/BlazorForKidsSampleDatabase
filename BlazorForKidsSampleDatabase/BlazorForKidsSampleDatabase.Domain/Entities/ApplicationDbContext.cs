using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlazorForKidsSampleDatabase.Domain.Entities
{
    public partial class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : IdentityDbContext<ApplicationUser>(options)
    {

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            // e.g. 9999999999.99 is the largest allowed value
            configurationBuilder.Properties<decimal>().HavePrecision(12, 2);

            base.ConfigureConventions(configurationBuilder);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
