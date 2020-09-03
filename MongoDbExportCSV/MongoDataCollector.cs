using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDbExportCSV
{
    public static class MongoDbGatherer
    {
        public static HashSet<string> ProcessTreeWithEmbededDocuments(HashSet<string> fields, List<BsonDocument> tree, string parentField)
        {
            foreach (var item in tree)
            {
                foreach (var field in item)
                {

                    string fieldName = field.Name;
                    if (parentField != "")
                    {
                        fieldName = parentField + "." + fieldName;
                    }

                    if (field.Value.IsBsonDocument)
                    {
                        ProcessTreeWithEmbededDocuments(fields, new List<BsonDocument> { field.Value.ToBsonDocument() }, fieldName);
                    }
                    else
                    {
                        fields.Add(fieldName);
                    }
                }
            }

            return fields;
        }

        public static HashSet<string> ProcessTreeNoEmbededDocuments(HashSet<string> fields, List<BsonDocument> tree, string parentField)
        {
            foreach (var item in tree)
            {
                foreach (var field in item)
                {
                    fields.Add(field.Name);
                }
            }

            return fields;
        }
    }
}
