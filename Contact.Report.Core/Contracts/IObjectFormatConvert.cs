using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Report.Common.Contract
{
    public interface IObjectFormatConvert
    {
        T JsonToObject<T>(string jsonString) where T : class, new();
        string ObjectToJson<T>(T entityObject) where T : class, new();
        T ParseObjectDataArray<T>(byte[] rawBytes) where T : class, new();
    }
}
