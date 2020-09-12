using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDBTests.Core;

namespace MongoDBTests.Data.Repository
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
    {
        private readonly IMongoDatabase _db;
        protected string CollectionName { get; set; }

        public RepositoryBase(string collectionName)
        {
            var databaseConnection = ConfigurationManager.AppSettings["DatabaseConnection"];
            var databaseName = ConfigurationManager.AppSettings["DatabaseName"];
            var client = new MongoClient(databaseConnection);
            _db = client.GetDatabase(databaseName);

            this.CollectionName = collectionName;
        }

        public void Save(TEntity entity)
        {
            var id = (entity as Entity).Id;

            var collection = _db.GetCollection<TEntity>(CollectionName);
            var result = collection.ReplaceOne(
                new BsonDocument("_id", BsonValue.Create(id)),
                entity,
                new ReplaceOptions { IsUpsert = true });
        }

        public void Delete(Guid id)
        {
            var collection = _db.GetCollection<TEntity>(CollectionName);
            var filter = Builders<TEntity>.Filter.Eq("Id", id);
            collection.DeleteOne(filter);
        }

        public IEnumerable<TEntity> SelectAll()
        {
            var collection = _db.GetCollection<TEntity>(CollectionName);
            return collection.Find(new BsonDocument()).ToList();
        }

        public IEnumerable<TEntity> SelectWhere(Expression<Func<TEntity, bool>> predicate)
        {
            var collection = _db.GetCollection<TEntity>(CollectionName);
            return collection.Find(predicate).ToList();
        }

        public TEntity SelectById(Guid id)
        {
            var collection = _db.GetCollection<TEntity>(CollectionName);
            var filter = Builders<TEntity>.Filter.Eq("Id", id);
            return collection.Find(filter).First();
        }

        public TEntity SelectOne(Expression<Func<TEntity, bool>> predicate)
        {
            var collection = _db.GetCollection<TEntity>(CollectionName);
            return collection.Find(predicate).FirstOrDefault();
        }
    }
}
