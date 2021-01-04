using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoApp.Utils;
using MongoDB.Driver;

namespace MongoApp.Models
{
    public class TodoContext : ITodoContext
    {
        private readonly IMongoDatabase _db;
        private IOptions<MongoDbConfig> _optionsMongoDbConfig;

        public TodoContext(IOptions<MongoDbConfig> optionsMongoDbConfig)
        {
            _optionsMongoDbConfig = optionsMongoDbConfig;
            var client = new MongoClient(_optionsMongoDbConfig.Value.ConnectionString);
            _db = client.GetDatabase(_optionsMongoDbConfig.Value.Database);
        }
        public IMongoCollection<Todo> Todos => _db.GetCollection<Todo>("Todos");
    }
}