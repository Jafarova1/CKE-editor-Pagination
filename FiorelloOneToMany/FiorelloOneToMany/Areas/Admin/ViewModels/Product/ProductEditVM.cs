namespace FiorelloOneToMany.Areas.Admin.ViewModels.Product
{
    public class ProductEditVM
    {
        public string Image { get; set; }
        public IFormFile NewImage { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
    }
}
