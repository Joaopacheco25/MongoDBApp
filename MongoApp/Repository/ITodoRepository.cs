using System.Collections.Generic;
using System.Threading.Tasks;
using MongoApp.Models;

namespace MongoApp.Repository
{
    public interface ITodoRepository
    {
        Task<IEnumerable<Todo>> GetAllTodos();
        Task<Todo> GetTodo(long id);
        Task Create(Todo todo);
        Task<bool> Update(Todo todo);
        Task<bool> Delete(long id);
        Task<long> GetNextId();
    }
}