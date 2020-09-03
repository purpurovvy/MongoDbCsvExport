using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDbExportCSV
{
    public class MongoDatabaseModel
    {
        public string DatabaseName { get; set; }
        public List<MongoCollectionModel> Collections { get; set; }
    }
}
