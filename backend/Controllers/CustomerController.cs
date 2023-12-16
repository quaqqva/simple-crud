using backend.Models;
using backend.Database;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : BaseController<Customer, CustomerViewModel>
    {
        public CustomerController(TypographyContext context): base(context, (context) => context.Customers) {}

        protected override Customer EntityFromViewModel(CustomerViewModel viewModel, int? id = null)
        {
            return new Customer() {
                Id = id,
                Name = viewModel.Name,
                AddressId = viewModel.AddressId
            };
        }
    }
}