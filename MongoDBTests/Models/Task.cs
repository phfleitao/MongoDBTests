using MongoDB.Bson.Serialization.Attributes;
using MongoDBTests.Core;

namespace MongoDBTests.Models
{
    public class Task : Entity
    {       
        public string Description { get; set; }
        
        public bool Completed { get; set; }
    }
}
