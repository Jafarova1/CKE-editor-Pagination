using FiorelloOneToMany.Data;
using FiorelloOneToMany.Models;
using FiorelloOneToMany.Responses;
using FiorelloOneToMany.Services.Interfaces;
using FiorelloOneToMany.VıewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FiorelloOneToMany.Services
{
    public class BasketService:IBasketService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _accessor;
        private readonly IProductService _productService;
            
        public BasketService(AppDbContext context, IHttpContextAccessor accessor, IProductService productService)
        {
            _context = context;
            _accessor = accessor;
            _productService = productService;
        }

        public void AddProduct(List<BasketVM> basket, Product product)
        {
            BasketVM existProduct = basket.FirstOrDefault(m => m.Id == product.Id);

            if (existProduct is null)
            {
                basket.Add(new BasketVM
                {
                    Id = product.Id,
                    Count = 1
                });
            }
            else
            {
                existProduct.Count++;
            }

            _accessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));
        }



        public async Task<BasketDeleteResponse> DeleteProduct(int? id)
        {
            List<BasketVM> basketDatas = JsonConvert.DeserializeObject<List<BasketVM>>(_accessor.HttpContext.Request.Cookies["basket"]);

            var data = basketDatas.FirstOrDefault(m => m.Id == id);

            basketDatas.Remove(data);

            _accessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basketDatas));

            decimal total = 0;

            foreach (var basketData in basketDatas)
            {
                Product dbProduct = await _productService.GetByIdAsync(basketData.Id);
                total += (dbProduct.Price * basketData.Count);
            }

            int count = basketDatas.Sum(m => m.Count);

            return new BasketDeleteResponse { Count = count, Total = total };
        }

        public List<BasketVM> GetAll()
        {
            List<BasketVM> basket;

            if (_accessor.HttpContext.Request.Cookies["basket"] != null)
            {
                basket = JsonConvert.DeserializeObject<List<BasketVM>>(_accessor.HttpContext.Request.Cookies["basket"]);
            }
            else
            {
                basket = new List<BasketVM>();
            }

            return basket;
        }

        public int GetCount()
        {
            List<BasketVM> basket;

            if (_accessor.HttpContext.Request.Cookies["basket"] != null)
            {
                basket = JsonConvert.DeserializeObject<List<BasketVM>>(_accessor.HttpContext.Request.Cookies["basket"]);
            }
            else
            {
                basket = new List<BasketVM>();
            }

            return basket.Sum(m => m.Count);
        }
    }
}
