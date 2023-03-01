using FinstarToDo.Controllers.DTOs;
using FinstarToDo.Controllers.Extensions;
using FinstarToDo.DB;
using FinstarToDo.DB.Models;
using FinstarToDo.Services.HashCalculator;
using Microsoft.EntityFrameworkCore;

namespace FinstarToDo.DAL;

public class DataAccessLayer : IDataAccessLayer
{
    private readonly ToDoContext _toDoContext;
    private readonly IHashCalculatorService _hashCalculatorService;

    public DataAccessLayer(ToDoContext toDoContext, IHashCalculatorService hashCalculatorService)
    {
        _toDoContext = toDoContext;
        _hashCalculatorService = hashCalculatorService;
    }
    public async Task<List<ToDoInfoDTO>> GetTodosList()
    {
        List<ToDo> toDos = await _toDoContext.ToDos.ToListAsync();

        return toDos
        .Select(x => new ToDoInfoDTO
        {
            ToDo = x,
            Hash = _hashCalculatorService.CalculateMD5Hash(x.Header)
        })
        .ToList();
    }
    public async Task PostTodo(ToDoPostDTO dto)
    {
        await _toDoContext.ToDos
        .AddAsync(new ToDo
        {
            Header = dto.Header,
            Category = dto.Category,
            Color = dto.Color,
        });

        await _toDoContext.SaveChangesAsync();
    }

    public async Task<ToDo?> GetTodo(Guid id)
    {
        return await _toDoContext.ToDos.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task DeleteTodo(Guid id)
    {
        ToDo? toDo = await _toDoContext.GetIfExistAsync<ToDo>(id);

        _toDoContext.ToDos.Remove(toDo);
        await _toDoContext.SaveChangesAsync();
    }

    public async Task UpdateHeader(Guid id, string updatedHeader)
    {
        ToDo? toDo = await _toDoContext.GetIfExistAsync<ToDo>(id);

        toDo.Header = updatedHeader;
        await _toDoContext.SaveChangesAsync();
    }

    public async Task<List<Commentary>> GetTodoComments(Guid id)
    {
        ToDo? toDo = await _toDoContext.GetIfExistAsync<ToDo>(id);

        return toDo.Commentaries;
    }

    public async Task AddToDoComment(Guid id, string commentary)
    {
        ToDo? toDo = await _toDoContext.GetIfExistAsync<ToDo>(id);

        toDo.Commentaries.Add(new Commentary 
        {
            Comment = commentary,
            ToDo = toDo
        });

        await _toDoContext.SaveChangesAsync();
    }
}
