using Microsoft.EntityFrameworkCore;
using UserC.Domain.Entities;
using UserC.Domain.Entities.Items;
using UserC.Domain.Entities.Orders;
using UserC.Infrastructure.Persistence.Config;

namespace UserC.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }
    
    /// <summary>
    /// 類目
    /// </summary>
    public DbSet<Category> Categories { get; set; }
    
    /// <summary>
    /// 鏈結
    /// </summary>
    public DbSet<Item> Items { get; set; }

    /// <summary>
    /// 相簿
    /// </summary>
    public DbSet<Album> Albums { get; set; }

    /// <summary>
    /// 庫存單元
    /// </summary>
    public DbSet<SKU> SKUs { get; set; }

    /// <summary>
    /// 使用者
    /// </summary>
    public DbSet<User> Users { get; set; }

    /// <summary>
    /// 交易(訂單)
    /// </summary>
    public DbSet<Order> Orders { get; set; }

    /// <summary>
    /// 付款資訊
    /// </summary>
    public DbSet<Payment> Payments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var itemConfig = new ItemConfig();
        modelBuilder.ApplyConfiguration<Category>(itemConfig);
        modelBuilder.ApplyConfiguration<CAttribute>(itemConfig);
        modelBuilder.ApplyConfiguration<Item>(itemConfig);
        modelBuilder.ApplyConfiguration<Album>(itemConfig);
        modelBuilder.ApplyConfiguration<SKU>(itemConfig);
        
        var orderConfig = new OrderConfig();
        modelBuilder.ApplyConfiguration<Order>(orderConfig);
        modelBuilder.ApplyConfiguration<OrderAmount>(orderConfig);
        modelBuilder.ApplyConfiguration<OrderRecord>(orderConfig);
        modelBuilder.ApplyConfiguration<OrderShipment>(orderConfig);
        modelBuilder.ApplyConfiguration<Payment>(orderConfig);

        var userConfig = new UserConfig();
        modelBuilder.ApplyConfiguration(userConfig);
    }
    
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<DateTimeOffset>().HaveConversion<DateTimeOffsetToUtcConverter>();
    }
}