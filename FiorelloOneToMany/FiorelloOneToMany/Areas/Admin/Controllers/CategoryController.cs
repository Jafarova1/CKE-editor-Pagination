using FiorelloOneToMany.Areas.Admin.ViewModels.Category;
using FiorelloOneToMany.Data;
using FiorelloOneToMany.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace 
FiorelloOneToMany.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;
        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult>Index()
        {
            List<CategoryVM> list = new();
            var datas = await _context.Categories.Where(m=>m.SoftDelete==true).OrderByDescending(m=>m.Id).ToListAsync();

            foreach (var item in datas)
            {
                list.Add(new CategoryVM
                {
                    Id = item.Id,
                    Name = item.Name
                });

            }

            return View(list);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(CategoryCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Category newCategory = new()
            {
                Name=request.Name
            };

            await _context.Categories.AddAsync(newCategory);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();
            var existCategory = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);
            if (existCategory is null) return NotFound();

            CategoryEditVM model = new()
            {
                Id=existCategory.Id,
                Name = existCategory.Name
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int? id,CategoryEditVM request)
        {
            if (id is null) return BadRequest();
            if (!ModelState.IsValid)
            {
                return View();
            }

            var existCategory = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            if (existCategory is null) return NotFound();

            if (existCategory.Name==request.Name)
            {

                return RedirectToAction(nameof(Index));
            }
            Category category = new()
            {
                Id = request.Id,
                Name = request.Name
            };


            _context.Update(category);


            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
    

        public async Task<IActionResult> Delete(int id)
        {
            var existCategory = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            _context.Remove(existCategory);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> SoftDelete(int id)
        {
            var existCategory = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);


            existCategory.SoftDelete = true;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
