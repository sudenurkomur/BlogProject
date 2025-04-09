using BlogProject.Data;
using BlogProject.Data.BlogProject.Data;
using Microsoft.EntityFrameworkCore;
using BlogProject.Data.Seed;
using BlogProject.Repositories.Interfaces;
using BlogProject.Repositories.Implementations;
using BlogProject.Repositories.Interfaces.BlogProject.Repositories.Interfaces;
using BlogProject.Services.Interfaces;
using BlogProject.Services.Implementations;
using FluentValidation.AspNetCore;
using BlogProject.Validators.BlogProject.Validators;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<IBlogService, BlogService>();   // dependency injection

builder.Services.AddScoped<IBlogRepository, BlogRepository>();  // dependency injection

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();        // dependency injection

builder.Services.AddScoped<ICategoryService, CategoryService>();                // dependency injection

builder.Services.AddScoped<IUserRepository, UserRepository>();               // dependency injection

builder.Services.AddScoped<IUserService, UserService>();              // dependency injection

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BlogContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllersWithViews();

builder.Services.AddControllersWithViews()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<RegisterValidator>());

var app = builder.Build();

DataSeeder.Seed(app);

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
