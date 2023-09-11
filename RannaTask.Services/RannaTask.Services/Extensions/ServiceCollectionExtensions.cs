using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RannaTask.Data.Abstract;
using RannaTask.Data.Concrete.EntityFramework;
using RannaTask.Data.Concrete.EntityFramework.Contexts;
using RannaTask.Services.Abstract;
using RannaTask.Services.Concrete;

namespace RannaTask.Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection LoadMyServices(this IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection.AddDbContext<RannaTaskContext>(options => options.UseSqlServer(connectionString));
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddScoped<IProductService, ProductManager>();
            return serviceCollection;
        }
    }
}
