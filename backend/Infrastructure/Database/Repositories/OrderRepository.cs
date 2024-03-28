using Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Database.Repositories;

public class OrderRepository(TypographyContext context) : Repository<Order>(context)
{
    protected override DbSet<Order> DbSet { get; init; } = context.Orders;

    protected override IQueryable<Order> EntitiesDetails =>
        DbSet.Include(order => order.Contract).Include(order => order.Product);
}