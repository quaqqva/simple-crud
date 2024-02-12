using backend.Database;
using backend.Database.Repositories;
using backend.Dtos;
using backend.Entities;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("addresses")]
    public class AddressController : BaseController<Address, AddressDto>
    {
        protected override Repository<Address> Repository { get; init; }

        public AddressController(TypographyContext context)
        {
            Repository = new AddressRepository(context);
        }

        protected override Address EntityFromDto(AddressDto dto, int? id = null)
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
