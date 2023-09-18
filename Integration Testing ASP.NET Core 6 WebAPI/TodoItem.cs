using System.ComponentModel.DataAnnotations;

namespace Integration_Testing_ASP.NET_Core_6_WebAPI
{
    public class TodoItem
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsComplete { get; set; }
        public DateTime DueDate { get; set; }
    }
}
