using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Web.Api.Config;

namespace Web.Api.Repositories
{
    public class RepositoryBase<T> where T : class
    {
        private readonly IMongoCollection<T> _collection;
        public virtual string CollectionName { get; set; }

        public RepositoryBase(IAppSettings settings)
        {
            var clientSettings = new MongoClientSettings
            {
                // TODO: Design a way to easily configure the MongoDB container to set an admin password
                //Credential = MongoCredential.CreateCredential(settings.Database.AdminDb, settings.Database.AdminUser, settings.Database.AdminPass),
                Server = new MongoServerAddress(settings.Database.ConnectionUrl, settings.Database.Port),
            };

            var client = new MongoClient(clientSettings);
            var database = client.GetDatabase(settings.Database.Name);

            var conventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
            ConventionRegistry.Register("camelCase", conventionPack, t => true);

            _collection = database.GetCollection<T>(CollectionName);
        }

        public async Task<T> GetById(string id)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);
            var result = (await _collection.FindAsync(filter)).FirstOrDefault();

            return result;
        }

        public async Task<List<T>> FindByFilter(FilterDefinition<T> filter)
        {
            var result = (await _collection.FindAsync(filter)).ToList();
            return result;
        }

        public async Task<List<T>> GetAll()
        {
            var result = (await _collection.FindAsync(_ => true)).ToList();
            return result;
        }

        /// <summary>
        /// Upsert a document to the database
        /// </summary>
        /// <param name="entity">A document to create or update</param>
        /// <param name="filter">Will only update if the filter matches an existing document</param>
        /// <returns>T</returns>
        public async Task<T> Upsert(T entity, FilterDefinition<T> filter = null)
        {
            // Probably shouldn't use reflection... Probably need to create my own GenericEntity<T> which contains Id (maybe CreatedOn, UpdatedOn?)?
            var id = entity.GetType().GetProperty("Id")?.GetValue(entity, null);

            // No ID, create and forget
            if (id == null)
            {
                await _collection.InsertOneAsync(entity);
                return entity;
            }

            // Has ID, better find and replace
            var filterDefinition = Builders<T>.Filter.And(Builders<T>.Filter.Eq("Id", id));
            if (filter != null)
            {
                filterDefinition &= filter;
            }

            await _collection.FindOneAndReplaceAsync(filterDefinition, entity);
            return entity;
        }
    }
}
