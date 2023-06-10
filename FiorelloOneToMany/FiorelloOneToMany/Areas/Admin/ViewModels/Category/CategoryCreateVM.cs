using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace FiorelloOneToMany.Areas.Admin.ViewModels.Category
{
    public class CategoryCreateVM
    {
        [Required]
      
        public string Name { get; set; }
    }
}
