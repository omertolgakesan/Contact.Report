using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Report.Common.Models
{
    public class ReportDto
    {
        public string UUID { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Firm { get; set; }
        public List<ContactInformationDto> Informations { get; set; }
    }
}
