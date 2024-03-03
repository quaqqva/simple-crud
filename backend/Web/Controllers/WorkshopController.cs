using Backend.Application.Dtos;
using Backend.Application.Interfaces;
using Backend.Domain.Entities;
using Backend.Infrastructure.Database;
using Backend.Infrastructure.Database.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("workshops")]
    public class WorkshopController(
        TypographyContext context,
        IEntityNotificationsService<Workshop> notificationHub
    ) : EntityController<Workshop, WorkshopDto>(notificationHub)
    {
        protected override IRepository<Workshop> Repository { get; init; } =
            new WorkshopRepository(context);

        protected override Workshop EntityFromDto(WorkshopDto dto, Guid? id = null)
        {
            return new Workshop()
            {
                Id = id,
                Name = dto.Name,
                PhoneNumber = dto.PhoneNumber,
                ChiefId = dto.ChiefId
            };
        }
    }
}
