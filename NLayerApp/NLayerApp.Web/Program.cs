using Autofac.Extensions.DependencyInjection;
using Autofac;
using NLayerApp.Web.Module;
using Microsoft.EntityFrameworkCore;
using NLayerApp.Repository.Context;
using System.Reflection;
using NLayerApp.Service.Mapping;
using FluentValidation.AspNetCore;
using NLayerApp.Service.Validation;
using NLayerApp.Web.Filter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<ProductDtoValidator>());

builder.Services.AddAutoMapper(typeof(MapProfile));

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), options =>
    {
        //AppDbContext NLayerApp.API'de de�il, NLayerApp.Repository'de bunun i�in
        //tip g�venlikli bir �ekilde ekledik.
        options.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
    });
});

builder.Services.AddScoped(typeof(NotFoundFilter<>));

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(contanerBuilder => contanerBuilder.RegisterModule(new RepoServiceModule()));

var app = builder.Build();

    app.UseExceptionHandler("/Home/Error");
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
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
