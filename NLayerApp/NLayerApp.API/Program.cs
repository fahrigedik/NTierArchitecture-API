using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLayerApp.API.Filters;
using NLayerApp.API.Middleware;
using NLayerApp.Core.Repositories;
using NLayerApp.Core.Services;
using NLayerApp.Core.UnitOfWorks;
using NLayerApp.Repository.Context;
using NLayerApp.Repository.Repositories;
using NLayerApp.Repository.UnitOfWork;
using NLayerApp.Service.Mapping;
using NLayerApp.Service.Services;
using NLayerApp.Service.Validation;
using System.Numerics;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options => { options.Filters.Add(new ValidateFilterAttribute()); });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

//ModelState Filtresini baskýlamak için, kendi ValidationFilter'ýmý kullanmak için Api'nin default davranýþýný Filter baskýlanmasýný etkinleþtirdim.

builder.Services.Configure<ApiBehaviorOptions>(opt =>
{
    opt.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<ProductDtoValidator>());



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//unitofwork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//repository
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository,CategoryRepository>();

//services
builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

//automapper
builder.Services.AddAutoMapper(typeof(MapProfile));


builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), options =>
    {
        //AppDbContext NLayerApp.API'de deðil, NLayerApp.Repository'de bunun için
        //tip güvenlikli bir þekilde ekledik.
        options.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UserCustomException();

app.UseAuthorization();

app.MapControllers();

app.Run();
