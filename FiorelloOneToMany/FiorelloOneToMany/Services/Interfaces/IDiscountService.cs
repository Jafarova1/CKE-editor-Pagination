using FiorelloOneToMany.Models;

namespace FiorelloOneToMany.Services.Interfaces
{
    public interface IDiscountService
    {
        Task<List<Discount>> GetAll();
    }
}
