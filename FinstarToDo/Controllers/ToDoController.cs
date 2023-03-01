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
        public async Task<List<ToDo>> GetTodosList()
        {
            return await _toDoContext.ToDos.ToListAsync();
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
            return await _toDoContext
                .Comments
                .Where(x => x.ToDo.Id == id)
                .ToListAsync();
        }

        [HttpPost("todo/{id}/commentary")]
        public async Task AddToDoComment(Guid id, string commentary)
        {
            ToDo? toDo = await _toDoContext.GetIfExistAsync<ToDo>(id);

            await _toDoContext.Comments.AddAsync(new Commentary
            {
                Comment = commentary,
                ToDo = toDo
            });

            await _toDoContext.SaveChangesAsync();
        }
    }
}
