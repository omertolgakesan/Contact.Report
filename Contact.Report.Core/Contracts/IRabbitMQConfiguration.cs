using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Report.Common.Contract
{
    public interface IRabbitMQConfiguration
    {
        string HostName { get; }
        string UserName { get; }
        string Password { get; }
    }
}
