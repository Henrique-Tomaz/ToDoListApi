using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using DataAccess.Entities;

namespace ToDoListApi
{
    public class ToDoItemContextFactory : IDesignTimeDbContextFactory<ToDoItemContext>
    {
        public ToDoItemContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var optionsBuilder = new DbContextOptionsBuilder<ToDoItemContext>();
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));

            return new ToDoItemContext(optionsBuilder.Options);
        }
    }
}
