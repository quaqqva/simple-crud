using System.Linq.Expressions;
using backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Database.Repositories
{
    public class OrderRepository: Repository<Order>
    {
        protected override DbSet<Order> DbSet { get; init; }

        protected override IQueryable<Order> EntitiesDetails => DbSet.Include((order) => order.Contract)
                                                                     .Include((order) => order.Product);

        protected override Dictionary<string, Expression<Func<Order, dynamic?>>> PropertyCallbacks => new() {
            ["productQuantity"] = (order) => order.ProductQuantity,
            ["code"] = (order) => order.ProductCode,
            ["contractNumber"] = (order) => order.ContractNumber,
            ["contract"] = (order) => order.Contract,
            ["product"] = (order) => order.Product
        };

        public OrderRepository(TypographyContext context): base(context) 
        {
            DbSet = context.Orders;
        }
    }
}