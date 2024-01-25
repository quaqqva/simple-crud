using System.Linq.Expressions;
using backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Database.Repositories
{
    public class WorkshopRepository: Repository<Workshop>
    {
        protected override DbSet<Workshop> DbSet { get; init; }

        protected override IQueryable<Workshop> EntitiesDetails => DbSet.Include((workshop) => workshop.Products)
                                                                        .Include((workshop) => workshop.Chief);

        protected override Dictionary<string, Expression<Func<Workshop, dynamic?>>> PropertyCallbacks => new() {
            ["name"] = (workshop) => workshop.Name,
            ["phoneNumber"] = (workshop) => workshop.PhoneNumber,
            ["chiefId"] = (workshop) => workshop.ChiefId,
            ["chief"] = (workshop) => workshop.Chief,
            ["products"] = (workshop) => workshop.Products
        };

        public WorkshopRepository(TypographyContext context): base(context) 
        {
            DbSet = context.Workshops;
        }
    }
}