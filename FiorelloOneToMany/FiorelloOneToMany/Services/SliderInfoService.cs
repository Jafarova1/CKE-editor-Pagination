using Elearn.Models;
using FiorelloOneToMany.Data;
using FiorelloOneToMany.Helpers;
using FiorelloOneToMany.Models;
using FiorelloOneToMany.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FiorelloOneToMany.Services
{
    public class SliderInfoService : ISliderInfoService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public SliderInfoService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task CreateAsync(List<IFormFile> images)
        {
            foreach (var item in images)
            {
                string filename = Guid.NewGuid().ToString() + "_" + item.FileName;

                await item.SaveFileAsync(filename, _env.WebRootPath, "img");


                SliderInfo sliderInfo= new()
                {
                  SignImage=filename
                };


                await _context.SliderInfos.AddAsync(sliderInfo);


                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            SliderInfo sliderInfo = await GetByIdAsync(id);

            _context.SliderInfos.Remove(sliderInfo);

            await _context.SaveChangesAsync();

            string path = Path.Combine(_env.WebRootPath, "img", sliderInfo.SignImage);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public async Task<SliderInfo> GetByIdAsync(int id)
        {
            return await _context.SliderInfos.FirstOrDefaultAsync(m=>m.Id==id);
            
        }

        public SliderInfo GetMappedData(SliderInfo sliderInfo)
        {
            SliderInfo info = new()
            {
                Description = sliderInfo.Description,
                Title = sliderInfo.Title,
                SignImage= sliderInfo.SignImage,

            };
            return info;
        }
    }
}
