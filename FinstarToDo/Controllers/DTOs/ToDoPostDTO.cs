using FinstarToDo.DB.Models.Enums;

namespace FinstarToDo.Controllers.DTOs
{
    public class ToDoPostDTO
    {
        public string Header { get; set; }
        public Category Category { get; set; }
        public Color Color { get; set; }
    }
}
