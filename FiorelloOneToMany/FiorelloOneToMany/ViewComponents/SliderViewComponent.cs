using Elearn.Models;
using FiorelloOneToMany.Areas.Admin.ViewModels.Slider;
using FiorelloOneToMany.Data;
using FiorelloOneToMany.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FiorelloOneToMany.ViewComponents
{
    public class SliderViewComponent:ViewComponent
    {
        private readonly AppDbContext _context;
        private readonly ISliderService _sliderService;
        public SliderViewComponent(AppDbContext context,ISliderService sliderService)
        {
            _context= context;
            _sliderService= sliderService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<Slider> sliders = await _sliderService.GetAllByStatusAsync
                ();

            //SliderInfo sliderInfo = await _context.SliderInfos.Where(m=>!m.SoftDelete).FirstOrDefaultAsync();   
            SliderVM model = new()
            {
                //Sliders = sliders
            //SliderInfo=sliderInfo
             };

            return await Task.FromResult(View(model));
        }

    }
}
