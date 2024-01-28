using backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Database.Repositories
{
    public class ContractRepository: Repository<Contract>
    {
        protected override DbSet<Contract> DbSet { get; init; }

        protected override IQueryable<Contract> EntitiesDetails => DbSet.Include(contract => contract.Customer)
                                                                        .Include(customer => customer.Orders);

        public ContractRepository(TypographyContext context): base(context) 
        {
            DbSet = context.Contracts;
        }
    }
}