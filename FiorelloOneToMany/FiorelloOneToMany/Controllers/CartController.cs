using FiorelloOneToMany.Data;
using FiorelloOneToMany.Models;
using FiorelloOneToMany.Services.Interfaces;
using FiorelloOneToMany.VıewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Fiorello_backend.Controllers
{
    public class CartController : Controller
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IBasketService _basketService;
        private readonly IProductService _productService;

        public CartController(IProductService productService,
                              IHttpContextAccessor accessor,
                              IBasketService basketService)
        {
            _accessor = accessor;
            _basketService = basketService;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            List<BasketDetailVm> basketList = new();

            if (_accessor.HttpContext.Request.Cookies["basket"] != null)
            {
                List<BasketVM> basketDatas = JsonConvert.DeserializeObject<List<BasketVM>>(_accessor.HttpContext.Request.Cookies["basket"]);

                foreach (var item in basketDatas)
                {
                    var dbProduct = await _productService.GetByIdWithImagesAsync(item.Id);

                    if(dbProduct != null)
                    {
                        BasketDetailVm basketDetail = new()
                        {
                            Id = dbProduct.Id,
                            Name = dbProduct.Name,
                            //Image = dbProduct.Image.Where(m => m.IsMain).FirstOrDefault().Image,
                            Count = item.Count,
                            //Price = dbProduct.Price,
                            //TotalPrice = item.Count * dbProduct.Price
                        };

                        basketList.Add(basketDetail);
                    }

                }
            }

            return View(basketList);
        }


        [HttpPost]
        public async Task<IActionResult> AddBasket(int? id)
        {
            if (id is null) return BadRequest();

            Product product = await _productService.GetByIdAsync(id);

            if (product is null) return NotFound();

            List<BasketVM> basket = _basketService.GetAll();

            _basketService.AddProduct(basket, product);

            return Ok(basket.Sum(m=>m.Count));
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteProductFromBasket(int? id)
        {
            return Ok(await _basketService.DeleteProduct(id));
        }
    }
}
