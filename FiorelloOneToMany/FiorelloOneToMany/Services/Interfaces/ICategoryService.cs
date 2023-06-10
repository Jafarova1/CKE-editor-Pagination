using FiorelloOneToMany.Models;

namespace FiorelloOneToMany.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAll();
    }
}
