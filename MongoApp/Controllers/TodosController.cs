using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using MongoApp.Models;
using MongoApp.Repository;

namespace MongoApp.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class TodosController : ControllerBase
    {
        private readonly ITodoRepository _repository;
        
        public TodosController(ITodoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodos()
        {
            return Ok(await _repository.GetAllTodos());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodo(long id)
        {
            return Ok(await _repository.GetTodo(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Todo todo)
        {
            todo.Id = await _repository.GetNextId();
            await _repository.Create(todo);
            return Ok(todo);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Todo>> Put(long id, [FromBody] Todo todo)
        {
            var todoFromDb = await _repository.GetTodo(id);

            if (todoFromDb == null)
            {
                return NotFound();
            }

            todo.Id = todoFromDb.Id;
            todo.InternalId = todoFromDb.InternalId;
            await _repository.Update(todo);
            return Ok(todo);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var todoFromDb = await _repository.GetTodo(id);

            if (todoFromDb == null)
            {
                return NotFound();
            }
            
            return Ok(await _repository.Delete(id));
        }
    }
}