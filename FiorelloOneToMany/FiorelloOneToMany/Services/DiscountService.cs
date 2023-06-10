using FiorelloOneToMany.Data;
using FiorelloOneToMany.Models;
using FiorelloOneToMany.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FiorelloOneToMany.Services
{
    public class DiscountService:IDiscountService
    {
        private readonly AppDbContext _context;

        public DiscountService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Discount>> GetAll()
        {
            return await _context.Discounts.ToListAsync();
        }
    }
}
