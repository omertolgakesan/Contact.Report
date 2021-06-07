using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Report.Common.Models
{
    public interface IDataModel<T>
    {
        IEnumerable<T> GetData();
    }
}
