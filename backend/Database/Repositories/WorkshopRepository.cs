using backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Database.Repositories
{
    public class WorkshopRepository : Repository<Workshop>
    {
        protected override DbSet<Workshop> DbSet { get; init; }

        protected override IQueryable<Workshop> EntitiesDetails =>
            DbSet.Include((workshop) => workshop.Products).Include((workshop) => workshop.Chief);

        public WorkshopRepository(TypographyContext context)
            : base(context)
        {
            DbSet = context.Workshops;
        }
    }
}
