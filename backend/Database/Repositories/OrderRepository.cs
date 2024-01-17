using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Database.Repositories
{
    public class OrderRepository: Repository<Order>
    {
        protected override DbSet<Order> DbSet { get; init; }

        protected override IQueryable<Order> EntitiesDetails => DbSet.Include((order) => order.Contract)
                                                                     .Include((order) => order.Product);

        public OrderRepository(TypographyContext context): base(context) 
        {
            DbSet = context.Orders;
        }
    }
}