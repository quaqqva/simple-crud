using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Database.Repositories
{
    public class ChiefRepository: Repository<Chief>
    {
        protected override DbSet<Chief> DbSet { get; init; }

        protected override IQueryable<Chief> EntitiesDetails => DbSet.Include(chief => chief.Workshops);

        public ChiefRepository(TypographyContext context): base(context) 
        {
            DbSet = context.Chiefs;
        }
    }
}