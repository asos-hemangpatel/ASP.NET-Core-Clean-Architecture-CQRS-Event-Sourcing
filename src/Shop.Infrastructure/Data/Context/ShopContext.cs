using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Shop.Core.AppSettings;
using Shop.Domain.Entities;
using Shop.Infrastructure.Data.Extensions;

namespace Shop.Infrastructure.Data.Context;

public class ShopContext : DbContext
{
    private readonly string _collation;

    public ShopContext(IOptions<ConnectionStrings> options, DbContextOptions<ShopContext> dbOptions) : base(dbOptions)
    {
        _collation = options.Value.Collation;
        ChangeTracker.LazyLoadingEnabled = false;
    }

    public DbSet<CatalogBrand> CatalogBrands => Set<CatalogBrand>();
    public DbSet<CatalogItem> CatalogItems => Set<CatalogItem>();
    public DbSet<CatalogType> CatalogTypes => Set<CatalogType>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        if (!string.IsNullOrWhiteSpace(_collation))
            modelBuilder.UseCollation(_collation);

        modelBuilder
                .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly())
                .RemoveCascadeDeleteConvention();
    }
}