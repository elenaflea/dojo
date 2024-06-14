using Microsoft.EntityFrameworkCore;
using TPDojoAPI.BO;

namespace TPDojoAPI

{
    public interface IHelloWorldDbContext
    {
        DbSet<Arme> Armes { get; set; }
        DbSet<Samourai> Samourais { get; set; }

    }
}
