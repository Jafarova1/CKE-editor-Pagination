using FiorelloOneToMany.Models;
using FiorelloOneToMany.Responses;
using FiorelloOneToMany.VıewModels;

namespace FiorelloOneToMany.Services.Interfaces
{
    public interface IBasketService
    {
        List<BasketVM> GetAll();
        void AddProduct(List<BasketVM> basket, Product product);
        Task<BasketDeleteResponse> DeleteProduct(int? id);
        int GetCount();
    }
}
