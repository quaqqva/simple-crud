using backend.Models;
using backend.DTOs;
using backend.Database;
using backend.Database.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressController : BaseController<Address, AddressDTO>
    {
        protected override Repository<Address> Repository { get; init; }

        public AddressController(TypographyContext context) {
            Repository = new AddressRepository(context);
        }

        protected override Address EntityFromDTO(AddressDTO dto, int? id = null)
        {
            return new Address() {
                Id = id,
                Country = dto.Country,
                City = dto.City,
                Street = dto.Street,
                Building = dto.Building
            };
        }
    }
}