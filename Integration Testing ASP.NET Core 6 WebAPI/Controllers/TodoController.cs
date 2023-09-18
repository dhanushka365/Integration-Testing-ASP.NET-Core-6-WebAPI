using Integration_Testing_ASP.NET_Core_6_WebAPI.Data;
using Integration_Testing_ASP.NET_Core_6_WebAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Integration_Testing_ASP.NET_Core_6_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {

        private readonly ITodoRepository _todoRepository;

        public TodoController(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            var todoItems = await _todoRepository.GetTodoItemsAsync();
            return Ok(todoItems);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(int id)
        {
            var todoItem = await _todoRepository.GetTodoItemAsync(id);

            if (todoItem == null)
            {
                return NotFound(); // Return a 404 response if the item is not found.
            }

            return Ok(todoItem);
        }

        [HttpPost]
        public async Task<ActionResult<TodoItem>> CreateTodoItem(TodoItem todoItem)
        {
            var createdTodoItem = await _todoRepository.AddTodoItemAsync(todoItem);
            return CreatedAtAction(nameof(GetTodoItem), new { id = createdTodoItem.Id }, createdTodoItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(int id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest(); // Return a 400 Bad Request response if the IDs do not match.
            }

            var updatedTodoItem = await _todoRepository.UpdateTodoItemAsync(todoItem);

            if (updatedTodoItem == null)
            {
                return NotFound(); // Return a 404 response if the item is not found.
            }

            return NoContent(); // Return a 204 No Content response on success.
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoItem>> DeleteTodoItem(int id)
        {
            var deletedTodoItem = await _todoRepository.DeleteTodoItemAsync(id);

            if (deletedTodoItem == null)
            {
                return NotFound(); // Return a 404 response if the item is not found.
            }

            return Ok(deletedTodoItem); // Return the deleted item in the response.
        }
    }
}
