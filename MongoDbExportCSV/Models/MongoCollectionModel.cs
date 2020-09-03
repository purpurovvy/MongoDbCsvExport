using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDbExportCSV
{
    public class MongoCollectionModel
    {
        public string CollectionName { get; set; }
        public HashSet<string> Columns { get; set; }
    }
}
