using Microsoft.EntityFrameworkCore;
using CRMSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CRMSystem.Data
{
    public class AppDbContext : IdentityDbContext //IdentityDbContext is used for login purposes (authorization and authentiation)
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; } //All models are added here so they stay upto date within AppDbContext and actual backend

    }
}
