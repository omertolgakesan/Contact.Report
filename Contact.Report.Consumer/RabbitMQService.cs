using Contact.Report.Common.Contract;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Contact.Report.Common.Services
{
    public class RabbitMQService : IRabbitMQService
    {
        private readonly IRabbitMQConfiguration _rabbitMQConfiguration;
        public RabbitMQService(IRabbitMQConfiguration rabbitMQConfiguration)
        {
            _rabbitMQConfiguration = rabbitMQConfiguration ?? throw new ArgumentNullException(nameof(rabbitMQConfiguration));
        }
        public IConnection GetConnection()
        {
            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = _rabbitMQConfiguration.HostName,
                    UserName = _rabbitMQConfiguration.UserName,
                    Password = _rabbitMQConfiguration.Password
                };

                factory.AutomaticRecoveryEnabled = true;
                factory.NetworkRecoveryInterval = TimeSpan.FromSeconds(10);
                factory.TopologyRecoveryEnabled = false;

                return factory.CreateConnection();
            }
            catch (BrokerUnreachableException)
            {
                Thread.Sleep(5000);
                return GetConnection();
            }
        }

        public IModel GetModel(IConnection connection)
        {
            return connection.CreateModel();
        }
    }
}
