﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Report.Common.Models
{
    [Serializable]
    public class ContactEntityModel
    {
        public ContactEntityModel()
        {
            UUID = Guid.NewGuid().ToString("N");
        }

        [BsonId]
        public ObjectId _id { get; set; }
        public string UUID { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Firm { get; set; }
    }
}
