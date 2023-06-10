using Elearn.Models;
using FiorelloOneToMany.Models;

namespace FiorelloOneToMany.VıewModels
{
    public class HomeVM
    {
        public IEnumerable<Slider> Sliders { get; set; }
        public SliderInfo SliderInfo { get; set; }
        public IEnumerable<Blog> Blogs { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Expert> Experts { get; set; }
    }
}
