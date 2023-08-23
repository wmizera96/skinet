using System.Reflection;
using API.Data;
using Core.Entities;
using Infrastructure.ValueConverters;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class StoreContext : DbContext
{
    private readonly UserProvider _userProvider;

    public StoreContext(DbContextOptions options, UserProvider userProvider) : base(options)
    {
        _userProvider = userProvider;
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<ProductBrand> ProductBrands => Set<ProductBrand>();
    public DbSet<ProductType> ProductTypes => Set<ProductType>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(new SoftDeleteInterceptor(_userProvider));
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyGlobalFilters<BaseEntity>(x => x.IsDeleted == false);
        
        modelBuilder.FixSqliteDateTimeOffset(Database);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
        
        configurationBuilder.Properties<ProductId>().HaveConversion<ProductIdValueConverter>();
        configurationBuilder.Properties<ProductBrandId>().HaveConversion<ProductBrandIdValueConverter>();
        configurationBuilder.Properties<ProductTypeId>().HaveConversion<ProductTypeIdValueConverter>();
    }
}