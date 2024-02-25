using backend.Database;
using backend.Database.Repositories;
using backend.Dtos;
using backend.Entities;
using backend.WebSocket;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("customers")]
    public class CustomerController(
        TypographyContext context,
        EntityNotificationHubOperator<Customer> notificationHub
    ) : EntityController<Customer, CustomerDto>(notificationHub)
    {
        protected override Repository<Customer> Repository { get; init; } =
            new CustomerRepository(context);

        protected override Customer EntityFromDto(CustomerDto dto, Guid? id = null)
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
