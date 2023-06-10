using Fiorello_backend.Services;
using FiorelloOneToMany.Data;
using FiorelloOneToMany.Services;
using FiorelloOneToMany.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<ILayoutService, LayoutService>();
builder.Services.AddScoped<IBasketService, BasketService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ISliderService, SliderService>();
builder.Services.AddScoped<ISettingService, SettingService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
//builder.Services.AddScoped<IDiscountService, DiscountService>();


builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
