using CRMSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext> //AppDbContextFactory to make AppDbContext with custom parameters
{
    public AppDbContext CreateDbContext(string[] args)
    {
        // Load configuration from appsettings.json
        IConfigurationRoot config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())  
            .AddJsonFile("appsettings.json")
            .Build();

        // Get the connection string
        var connectionString = config.GetConnectionString("DefaultConnection"); 

        // Set up options for the context
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        // Return new context instance
        return new AppDbContext(optionsBuilder.Options);
    }
}
