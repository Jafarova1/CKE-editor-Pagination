using FiorelloOneToMany.Areas.Admin.ViewModels.SliderInfo;
using FiorelloOneToMany.Data;
using FiorelloOneToMany.Helpers;
using FiorelloOneToMany.Models;
using FiorelloOneToMany.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FiorelloOneToMany.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderInfoController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ISliderInfoService _sliderInfoService;
        public SliderInfoController(AppDbContext context,ISliderInfoService sliderInfoService )
        {
            _context = context;
            _sliderInfoService = sliderInfoService;

     
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            SliderInfo info = await _sliderInfoService.GetByIdAsync((int)id);
            if(info is null) return NotFound();

            return(View(_sliderInfoService.GetMappedData(info)));

        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SliderInfoCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            foreach (var item in request.SignImages)
            {
                if (!item.CheckFileType("image/"))
                {
                    ModelState.AddModelError("image", "plaese select only image file");
                    return View();
                }
                if (item.CheckFileSize(200))
                {
                    ModelState.AddModelError("image", "image size must be max 200 kb");
                    return View();
                }
            }

            await _sliderInfoService.CreateAsync(request.SignImages);
            return RedirectToAction("Index");

        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _sliderInfoService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        
    }
}
