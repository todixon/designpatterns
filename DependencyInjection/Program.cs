using Microsoft.Extensions.DependencyInjection;
using System;

namespace DependencyInjection
{
    class Program
    {
        static void Main(string[] args)
        {
            var collection = new ServiceCollection();
            collection.AddSingleton<IDemoService, DemoService>();
            collection.AddScoped<IDemoService1, DemoService1>();
            collection.AddScoped<IParse, Parse>();

            IServiceProvider serviceProvider = collection.BuildServiceProvider();

            //var service = serviceProvider.GetService<IDemoService>();
            //var service1 = serviceProvider.GetService<IDemoService1>();
            //Console.WriteLine(service.DooWah());
            //Console.WriteLine(service1.DooWahkaDo());


            var service = serviceProvider.GetService<IParse>();

            service.DoParse();

            if (serviceProvider is IDisposable)
            {
                ((IDisposable)serviceProvider).Dispose();
            }
        }
    }
}