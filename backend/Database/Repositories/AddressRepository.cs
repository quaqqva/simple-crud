using backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Database.Repositories
{
    public class AddressRepository : Repository<Address>
    {
        protected override DbSet<Address> DbSet { get; init; }

        protected override IQueryable<Address> EntitiesDetails =>
            DbSet.Include(address => address.Customers);

        public AddressRepository(TypographyContext context)
            : base(context)
        {
            DbSet = context.Addresses;
        }
    }
}
