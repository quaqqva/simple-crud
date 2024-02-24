using backend.Database;
using backend.Database.Repositories;
using backend.Dtos;
using backend.Entities;
using backend.WebSocket;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("contracts")]
    public class ContractController(
        TypographyContext context,
        EntityNotificationHubOperator<Contract> notificationHub
    ) : BaseController<Contract, ContractDto>(notificationHub)
    {
        protected override Repository<Contract> Repository { get; init; } =
            new ContractRepository(context);

        protected override Contract EntityFromDto(ContractDto dto, int? id = null)
        {
            return new Contract()
            {
                Id = id,
                CompletionDate = dto.CompletionDate,
                RegistrationDate = dto.RegistrationDate,
                CustomerId = dto.CustomerId
            };
        }
    }
}
