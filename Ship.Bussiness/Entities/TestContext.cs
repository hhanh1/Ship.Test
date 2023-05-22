
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ship.Bussiness.Entities
{
    public class TestContext : DbContext
    {
        public TestContext()
        {

        }

        public TestContext(DbContextOptions<TestContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var defaultPorts = new List<Port>();
            var Random = new Random();
            for (int i = 0; i < 10; i++)
            {
                int lat = Random.Next(516400146, 630304598);
                int lon = Random.Next(224464416, 341194152);
                defaultPorts.Add(new Port() { Name = $"Port {i}", Id = Guid.NewGuid(), Latitude = 18d + lat / 1000000000d, Longitude = -72d - lon / 1000000000d });
            }
            modelBuilder.Entity<Port>().HasData(
               defaultPorts
            );
        }
        public DbSet<Ship> Ships { get; set; }
        public DbSet<Port> Ports { get; set; }
    }
}
