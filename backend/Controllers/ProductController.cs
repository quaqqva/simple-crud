using backend.Database;
using backend.Database.Repositories;
using backend.Dtos;
using backend.Entities;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductController : BaseController<Product, ProductDto>
    {
        protected override Repository<Product> Repository { get; init; }

        public ProductController(TypographyContext context)
        {
            Repository = new ProductRepository(context);
        }

        protected override Product EntityFromDto(ProductDto dto, int? id = null)
        {
            return new Product()
            {
                Id = id,
                Name = dto.Name,
                WorkshopNumber = dto.WorkshopNumber,
                Price = dto.Price
            };
        }
    }
}
