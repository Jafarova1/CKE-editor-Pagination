using FiorelloOneToMany.Data;
using FiorelloOneToMany.Models;
using FiorelloOneToMany.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FiorelloOneToMany.Services
{
    public class CategoryService:ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAll()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}
