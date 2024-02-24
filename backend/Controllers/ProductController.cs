using backend.Database;
using backend.Database.Repositories;
using backend.Dtos;
using backend.Entities;
using backend.WebSocket;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductController(
        TypographyContext context,
        EntityNotificationHubOperator<Product> notificationHub
    ) : BaseController<Product, ProductDto>(notificationHub)
    {
        protected override Repository<Product> Repository { get; init; } =
            new ProductRepository(context);

        protected override Product EntityFromDto(ProductDto dto, int? id = null)
        {
            return new Product()
            {
                Id = id,
                Name = dto.Name,
                WorkshopId = dto.WorkshopId,
                Price = dto.Price
            };
        }
    }
}
