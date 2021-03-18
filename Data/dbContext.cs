using System.Data.Entity;
using ToDoListTeldat.Models;

namespace ToDoListTeldat.Data
{
    public class dbContext : DbContext
    {
        public dbContext() : base("Data")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<dbContext>());
        }

        public DbSet<Item> Items { get; set; }
    }
}
