using Backend.Application.Dtos;
using Backend.Application.Interfaces;
using Backend.Domain.Entities;
using Backend.Infrastructure.Database;
using Backend.Infrastructure.Database.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("chiefs")]
public class ChiefController(
    TypographyContext context,
    IEntityNotificationsService<Chief> notificationHub
) : EntityController<Chief, ChiefDto>(notificationHub)
{
    protected override IRepository<Chief> Repository { get; init; } =
        new ChiefRepository(context);

    protected override Chief EntityFromDto(ChiefDto dto, Guid? id = null)
    {
        return new Chief
        {
            Id = id,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Patronymic = dto.Patronymic
        };
    }
}