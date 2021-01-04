using MongoDB.Driver;

namespace MongoApp.Models
{
    public interface ITodoContext
    {
        IMongoCollection<Todo> Todos {get;}
    }
}