using Contact.Report.Common;
using Contact.Report.Common.Contract;
using Contact.Report.Common.Contracts;
using Contact.Report.Common.Models;
using Contact.Report.Common.Services;
using Contact.Report.Data.Mongo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;
using Contact.Report.Data.Mongo;
using Contact.Report.Consumer;

namespace Contact.Report
{
    class Program
    {
        static async Task Main()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var configuration = builder.Build();

            var serviceProvider = new ServiceCollection()
                .RegisterMongoConsumers(configuration)
                .AddSingleton<IConfiguration>(configuration)
                .AddSingleton<IRabbitMQConfiguration, RabbitMQConfiguration>()
                .AddSingleton<IRabbitMQService, RabbitMQService>()
                .AddSingleton<IObjectFormatConvert, ObjectConvertFormatManager>()
                .AddSingleton<IConsumerService, ConsumerManager>()
                .BuildServiceProvider();

            var consumerService = serviceProvider.GetService<IConsumerService>();
            Console.WriteLine("consumerService alındı.");
            Console.WriteLine($"consumerService.Start() başladı: {DateTime.Now.ToShortTimeString()}");
            await consumerService.Start();
            Console.WriteLine($"consumerService.Start() bitti:  {DateTime.Now.ToShortTimeString()}");
        }
    }
}
