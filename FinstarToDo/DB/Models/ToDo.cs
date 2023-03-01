using FinstarToDo.DB.Models.Enums;

namespace FinstarToDo.DB.Models
{
    public class ToDo
    {
        public Guid Id { get; set; }
        public string Header { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsDone { get; set; }
        public Category Category { get; set; }
        public Color Color { get; set; }
        public List<Commentary> Commentaries { get; set; } = new();
    }
}
