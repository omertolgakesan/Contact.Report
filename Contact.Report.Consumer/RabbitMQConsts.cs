using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Report.Common
{
    public static class RabbitMQConsts
    {
        public static int MessagesTTL { get; set; } = 1000 * 60 * 60 * 2;

        public static ushort ParallelThreadsCount { get; set; } = 3;
        public enum RabbitMqConstsList
        {
            [Description("ContactReport")]
            ContactReport = 1
        }
    }
}
