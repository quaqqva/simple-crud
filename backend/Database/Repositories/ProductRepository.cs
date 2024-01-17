using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Database.Repositories
{
    public class ProductRepository: Repository<Product>
    {
        protected override DbSet<Product> DbSet { get; init; }

        protected override IQueryable<Product> EntitiesDetails => DbSet.Include((product) => product.Orders)
                                                                       .Include((product) => product.Workshop);

        public ProductRepository(TypographyContext context): base(context) 
        {
            DbSet = context.Products;
        }
    }
}