namespace Integration_Testing_ASP.NET_Core_6_WebAPI.Repositories
{
    public interface ITodoRepository
    {
        Task<IEnumerable<TodoItem>> GetTodoItemsAsync();
        Task<TodoItem> GetTodoItemAsync(int id);
        Task<TodoItem> AddTodoItemAsync(TodoItem todoItem);
        Task<TodoItem> UpdateTodoItemAsync(TodoItem todoItem);
        Task<TodoItem> DeleteTodoItemAsync(int id);
    }
}
