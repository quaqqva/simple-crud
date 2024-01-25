using System.Linq.Expressions;
using backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Database.Repositories
{
    public class ContractRepository: Repository<Contract>
    {
        protected override DbSet<Contract> DbSet { get; init; }

        protected override IQueryable<Contract> EntitiesDetails => DbSet.Include(contract => contract.Customer)
                                                                        .Include(customer => customer.Orders);

        protected override Dictionary<string, Expression<Func<Contract, dynamic?>>> PropertyCallbacks => new() {
            ["completionDate"] = (contract) => contract.CompletionDate,
            ["registrationDate"] = (contract) => contract.RegistrationDate,
            ["customerId"] = (contract) => contract.CustomerId,
            ["customer"] = (contract) => contract.Customer,
            ["orders"] = (contract) => contract.Orders
        };

        public ContractRepository(TypographyContext context): base(context) 
        {
            DbSet = context.Contracts;
        }
    }
}