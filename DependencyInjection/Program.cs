using Microsoft.Extensions.DependencyInjection;
using System;

namespace DependencyInjection
{
    class Program
    {
        static void Main(string[] args)
        {
            var collection = new ServiceCollection();   // Must have this first to instantiate collection
            collection.AddSingleton<IDemoService, DemoService>();   // add a class with interface
            collection.AddScoped<IDemoService1, DemoService1>();
            collection.AddScoped<IParse, Parse>();  // make sure chained dependencies are added in reverse order

            IServiceProvider serviceProvider = collection.BuildServiceProvider();       // last call to build collection

            //var service = serviceProvider.GetService<IDemoService>();
            //var service1 = serviceProvider.GetService<IDemoService1>();
            //Console.WriteLine(service.DooWah());
            //Console.WriteLine(service1.DooWahkaDo());


            var service = serviceProvider.GetService<IParse>(); // create service provider reference

            service.DoParse();  // call the method!

            if (serviceProvider is IDisposable)
            {
                ((IDisposable)serviceProvider).Dispose();
            }
        }
    }
}