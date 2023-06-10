using FiorelloOneToMany.Data;
using FiorelloOneToMany.Models;
using FiorelloOneToMany.VıewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FiorelloOneToMany.Controllers
{
    
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        public ProductController(AppDbContext context)
        {
            _context= context;
        }
        public async Task<IActionResult> Index(int? id)
        {
            if (id is null) return BadRequest();

            Product product = await _context.Products.Include(m => m.Discount).Include(m => m.Images).Include(m => m.Category).Where(m => !m.SoftDelete).FirstOrDefaultAsync(x => x.Id == id);

            if (product is null) return NotFound();

            ProductDetailVM model = new()
            {
                Id = product.Id,
                Name = product.Name,
                CategoryName = product.Category.Name,
                ActualPrice = product.Price,
                DiscountPrice = product.Price - (product.Price * product.Discount.Percent) / 100,
                Percent = product.Discount.Percent,
                Description = product.Description,
                Images = product.Images.ToList()
            };

            return View(model);
        }
    }
}
