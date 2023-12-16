using backend.Models;
using backend.Database;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressController : BaseController<Address, AddressViewModel>
    {
        public AddressController(TypographyContext context): base(context, (context) => context.Addresses) {}

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
    }
}