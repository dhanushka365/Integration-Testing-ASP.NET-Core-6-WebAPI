namespace Integration_Testing_ASP.NET_Core_6_WebAPI.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsComplete { get; set; }
        public DateTime DueDate { get; set; }
    }
}
