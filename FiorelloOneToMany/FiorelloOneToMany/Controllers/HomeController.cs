using FiorelloOneToMany.Data;
using FiorelloOneToMany.Models;
using FiorelloOneToMany.Services.Interfaces;
using FiorelloOneToMany.VıewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FiorelloOneToMany.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IProductService _productService;
        public HomeController(AppDbContext context, IProductService productService)
        {
            _context = context;
            _productService = productService;
        }


        public async Task<IActionResult> Index()
        {
            IEnumerable<Blog> blogs = await _context.Blogs.Where(m => !m.SoftDelete).OrderByDescending(m => m.Id).Take(3).ToListAsync();

            IEnumerable<Category> categories = await _context.Categories.Where(m => !m.SoftDelete).ToListAsync();

            IEnumerable<Product> products = await _productService.GetAllAsync();

            IEnumerable<Expert> experts = await _context.Experts.Include(m => m.Position).Where(m => !m.SoftDelete).ToListAsync();

            HomeVM model = new()
            {
                Blogs = blogs,
                Categories = categories,
                Products = products,
                Experts = experts
            };

            return View(model);



        }
    }
}