using Microsoft.EntityFrameworkCore;
using UserC.Domain.Entities;

namespace UserC.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }
    
    /// <summary>
    /// 類目
    /// </summary>
    public DbSet<Category> Categories { get; set; }
    
    /// <summary>
    /// 品牌
    /// </summary>
    public DbSet<Brand> Brands { get; set; }
    
    /// <summary>
    /// 鏈結
    /// </summary>
    public DbSet<Item> Items { get; set; }

    /// <summary>
    /// 使用者
    /// </summary>
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var config = new DbConfig();
        modelBuilder.ApplyConfiguration<Category>(config);
        modelBuilder.ApplyConfiguration<Brand>(config);
        modelBuilder.ApplyConfiguration<Item>(config);
        modelBuilder.ApplyConfiguration<SKU>(config);
        modelBuilder.ApplyConfiguration<Inventory>(config);
        modelBuilder.ApplyConfiguration<User>(config);
    }
    
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<DateTimeOffset>().HaveConversion<DateTimeOffsetToUtcConverter>();
    }
}