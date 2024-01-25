using System.Linq.Expressions;
using backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Database.Repositories
{
    public class ProductRepository: Repository<Product>
    {
        protected override DbSet<Product> DbSet { get; init; }

        protected override IQueryable<Product> EntitiesDetails => DbSet.Include((product) => product.Orders)
                                                                       .Include((product) => product.Workshop);

        protected override Dictionary<string, Expression<Func<Product, dynamic?>>> PropertyCallbacks => new() {
            ["name"] = (product) => product.Name,
            ["price"] = (product) => product.Price,
            ["workshopNumber"] = (product) => product.WorkshopNumber,
            ["orders"] = (product) => product.Orders,
            ["workshop"] = (product) => product.Workshop
        };

        public ProductRepository(TypographyContext context): base(context) 
        {
            DbSet = context.Products;
        }
    }
}