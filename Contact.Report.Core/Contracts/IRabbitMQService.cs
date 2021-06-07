using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Report.Common.Contract
{
    public interface IRabbitMQService
    {
        IConnection GetConnection();
        IModel GetModel(IConnection connection);
    }
}
