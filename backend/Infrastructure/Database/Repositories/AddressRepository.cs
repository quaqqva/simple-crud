using Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Database.Repositories;

public class AddressRepository(TypographyContext context) : Repository<Address>(context)
{
    protected override DbSet<Address> DbSet { get; init; } = context.Addresses;

    protected override IQueryable<Address> EntitiesDetails =>
        DbSet.Include(address => address.Customers);
}