using LibraryApp.DAL.Entities;
using LibraryApp.Interfaces.Base.Repositories;
using LibraryApp.WebAPIClients.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.ConsoleUI
{
    class Program
    {
        private static IHost _hosting;

        public static IHost Hosting => _hosting ??= CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

        public static IServiceProvider Services => Hosting.Services;

        private static IHostBuilder CreateHostBuilder(string[] args) => Host
            .CreateDefaultBuilder(args)
            .ConfigureServices(ConfigureServices);

        private static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            services.AddHttpClient<IRepository<DataSource>, WebRepository<DataSource>>(
                client =>
                {
                    client.BaseAddress = new Uri($"{host.Configuration["WebAPI"]}/api/DataSources/"); // в конце адреса / обязательна
                }
                );
        }

        static async Task Main(string[] args)
        {
            using var host = Hosting;
            await host.StartAsync();

            var data_sources = Services.GetRequiredService<IRepository<DataSource>>();

            var start_count = await data_sources.GetCount();

            Console.WriteLine();
            Console.WriteLine(">>> Start count elements: {0}", start_count);
            Console.WriteLine();

            var sources = await data_sources.Get(3, 5);
            foreach (var item in sources)
            {
                Console.WriteLine($"{item.Id}: {item.Name} - {item.Description}");
            }

            var edited_item = await data_sources.Update(
                    new DataSource
                    {
                        Id = 6,
                        Name = $"Edited source at {DateTime.Now:HH-mm-ss}",
                        Description = "Edited description"
                    }
                );

            var end_count = await data_sources.GetCount();
            Console.WriteLine();
            Console.WriteLine(">>> В конце репозиторий элементов стало {0}", end_count);
            Console.WriteLine();

            var last_5_sources = await data_sources.Get(end_count - 5, 5);

            Console.WriteLine();
            foreach (var item in last_5_sources)
            {
                Console.WriteLine($">>> {item.Id}: {item.Name} - {item.Description}");
            }
            Console.WriteLine();

            var first_items = (await data_sources.Get(0, 2)).ToArray();

            var delete_item0 = await data_sources.DeleteById(first_items[0].Id);
            var delete_item1 = await data_sources.Delete(first_items[1]);

            Console.WriteLine();
            Console.WriteLine(">>> Completed");
            Console.ReadKey();

            await host.StopAsync();
        }
    }
}
