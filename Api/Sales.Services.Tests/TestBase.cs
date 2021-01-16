using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sales.Domain.Data;
using System;

namespace Sales.Services.Tests
{
    public class TestBase<T> where T : class
    {
        public ServiceCollection Services { get; set; }
        public ServiceProvider ServiceProvider { get; set; }
        public T Service { get; set; }
        public SalesDbContext Context { get; set; }


        public TestBase()
        {
            Services = new ServiceCollection();
            Services.AddLogging()
                .AddDbContext<SalesDbContext>(options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()))
                .AddTransient<T>();

            ServiceProvider = Services.BuildServiceProvider();

            Service = ServiceProvider.GetService<T>();
            Context = ServiceProvider.GetService<SalesDbContext>();
        }
    }
}
