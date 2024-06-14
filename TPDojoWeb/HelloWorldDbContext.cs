using Microsoft.EntityFrameworkCore;
using TPDojoWeb.BO;

namespace TPDojoWeb
{
    public class HelloWorldDbContext : DbContext, IHelloWorldDbContext
    {
        public HelloWorldDbContext()
        {
        }

        public HelloWorldDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Samourai> Samourais { get; set; }

        public virtual DbSet<Arme> Armes { get; set; }
        public bool IsWeaponAssignedToSamurai(int weaponId)
        {
            return Set<Samourai>().Any(s => s.Arme.Id == weaponId);
        }
        public DbSet<TPDojoWeb.BO.ArtMartial> ArtMartial { get; set; } = default!;
    }
}
