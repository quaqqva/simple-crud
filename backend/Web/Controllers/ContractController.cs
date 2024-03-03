using Backend.Application.Dtos;
using Backend.Application.Interfaces;
using Backend.Infrastructure.Database;
using Backend.Infrastructure.Database.Repositories;
using Microsoft.AspNetCore.Mvc;
using Contract = Backend.Domain.Entities.Contract;

namespace backend.Controllers
{
    [ApiController]
    [Route("contracts")]
    public class ContractController(
        TypographyContext context,
        IEntityNotificationsService<Contract> notificationsService
    ) : EntityController<Contract, ContractDto>(notificationsService)
    {
        protected override IRepository<Contract> Repository { get; init; } =
            new ContractRepository(context);

        protected override Contract EntityFromDto(ContractDto dto, Guid? id = null)
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
