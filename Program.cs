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
using BlogProject.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BlogProject.Helpers;
using System.Security.Claims;
using System.Net.Http.Headers;




var builder = WebApplication.CreateBuilder(args);

// JWT Ayarları
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

// JWT Authentication Ekleme
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "UserCookie"; // varsayılan cookie olacak
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddCookie("UserCookie", options =>
{
    options.LoginPath = "/Auth/Login";
    options.LogoutPath = "/Auth/Logout";
    options.AccessDeniedPath = "/Auth/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
})
.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.AddScoped<IBlogService, BlogService>();   //dependency injection
builder.Services.AddScoped<IBlogRepository, BlogRepository>();               //dependency injection
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();        //dependency injection
builder.Services.AddScoped<ICategoryService, CategoryService>();       //dependency injection
builder.Services.AddScoped<IUserRepository, UserRepository>();        //dependency injection
builder.Services.AddScoped<IUserService, UserService>();          //dependency injection
builder.Services.AddScoped<JwtTokenHelper>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateCommentRequestValidator>();
builder.Services.AddHttpClient<GeminiService>();



builder.Services.AddControllers()
    .AddJsonOptions(x =>
        x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);

builder.Services.AddDbContext<BlogContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllersWithViews()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<RegisterRequestValidator>());


var app = builder.Build();

DataSeeder.Seed(app);

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.MapControllers();
app.UseAuthentication(); // JWT + Cookie auth
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();