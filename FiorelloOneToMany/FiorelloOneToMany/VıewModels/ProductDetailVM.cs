using FiorelloOneToMany.Models;

namespace FiorelloOneToMany.VıewModels
{
    public class ProductDetailVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public decimal ActualPrice { get; set; }
        public decimal DiscountPrice { get; set; }
        public string Description { get; set; }
        public byte Percent { get; set; }
        public List<ProductImage> Images { get; set; }
    }
}
