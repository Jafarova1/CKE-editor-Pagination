namespace FiorelloOneToMany.VıewModels
{
    public class BasketDetailVm
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
