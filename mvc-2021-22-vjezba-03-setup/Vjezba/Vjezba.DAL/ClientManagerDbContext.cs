using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vjezba.Model;

namespace Vjezba.DAL
{
    public class ClientManagerDbContext : DbContext
    {
        protected ClientManagerDbContext() { }
        public ClientManagerDbContext(DbContextOptions<ClientManagerDbContext> options) : base(options){}

        public DbSet<City> Cities { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Meeting> Meetings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<City>().HasData(new City { Id = 1, Name = "Zagreb" }, new City { Id = 2, Name = "Prag"}, new City { Id = 3, Name = "Ljubljana" });
            modelBuilder.Entity<Client>().HasData(new Client { Id = 1, CityId = 1, FirstName = "Karlo", LastName = "Dragovic", Email = "karlodragovic@gmail.com",
            Gender = 'M', PhoneNumber = "069-9585-466", Address = "Ilica 205"});
        }

    }
}
