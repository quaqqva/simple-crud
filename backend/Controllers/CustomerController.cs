using backend.Entities;
using backend.Database;
using Microsoft.AspNetCore.Mvc;
using backend.DTOs;
using backend.Database.Repositories;

namespace backend.Controllers
{
    [ApiController]
    [Route("customers")]
    public class CustomerController : BaseController<Customer, CustomerDTO>
    {
        protected override Repository<Customer> Repository { get; init; }
        public CustomerController(TypographyContext context)
        {
            Repository = new CustomerRepository(context);
        }

        protected override Customer EntityFromDTO(CustomerDTO dto, int? id = null)
        {
            return new Customer() {
                Id = id,
                Name = dto.Name,
                AddressId = dto.AddressId
            };
        }
    }
}