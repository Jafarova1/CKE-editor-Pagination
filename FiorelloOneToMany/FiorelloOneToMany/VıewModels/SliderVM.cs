using Elearn.Models;
using FiorelloOneToMany.Models;

namespace FiorelloOneToMany.VıewModels
{
    public class SliderVM
    {
        public IEnumerable<Slider> Sliders { get; set; }
        public SliderInfo SliderInfo { get; set; }
    }
}
