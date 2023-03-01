using FinstarToDo.Controllers.DTOs;
using FinstarToDo.DB.Models;

namespace FinstarToDo.DAL
{
    public interface IDataAccessLayer
    {
        Task AddToDoComment(Guid id, string commentary);
        Task DeleteTodo(Guid id);
        Task<ToDo?> GetTodo(Guid id);
        Task<List<Commentary>> GetTodoComments(Guid id);
        Task<List<ToDoInfoDTO>> GetTodosList();
        Task PostTodo(ToDoPostDTO dto);
        Task UpdateHeader(Guid id, string updatedHeader);
    }
}