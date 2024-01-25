using System.Linq.Expressions;
using backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Database.Repositories
{
    public class ChiefRepository: Repository<Chief>
    {
        protected override DbSet<Chief> DbSet { get; init; }

        protected override IQueryable<Chief> EntitiesDetails => DbSet.Include(chief => chief.Workshops);

        protected override Dictionary<string, Expression<Func<Chief, dynamic?>>> PropertyCallbacks => new() {
            ["firstName"] = (chief) => chief.FirstName,
            ["lastName"] = (chief) => chief.LastName,
            ["patronymic"] = (chief) => chief.Patronymic,
            ["workshops"] = (chief) => chief.Workshops
        };

        public ChiefRepository(TypographyContext context): base(context) 
        {
            DbSet = context.Chiefs;
        }
    }
}