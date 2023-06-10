using Microsoft.Build.Framework;

namespace FiorelloOneToMany.Areas.Admin.ViewModels.Category
{
    public class CategoryEditVM
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
