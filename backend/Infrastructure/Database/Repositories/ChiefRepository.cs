using Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Database.Repositories;

public class ChiefRepository(TypographyContext context) : Repository<Chief>(context)
{
    protected override DbSet<Chief> DbSet { get; init; } = context.Chiefs;

    protected override IQueryable<Chief> EntitiesDetails =>
        DbSet.Include(chief => chief.Workshops);
}