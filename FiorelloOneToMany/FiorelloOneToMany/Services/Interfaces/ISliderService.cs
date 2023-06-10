using Elearn.Models;
using FiorelloOneToMany.Areas.Admin.ViewModels.Slider;

namespace FiorelloOneToMany.Services.Interfaces
{
    public interface ISliderService
    {
        Task<List<Slider>> GetAllAsync();
        Task<Slider> GetByIdAsync(int id);
        Task<List<SliderVM>> GetAllMappedDatasAsync();
        SliderDetailVM GetMappedData(Slider slider);
        Task CreateAsync(List<IFormFile> images);
        Task DeleteAsync(int id);
        Task EditAsync(Slider slider, IFormFile newImage);
        Task<List<Slider>> GetAllByStatusAsync();
        Task<int> GetCountAsync();
        Task<bool> ChangeStatusAsync(Slider slider);
    }
}
