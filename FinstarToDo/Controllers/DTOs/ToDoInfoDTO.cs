using FinstarToDo.DB.Models;

namespace FinstarToDo.Controllers.DTOs
{
    public class ToDoInfoDTO
    {
        public ToDo ToDo { get; set; }
        public string Hash { get; set; }
    }
}
