using FinstarToDo.Controllers.DTOs;
using FinstarToDo.DAL;
using FinstarToDo.DB.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinstarToDo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly IDataAccessLayer _dataAccessLayer;

        public ToDoController(IDataAccessLayer dataAccessLayer)
        {
            _dataAccessLayer = dataAccessLayer;
        }

        [HttpGet("todos")]
        public async Task<List<ToDoInfoDTO>> GetTodosList() => await _dataAccessLayer.GetTodosList();

        [HttpPost("todo")]
        public async Task PostTodo(ToDoPostDTO dto) => await _dataAccessLayer.PostTodo(dto);

        [HttpGet("todo/{id}")]
        public async Task<ToDo?> GetTodo(Guid id) => await _dataAccessLayer.GetTodo(id);

        [HttpDelete("todo/{id}")]
        public async Task DeleteTodo(Guid id) => await _dataAccessLayer.DeleteTodo(id);

        [HttpPut("todo/{id}/header")]
        public async Task UpdateHeader(Guid id, string updatedHeader) => await _dataAccessLayer.UpdateHeader(id, updatedHeader);

        [HttpGet("todo/{id}/comments")]
        public async Task<List<Commentary>> GetTodoComments(Guid id) => await _dataAccessLayer.GetTodoComments(id);

        [HttpPost("todo/{id}/commentary")]
        public async Task AddToDoComment(Guid id, string commentary) => await _dataAccessLayer.AddToDoComment(id, commentary);
    }
}
