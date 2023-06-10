using Elearn.Models;
using FiorelloOneToMany.Areas.Admin.ViewModels.Product;
using FiorelloOneToMany.Models;

namespace FiorelloOneToMany.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<List<Product>> GetPaginatedDatasAsync(int page, int take);
        Task<Product> GetByIdAsync(int? id);
        Task<Product> GetByIdWithImagesAsync(int? id);
        List<ProductVM> GetMappedDatas(List<Product> products);
        Task<Product> GetWithIncludesAsync(int id);
        ProductDetailVM GetMappedData(Product product);
        Task<int> GetCountAsync();
        Task CreateAsync(ProductCreateVM model);

        Task DeleteAsync(int id);
        Task EditAsync(Product product, IFormFile newImage);

    }
}
