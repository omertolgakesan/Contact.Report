using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Report.Common.Models
{
    public class ContactReportDto
    {
        public string Location { get; set; }
        public List<ReportDto> Contacts { get; set; }
    }
}
