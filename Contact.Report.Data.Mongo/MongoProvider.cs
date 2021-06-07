using Contact.Report.Common;
using Contact.Report.Common.Contract;
using Contact.Report.Common.Contracts;
using Contact.Report.Common.Enum;
using Contact.Report.Common.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Contact.Report.Data.Mongo
{
    public class MongoProvider : IMongoProvider
    {
        static readonly object LockMongoInstance = new object();
        private IOptions<MongoSetting> MongoSettings { get; set; }
        private IMongoDatabase _mongoDatabase { get; set; }
        private IMongoDatabase MongoDatabase
        {
            get
            {
                lock (LockMongoInstance)
                {
                    if (_mongoDatabase == null)
                    {
                        var MongoClient = new MongoClient(MongoSettings.Value.ConnectionString);
                        _mongoDatabase = MongoClient.GetDatabase(MongoSettings.Value.DatabaseName);
                    }
                }
                return _mongoDatabase;
            }
        }

        public MongoProvider(IOptions<MongoSetting> options)
        {
            MongoSettings = options;
        }

        public List<string> GetContactIdListByLocation(ContactReportProducerRequestModel request, MongoCollectionType mongoCollectionType)
        {
            var collection = MongoDatabase.GetCollection<InformationEntityModel>(mongoCollectionType.ToString("g"));
            return collection.AsQueryable().Where(x => x.ContactInformationType == ContactInformationType.Location && x.InformationDescription == request.Location).Select(x => x.UUID).ToList();
        }

        public List<ContactEntityModel> GetContactListListByUuid(List<string> contactInformations, MongoCollectionType collectionType)
        {
            var collection = MongoDatabase.GetCollection<ContactEntityModel>(collectionType.ToString("g"));
            return collection.AsQueryable().Where(x => contactInformations.Contains(x.UUID)).ToList();
        }

    }
}
