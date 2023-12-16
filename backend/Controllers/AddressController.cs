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

        public override async Task<ActionResult<Address>> Post([FromBody] AddressViewModel addressViewModel) {
            if (addressViewModel == null) return new BadRequestResult();

            var address = new Address() {
              Id = null,
              Country = addressViewModel.Country,
              City = addressViewModel.City,
              Street = addressViewModel.Street,
              Building = addressViewModel.Building
            };
            return new ObjectResult(await _repository.Create(address));
        }

        public override async Task<ActionResult<Address>> Put(int id, [FromBody] AddressViewModel addressViewModel)
        {
            var address = new Address() {
                Id = id,
                Country = addressViewModel.Country,
                City = addressViewModel.City,
                Street = addressViewModel.Street,
                Building = addressViewModel.Building
            };
            try {
                await _repository.Update(address);
                return new ObjectResult(await _repository.Read(id));
            } catch {
                return new BadRequestResult();
            }
        }
    }
}