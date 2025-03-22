using Microsoft.EntityFrameworkCore;
using NetCoreAI.Projet01_ApiDemo.Entities;

namespace NetCoreAI.Projet01_ApiDemo.Context
{
    public class ApiContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\hasan; initial catalog=ApiAIDb; integrated security=true; trustservercertificate=true");
        }

        public DbSet<Customer> Customers { get; set; }
    }
}
