namespace FiorelloOneToMany.Models
{
    public class Employee:BaseEntity
    {
        public string FullName { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public decimal Salary { get; set; }
    }
}
