using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ToDo.Models
{
    public class Task 
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id {get; set; }
        [BsonElement("title")]
        public string title {get; set; }
        public bool completed {get; set; }
    }
}