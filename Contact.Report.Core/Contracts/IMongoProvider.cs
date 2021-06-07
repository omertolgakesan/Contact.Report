using Contact.Report.Common.Enum;
using Contact.Report.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Report.Common.Contracts
{
    public interface IMongoProvider
    {
        List<string> GetContactIdListByLocation(ContactReportProducerRequestModel request, MongoCollectionType mongoCollectionType);
        List<ContactEntityModel> GetContactListListByUuid(List<string> contactIdList, MongoCollectionType mongoCollectionType);
        List<InformationEntityModel> GetContactInformationList(string uuid, MongoCollectionType ınformation);
    }
}
