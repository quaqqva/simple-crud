using Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Database.Repositories;

public class WorkshopRepository(TypographyContext context) : Repository<Workshop>(context)
{
    protected override DbSet<Workshop> DbSet { get; init; } = context.Workshops;

    protected override IQueryable<Workshop> EntitiesDetails =>
        DbSet.Include(workshop => workshop.Products).Include(workshop => workshop.Chief);
}