using Contact.Report.Common.Contract;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Report.Common.Services
{
    public class RabbitMQConfiguration : IRabbitMQConfiguration
    {
        public IConfiguration Configuration { get; }
        public RabbitMQConfiguration(IConfiguration configuration) => Configuration = configuration;
        public string HostName => Configuration.GetSection("RabbitMQConfiguration:HostName").Value;
        public string UserName => Configuration.GetSection("RabbitMQConfiguration:UserName").Value;
        public string Password => Configuration.GetSection("RabbitMQConfiguration:Password").Value;
    }
}
