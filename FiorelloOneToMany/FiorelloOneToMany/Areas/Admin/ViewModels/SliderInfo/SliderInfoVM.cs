namespace FiorelloOneToMany.Areas.Admin.ViewModels.SliderInfo
{
    public class SliderInfoVM
    {
        public int Id { get; set; }
        public List<IFormFile> SignImages { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
  
    }
}
