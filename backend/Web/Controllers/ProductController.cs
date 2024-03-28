using Backend.Application.Dtos;
using Backend.Application.Interfaces;
using Backend.Domain.Entities;
using Backend.Infrastructure.Database;
using Backend.Infrastructure.Database.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("products")]
public class ProductController(
    TypographyContext context,
    IEntityNotificationsService<Product> notificationsService
) : EntityController<Product, ProductDto>(notificationsService)
{
    protected override IRepository<Product> Repository { get; init; } =
        new ProductRepository(context);

    protected override Product EntityFromDto(ProductDto dto, Guid? id = null)
    {
        return new Product
        {
            Id = id,
            Name = dto.Name,
            WorkshopId = dto.WorkshopId,
            Price = dto.Price
        };
    }
}