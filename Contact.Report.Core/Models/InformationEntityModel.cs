using Contact.Report.Common.Enum;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Report.Common.Models
{
    [Serializable]
    public class InformationEntityModel
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string UUID { get; set; }
        public ContactInformationType ContactInformationType { get; set; }
        public string InformationDescription { get; set; }
    }
}
