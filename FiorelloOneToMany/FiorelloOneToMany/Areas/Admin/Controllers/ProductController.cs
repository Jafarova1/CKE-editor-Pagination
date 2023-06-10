using FiorelloOneToMany.Areas.Admin.ViewModels.Product;
using FiorelloOneToMany.Helpers;
using FiorelloOneToMany.Models;
using FiorelloOneToMany.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FiorelloOneToMany.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ISettingService _settingService;
        private readonly IDiscountService _discountService;
        private readonly ICategoryService _categoryService;


        public ProductController(IProductService productService,
                                 ISettingService settingService,
                                 IDiscountService discountService,
                                 ICategoryService categoryService)
        {
            _productService = productService;
            _settingService = settingService;
            _discountService = discountService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            var settingDatas = _settingService.GetAll();
            int take = int.Parse(settingDatas["AdminProductPaginateTake"]);
            var paginatedDatas = await _productService.GetPaginatedDatasAsync(page, take);

            int pageCount = await GetCountAsync(take);

            if (page > pageCount)
            {
                return NotFound();
            }

            List<ProductVM> mappedDatas = _productService.GetMappedDatas(paginatedDatas);

            Paginate<ProductVM> result = new(mappedDatas, page, pageCount);

            return View(result);
        }

        private async Task<int> GetCountAsync(int take)
        {
            int count = await _productService.GetCountAsync();

            decimal result = Math.Ceiling((decimal)count / take);

            return (int)result;

        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            Product product = await _productService.GetWithIncludesAsync((int)id);

            if (product is null) return NotFound();

            return View(_productService.GetMappedData(product));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await GetCategoriesAndDiscounts();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateVM request)
        {
            await GetCategoriesAndDiscounts();

            if (!ModelState.IsValid)
            {
                return View();
            }

            foreach (var item in request.Images)
            {
                if (!item.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Image", "Please select only image file");
                    return View();
                }


                if (item.CheckFileSize(200))
                {
                    ModelState.AddModelError("Image", "Image size must be max 200 KB");
                    return View();
                }
            }

            await _productService.CreateAsync(request);
            return RedirectToAction(nameof(Index));
        }


        private async Task GetCategoriesAndDiscounts()
        {
            ViewBag.categories = await GetCategories();
            ViewBag.discounts = await GetDiscounts();
        }




        private async Task<SelectList> GetCategories()
        {
            List<Category> categories = await _categoryService.GetAll();
            return new SelectList(categories, "Id", "Name");
        }


        private async Task<SelectList> GetDiscounts()
        {
            List<Discount> discounts = await _discountService.GetAll();
            return new SelectList(discounts, "Id", "Name");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            await _productService.DeleteAsync((int)id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null) return BadRequest();
            Product dbProduct = await _productService.GetByIdAsync(id);
            if (dbProduct != null) return NotFound();
            return View(new ProductEditVM { Image = dbProduct.Images });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, ProductEditVM request)
        {
            if(id == null) return BadRequest();
            Product dbProduct = await _productService.GetByIdAsync(id);
            if (dbProduct != null) return NotFound();
            if (request.NewImage is null) return RedirectToAction(nameof(Index));
            if (!request.NewImage.CheckFileType("image/"))
            {
                ModelState.AddModelError("NewImage", "plaese select only image file");
                request.Image = dbProduct.Images;
                return View(request);
            }

            if (request.NewImage.CheckFileSize(200))
            {
                ModelState.AddModelError("NewImage", "image size must be max 200 kb");
                request.Image = dbProduct.Images;
                return View(request);
            }
            await _productService.EditAsync(dbProduct, request.NewImage);

            return RedirectToAction(nameof(Index));

        }
    }
}

