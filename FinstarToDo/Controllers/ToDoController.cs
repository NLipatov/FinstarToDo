using FinstarToDo.Controllers.DTOs;
using FinstarToDo.Controllers.Extensions;
using FinstarToDo.DB;
using FinstarToDo.DB.Models;
using FinstarToDo.Services.HashCalculator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinstarToDo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly ToDoContext _toDoContext;
        private readonly IHashCalculatorService _hashCalculatorService;

        public ToDoController
            (ToDoContext toDoContext,
            IHashCalculatorService hashCalculatorService)
        {
            _toDoContext = toDoContext;
            _hashCalculatorService = hashCalculatorService;
        }

        [HttpGet("todos")]
        public async Task<List<AllToDosDTO>> GetTodosList()
        {
            List<ToDo> toDos = await _toDoContext.ToDos.ToListAsync();

            return toDos
                .Select(x => new AllToDosDTO
                {
                    ToDo = x,
                    Hash = _hashCalculatorService.CalculateMD5Hash(x.Header)
                })
                .ToList();
        }

        [HttpPost("todo")]
        public async Task PostTodo(ToDo toDo)
        {
            await _toDoContext.ToDos.AddAsync(toDo);
            await _toDoContext.SaveChangesAsync();
        }

        [HttpGet("todo/{id}")]
        public async Task<ToDo?> GetTodo(Guid id)
        {
            return await _toDoContext.ToDos.FirstOrDefaultAsync(x=>x.Id == id);
        }

        [HttpDelete("todo/{id}")]
        public async Task DeleteTodo(Guid id)
        {
            ToDo? toDo = await _toDoContext.GetIfExistAsync<ToDo>(id);

            _toDoContext.ToDos.Remove(toDo);
            await _toDoContext.SaveChangesAsync();
        }

        [HttpPut("todo/{id}/header")]
        public async Task UpdateHeader(Guid id, string updatedHeader)
        {
            ToDo? toDo = await _toDoContext.GetIfExistAsync<ToDo>(id);

            toDo.Header = updatedHeader;
            await _toDoContext.SaveChangesAsync();
        }

        [HttpGet("todo/{id}/comments")]
        public async Task<List<Commentary>> GetTodoComments(Guid id)
        {
            ToDo? toDo = await _toDoContext.GetIfExistAsync<ToDo>(id);

            return toDo.Commentaries;
        }

        [HttpPost("todo/{id}/commentary")]
        public async Task<List<Commentary>> AddToDoComment(Guid id, string commentary)
        {
            ToDo? toDo = await _toDoContext.GetIfExistAsync<ToDo>(id);

            return toDo.Commentaries;
        }
    }
}
