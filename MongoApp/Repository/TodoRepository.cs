using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoApp.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoApp.Repository
{
    public class TodoRepository : ITodoRepository
    {
        private readonly ITodoContext _context;
        public TodoRepository(ITodoContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Todo>> GetAllTodos()
        {
            return await _context.Todos.Find(_ => true)
                .ToListAsync();
        }

        public async Task<Todo> GetTodo(long id)
        {
            FilterDefinition<Todo> filter = Builders<Todo>.Filter
                .Eq(m => m.Id, id);
            
            return await _context
                .Todos
                .Find(filter)
                .FirstOrDefaultAsync();
        }

        public async Task Create(Todo todo)
        {
            try
            {
                await _context.Todos.InsertOneAsync(todo);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<bool> Update(Todo todo)
        {
            var update = await _context.Todos
                .ReplaceOneAsync(filter: g => g.Id == todo.Id, replacement:todo);

            return update.IsAcknowledged && update.ModifiedCount > 0;
        }

        public async Task<bool> Delete(long id)
        {
            var filter = Builders<Todo>.Filter.Eq(m => m.Id, id);
            var delete = await _context.Todos.DeleteOneAsync(filter);
            
            return delete.IsAcknowledged && delete.DeletedCount > 0;
        }

        public async Task<long> GetNextId()
        {
            return await _context
                .Todos
                .CountDocumentsAsync(new BsonDocument()) + 1;
        }
    }
}