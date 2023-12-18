using backend.Models;
using backend.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressController : BaseController<Address, AddressViewModel>
    {
        public AddressController(TypographyContext context): 
        base(
        context, 
        (context) => context.Addresses,
        (address) => address.Id,
        (context) => context.Addresses.Include(address => address.Customers).ToArrayAsync()) {}

        protected override Address EntityFromViewModel(AddressViewModel viewModel, int? id = null)
        {
            return new Address() {
                Id = id,
                Country = viewModel.Country,
                City = viewModel.City,
                Street = viewModel.Street,
                Building = viewModel.Building
            };
        }

        protected override Address UpdateEntity(Address source, Address incoming)
        {
            source.Country = incoming.Country;
            source.City = incoming.City;
            source.Street = incoming.Street;
            source.Building = incoming.Building;
            return source;
        }
    }
}