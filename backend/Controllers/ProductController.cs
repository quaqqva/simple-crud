using backend.Models;
using backend.Database;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : BaseController<Product, ProductViewModel>
    {
        public ProductController(TypographyContext context): base(context, (context) => context.Products) {}

        protected override Product EntityFromViewModel(ProductViewModel viewModel, int? id = null)
        {
            return new Product() {
                Code = id,
                Name = viewModel.Name,
                WorkshopNumber = viewModel.WorkshopNumber,
            };
        }
    }
}