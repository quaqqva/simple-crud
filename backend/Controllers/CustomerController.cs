using backend.Models;
using backend.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : BaseController<Customer, CustomerViewModel>
    {
        public CustomerController(TypographyContext context): 
        base(context, 
        (context) => context.Customers,
        (customer) => customer.Id,
        (context) => context.Customers
                            .Include(customer => customer.Address)
                            .Include(customer => customer.Contracts)
                            .ToArrayAsync()
        ) {}

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