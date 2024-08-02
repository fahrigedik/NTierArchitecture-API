using Autofac;
using NLayerApp.Core.Repositories;
using NLayerApp.Core.Services;
using NLayerApp.Core.UnitOfWorks;
using NLayerApp.Repository.Context;
using NLayerApp.Repository.Repositories;
using NLayerApp.Repository.UnitOfWork;
using NLayerApp.Service.Mapping;
using NLayerApp.Service.Services;
using System.Reflection;

namespace NLayerApp.API.Module
{
    public class RepoServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(ServiceWithDto<,>)).As(typeof(IServiceWithDto<,>))
             .AsImplementedInterfaces()
             .InstancePerLifetimeScope();

           

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterType<ProductServiceWithDto>().As<IProductServiceWithDto>().AsImplementedInterfaces().InstancePerLifetimeScope();




            var apiAssembly = Assembly.GetExecutingAssembly();
        
            var repoAssembly = Assembly.GetAssembly(typeof(AppDbContext));
            var serviceAssemblye = Assembly.GetAssembly(typeof(MapProfile));


            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssemblye).Where(x=>x.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope(); //AddScoped


            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssemblye).Where(x => x.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope(); //AddScoped
        }

    }

}
