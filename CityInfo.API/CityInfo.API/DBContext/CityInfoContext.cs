using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.DBContext
{
    public class CityInfoContext : DbContext
    {
        public CityInfoContext(DbContextOptions<CityInfoContext> options)
            : base(options)
        {
        }

        public DbSet<Entities.City> Cities { get; set; }
        public DbSet<Entities.PointOfInterest> PointsOfInterest { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.City>().HasData(
                new Entities.City("New York City")
                {
                    Id = 1,
                    Description = "The one with that big park."
                },
                new Entities.City("Antwerp")
                {
                    Id = 2,
                    Description = "The one with the cathedral that was never really finished."
                }
            );

            modelBuilder.Entity<Entities.PointOfInterest>().HasData(
                new Entities.PointOfInterest("Central Park")
                {
                    Id = 1,
                    CityId = 1
                },
                new Entities.PointOfInterest("Empire State Building")
                {
                    Id = 2,
                    CityId = 1
                },
                new Entities.PointOfInterest("Cathedral of Our Lady")
                {
                    Id = 3,
                    CityId = 2
                },
                new Entities.PointOfInterest("Antwerp Central Station")
                {
                    Id = 4,
                    CityId = 2
                }
            );
        }

    }
}
