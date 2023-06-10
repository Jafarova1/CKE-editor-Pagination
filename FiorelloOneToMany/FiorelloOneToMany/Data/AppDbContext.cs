using Elearn.Models;
using FiorelloOneToMany.Models;
using Microsoft.EntityFrameworkCore;

namespace FiorelloOneToMany.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> option) : base(option)
        {

        }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<SliderInfo> SliderInfos { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Expert> Experts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Setting> Settings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<Slider>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<Category>().HasQueryFilter(m => !m.SoftDelete);


            modelBuilder.Entity<Setting>().HasData(
               new Setting
               {
                   Id= 1,
                   Key="HeaderLogo",
                   Value= "logo.png"
               },
               new Setting
               {
                   Id = 2,
                   Key = "Phone",
                    Value = "2344333"
               },
               new Setting
                {
                      Id = 3,
                      Key = "Email",
                      Value = "fıorello@gmail.com"
                 }


         );

            





        }
    }

    

}
