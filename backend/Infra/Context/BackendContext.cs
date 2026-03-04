using Core.Models;
using Infra.Context.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infra.Context;

public class BackendContext : DbContext
{
    public BackendContext(DbContextOptions<BackendContext> options) : base(options) { }
    public DbSet<User> Users { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}
