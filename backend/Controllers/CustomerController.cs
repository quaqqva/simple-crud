using backend.Database;
using backend.Database.Repositories;
using backend.Dtos;
using backend.Entities;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("customers")]
    public class CustomerController : BaseController<Customer, CustomerDto>
    {
        protected override Repository<Customer> Repository { get; init; }

        public CustomerController(TypographyContext context)
        {
            Repository = new CustomerRepository(context);
        }

        protected override Customer EntityFromDto(CustomerDto dto, int? id = null)
        {
            return new Customer()
            {
                Id = id,
                Name = dto.Name,
                AddressId = dto.AddressId
            };
        }
    }
}
