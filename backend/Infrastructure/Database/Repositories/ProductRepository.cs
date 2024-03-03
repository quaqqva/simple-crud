using Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Database.Repositories
{
    public class ProductRepository(TypographyContext context) : Repository<Product>(context)
    {
        protected override DbSet<Product> DbSet { get; init; } = context.Products;

        protected override IQueryable<Product> EntitiesDetails =>
            DbSet.Include((product) => product.Orders).Include((product) => product.Workshop);
    }
}
