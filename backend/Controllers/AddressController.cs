using backend.Database;
using backend.Database.Repositories;
using backend.Dtos;
using backend.Entities;
using backend.WebSocket;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("addresses")]
    public class AddressController(
        TypographyContext context,
        EntityNotificationHubOperator<Address> notificationHub
    ) : EntityController<Address, AddressDto>(notificationHub)
    {
        protected override Repository<Address> Repository { get; init; } =
            new AddressRepository(context);

        protected override Address EntityFromDto(AddressDto dto, Guid? id = null)
        {
            return new Address()
            {
                Id = id,
                Country = dto.Country,
                City = dto.City,
                Street = dto.Street,
                Building = dto.Building
            };
        }
    }
}
