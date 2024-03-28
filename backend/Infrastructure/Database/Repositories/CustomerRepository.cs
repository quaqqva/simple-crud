using Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Database.Repositories;

public class CustomerRepository(TypographyContext context) : Repository<Customer>(context)
{
    protected override DbSet<Customer> DbSet { get; init; } = context.Customers;

    protected override IQueryable<Customer> EntitiesDetails =>
        DbSet.Include(customer => customer.Address).Include(customer => customer.Contracts);
}