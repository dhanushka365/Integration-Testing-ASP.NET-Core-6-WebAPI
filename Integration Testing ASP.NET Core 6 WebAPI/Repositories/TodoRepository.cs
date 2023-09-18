using Integration_Testing_ASP.NET_Core_6_WebAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace Integration_Testing_ASP.NET_Core_6_WebAPI.Repositories
{
    public class TodoRepository :ITodoRepository
    {
        private readonly AppDbContext _context;

        public TodoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TodoItem>> GetTodoItemsAsync()
        {
            return await _context.TodoItems.ToListAsync();
        }

        public async Task<TodoItem> GetTodoItemAsync(int id)
        {
            return await _context.TodoItems.FindAsync(id);
        }

        public async Task<TodoItem> AddTodoItemAsync(TodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();
            return todoItem;
        }

        public async Task<TodoItem> UpdateTodoItemAsync(TodoItem todoItem)
        {
            _context.Entry(todoItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return todoItem;
        }

        public async Task<TodoItem> DeleteTodoItemAsync(int id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null) return null;
            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();
            return todoItem;
        }
    }
}
