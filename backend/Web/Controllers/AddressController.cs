using Backend.Application.Dtos;
using Backend.Application.Interfaces;
using Backend.Domain.Entities;
using Backend.Infrastructure.Database;
using Backend.Infrastructure.Database.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("addresses")]
    public class AddressController(
        TypographyContext context,
        IEntityNotificationsService<Address> notificationsService
    ) : EntityController<Address, AddressDto>(notificationsService)
    {
        protected override IRepository<Address> Repository { get; init; } =
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
