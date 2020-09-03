using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MongoDbExportCSV
{

    
        public class MongoDatabase
        {
            private string _connectionString;
            private MongoClient _dbClient;

            public MongoDatabase(string connectionString)
            {
                _connectionString = connectionString;
                _dbClient = new MongoClient(_connectionString);

            }

            public List<string> GetListOfDatabases()
            {
                var dbList = _dbClient.ListDatabases().ToList();
                var mongoDbFiltredList = new List<string>();

                foreach (var db in dbList)
                {
                    if (!new[] { "admin", "local", "config" }.Any(db["name"].AsString.Contains))
                    {
                        mongoDbFiltredList.Add(db["name"].AsString);
                    }
                }
                return mongoDbFiltredList;
            }

            public List<string> GetCollectionNameList(string databaseName)
            {
                List<string> CollectionList = new List<string>();
                var db = _dbClient.GetDatabase(databaseName);
                foreach (var collection in db.ListCollectionsAsync().Result.ToListAsync<BsonDocument>().Result)
                {
                    CollectionList.Add(collection["name"].ToString());
                }
                return CollectionList;
            }

            public HashSet<string> GetColumnKeys(string databaseName, string collectionName)
            {
                HashSet<string> fields = new HashSet<string>();
                var result = _dbClient.GetDatabase(databaseName).GetCollection<BsonDocument>(collectionName).Find(new BsonDocument()).Limit(50).ToListAsync().Result;
                return MongoDbGatherer.ProcessTreeNoEmbededDocuments(fields, result, "");
            }

        }
 }


