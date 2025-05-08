using Dealship.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Dealship.Api.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    { }

    public DbSet<Car> Cars => Set<Car>();
    public DbSet<Color> Colors => Set<Color>();
    public DbSet<Engine> Engines => Set<Engine>();
    public DbSet<FuelType> FuelTypes => Set<FuelType>();
    public DbSet<Transmission> Transmissions => Set<Transmission>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder mb)
    {
        base.OnModelCreating(mb);

        mb.Entity<Car>()
          .HasOne(c => c.Engine).WithMany().HasForeignKey(c => c.EngineId);
        mb.Entity<Car>()
          .HasOne(c => c.Transmission).WithMany().HasForeignKey(c => c.TransmissionId);
        mb.Entity<Car>()
          .HasOne(c => c.FuelType).WithMany().HasForeignKey(c => c.FuelTypeId);
        mb.Entity<Car>()
          .HasOne(c => c.Color).WithMany().HasForeignKey(c => c.ColorId);

        mb.Entity<Order>()
          .HasOne(o => o.Customer).WithMany().HasForeignKey(o => o.CustomerId);
        mb.Entity<Order>()
          .HasOne(o => o.Car).WithMany().HasForeignKey(o => o.CarId);

        mb.Entity<Car>().Property(c => c.Price).HasPrecision(18, 2);
        mb.Entity<Order>().Property(o => o.TotalPrice).HasPrecision(18, 2);

        mb.Entity<User>().HasIndex(u => u.Email).IsUnique();
    }
}
