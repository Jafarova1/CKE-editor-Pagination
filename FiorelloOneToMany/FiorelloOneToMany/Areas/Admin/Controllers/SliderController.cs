
using FiorelloOneToMany.Areas.Admin.ViewModels.Slider;
using FiorelloOneToMany.Models;
using FiorelloOneToMany.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Elearn.Models;
using FiorelloOneToMany.Helpers;
using System.IO;
using FiorelloOneToMany.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Elearn.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class SliderController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly ISliderService _sliderService;

        public SliderController(AppDbContext context, IWebHostEnvironment env,ISliderService sliderService)
        {
            _context = context;
            _env = env;
            _sliderService = sliderService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View( await _sliderService.GetAllMappedDatasAsync());
        }
    


        [HttpGet]

        public async Task<IActionResult> Detail(int? id)
        {

            if (id is null) return BadRequest();

            Slider dbSlider = await _sliderService.GetByIdAsync((int)id);

            if (dbSlider is null) return NotFound();

       
            return View(_sliderService.GetMappedData(dbSlider));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(SliderCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            foreach (var item in request.Images)
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

         
            
                await _sliderService.CreateAsync(request.Images);

                 return RedirectToAction(nameof(Index));
        }

        [HttpPost]

        public async Task<IActionResult> Delete(int id)
        {
            await _sliderService.DeleteAsync(id);
        
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult>Edit(int? id)
        {

            if (id is null) return BadRequest();

            Slider dbslider = await _sliderService.GetByIdAsync((int)id);

            if (dbslider is null) return NotFound();

            return View(new SliderEditVM { Image= dbslider.Image });


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id,SliderEditVM request)
        {

            if (id is null) return BadRequest();

            Slider dbslider = await _sliderService.GetByIdAsync((int)id);

            if (dbslider is null) return NotFound();

            if (request.NewImage is null) return RedirectToAction(nameof(Index));

            if (!request.NewImage.CheckFileType("image/"))
            {
                ModelState.AddModelError("NewImage", "plaese select only image file");
                request.Image = dbslider.Image;
                return View(request);
            }

            if (request.NewImage.CheckFileSize(200))
            {
                ModelState.AddModelError("NewImage", "image size must be max 200 kb");
                request.Image = dbslider.Image;
                return View(request);
            }



            await _sliderService.EditAsync(dbslider,request.NewImage);

            return RedirectToAction(nameof(Index));


        }


        [HttpPost]
        public async Task<IActionResult> ChangeStatus(int? id)
        {
            if (id is null) return BadRequest();

            Slider slider = await _sliderService.GetByIdAsync((int)id);
            if(slider is null) return NotFound();


            return Ok(await _sliderService.ChangeStatusAsync(slider));
        }
    }


}

