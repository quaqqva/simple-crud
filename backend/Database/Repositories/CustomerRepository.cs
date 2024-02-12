using backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Database.Repositories
{
    public class CustomerRepository : Repository<Customer>
    {
        protected override DbSet<Customer> DbSet { get; init; }

        protected override IQueryable<Customer> EntitiesDetails =>
            DbSet.Include(customer => customer.Address).Include(customer => customer.Contracts);

        public CustomerRepository(TypographyContext context)
            : base(context)
        {
            DbSet = context.Customers;
        }
    }
}
