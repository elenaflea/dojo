using Microsoft.EntityFrameworkCore;
using TPDojoWeb.BO;

namespace TPDojoWeb

{
    public interface IHelloWorldDbContext
    {
        DbSet<Arme> Armes { get; set; }
        DbSet<Samourai> Samourais { get; set; }

    }
}
