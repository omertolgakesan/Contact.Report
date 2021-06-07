using Contact.Report.Common;
using Contact.Report.Common.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Report.Data.Mongo
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection RegisterMongoConsumers(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoSetting>(configuration.GetSection("MongoSettings"));
            services.AddSingleton<IMongoProvider,MongoProvider>();

            DIServiceProvider.ServiceProvider = services.BuildServiceProvider();
            return services;
        }
    }
}
