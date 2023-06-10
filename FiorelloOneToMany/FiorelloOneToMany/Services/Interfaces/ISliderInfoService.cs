using Elearn.Models;
using FiorelloOneToMany.Models;

namespace FiorelloOneToMany.Services.Interfaces
{
    public interface ISliderInfoService
    {
        Task<SliderInfo> GetByIdAsync(int id);
        SliderInfo GetMappedData(SliderInfo sliderInfo);
        Task CreateAsync(List<IFormFile> images);
        Task DeleteAsync(int id);

    }
}
