using backend.Database;
using backend.Database.Repositories;
using backend.Dtos;
using backend.Entities;
using backend.WebSocket;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("workshops")]
    public class WorkshopController(
        TypographyContext context,
        EntityNotificationHubOperator<Workshop> notificationHub
    ) : EntityController<Workshop, WorkshopDto>(notificationHub)
    {
        protected override Repository<Workshop> Repository { get; init; } =
            new WorkshopRepository(context);

        protected override Workshop EntityFromDto(WorkshopDto dto, int? id = null)
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
