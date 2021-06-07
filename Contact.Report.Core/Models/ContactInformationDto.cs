using Contact.Report.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Report.Common.Models
{
    public class ContactInformationDto
    {
        public ContactInformationType ContactInformationType { get; set; }
        public string InformationDescription { get; set; }
    }
}
