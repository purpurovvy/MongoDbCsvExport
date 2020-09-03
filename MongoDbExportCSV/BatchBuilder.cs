using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDbExportCSV
{
    public static class BatchBuilder
    {
        public static void PrepareFullExport(string connectionString, string path, string mongoexportExe)
        {
            var MongoDatabaseModelList = new List<MongoDatabaseModel>();
            var MongoDatabaseInstance = new MongoDatabase(connectionString);
            var MongoDbList = MongoDatabaseInstance.GetListOfDatabases();
            foreach (var mongoDbInstance in MongoDbList)
            {
                var CollectionList = MongoDatabaseInstance.GetCollectionNameList(mongoDbInstance);
                var MongoDbCollectionModelList = new List<MongoCollectionModel>();

                foreach (var collection in CollectionList)
                {
                    var Columns = MongoDatabaseInstance.GetColumnKeys(mongoDbInstance, collection);
                    MongoDbCollectionModelList.Add(new MongoCollectionModel { CollectionName = collection, Columns = Columns });

                }
                MongoDatabaseModelList.Add(new MongoDatabaseModel { DatabaseName = mongoDbInstance, Collections = MongoDbCollectionModelList });
            }

            CommandBuilder(MongoDatabaseModelList, path, mongoexportExe);

        }

        public static void PrepareSpecificExport(string connectionString, string databaseName, string? collectionName, string path, string mongoexportExe)
        {
            var MongoDatabaseModelList = new List<MongoDatabaseModel>();
            var MongoDbCollectionModelList = new List<MongoCollectionModel>();
            var CollectionList = new List<string>();
            var MongoDatabaseInstance = new MongoDatabase(connectionString);
            if(collectionName == null)
            {
                CollectionList = MongoDatabaseInstance.GetCollectionNameList(databaseName);
            }
            else
            {
             
                CollectionList.Add(collectionName);
            }

            foreach (var collection in CollectionList)
            {
                var Columns = MongoDatabaseInstance.GetColumnKeys(databaseName, collection);
                MongoDbCollectionModelList.Add(new MongoCollectionModel { CollectionName = collection, Columns = Columns });

            }
            MongoDatabaseModelList.Add(new MongoDatabaseModel { DatabaseName = databaseName, Collections = MongoDbCollectionModelList });

            CommandBuilder(MongoDatabaseModelList, path, mongoexportExe);
            Console.WriteLine("Your results are available in ./CSV_Export folder");
        }

        private static void CommandBuilder(List<MongoDatabaseModel> mongoDatabaseModel, string path, string mongoexportExe)
        {
            foreach (var database in mongoDatabaseModel)
            {
                foreach (var collection in database.Collections)
                {
                    var fields = new StringBuilder("");
                    foreach (var column in collection.Columns)
                    {
                        fields.Append($"{column},");
                    }
                    fields.Length--;
                    MongoExport.ExportProcess(database.DatabaseName, collection.CollectionName, fields.ToString(), mongoexportExe, path );
                }
            }
        }

    }
}
