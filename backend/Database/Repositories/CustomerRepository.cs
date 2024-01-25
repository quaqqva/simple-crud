using System.Linq.Expressions;
using backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Database.Repositories
{
    public class CustomerRepository: Repository<Customer>
    {
        protected override DbSet<Customer> DbSet { get; init; }

        protected override IQueryable<Customer> EntitiesDetails => DbSet.Include(customer => customer.Address)
                                                                        .Include(customer => customer.Contracts);

        protected override Dictionary<string, Expression<Func<Customer, dynamic?>>> PropertyCallbacks => new() {
            ["name"] = (customer) => customer.Name,
            ["addressId"] = (customer) => customer.AddressId,
            ["address"] = (customer) => customer.Address,
            ["contracts"] = (customer) => customer.Contracts
        };

        public CustomerRepository(TypographyContext context): base(context) 
        {
            DbSet = context.Customers;
        }
    }
}