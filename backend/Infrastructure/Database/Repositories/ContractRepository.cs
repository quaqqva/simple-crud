using Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Database.Repositories;

public class ContractRepository(TypographyContext context) : Repository<Contract>(context)
{
    protected override DbSet<Contract> DbSet { get; init; } = context.Contracts;

    protected override IQueryable<Contract> EntitiesDetails =>
        DbSet.Include(contract => contract.Customer).Include(customer => customer.Orders);
}