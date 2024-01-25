using System.Linq.Expressions;
using backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Database.Repositories
{
    public class AddressRepository: Repository<Address>
    {
        protected override DbSet<Address> DbSet { get; init; }

        protected override IQueryable<Address> EntitiesDetails => DbSet.Include(address => address.Customers);

        protected override Dictionary<string, Expression<Func<Address, dynamic?>>> PropertyCallbacks => new() {
            ["country"] = (address) => address.Country,
            ["city"] = (address) => address.City,
            ["street"] = (address) => address.Street,
            ["building"] = (address) => address.Building,
            ["customers"] = (address) => address.Customers
        };

        public AddressRepository(TypographyContext context): base(context) 
        {
            DbSet = context.Addresses;
        }
    }
}