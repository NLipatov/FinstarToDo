using FinstarToDo.Controllers.DTOs;
using FinstarToDo.DB;
using FinstarToDo.DB.Models;
using FinstarToDo.Services.HashCalculator;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace FinstarToDo.DAL;

public class DataAccessLayer : IDataAccessLayer
{
    private readonly ToDoContext _toDoContext;
    private readonly IHashCalculatorService _hashCalculatorService;
    private readonly ILogger<DataAccessLayer> _logger;

    public DataAccessLayer
    (ToDoContext toDoContext, 
    IHashCalculatorService hashCalculatorService,
    ILogger<DataAccessLayer> logger)
    {
        _toDoContext = toDoContext;
        _hashCalculatorService = hashCalculatorService;
        _logger = logger;
    }
    public async Task<List<ToDoInfoDTO>> GetTodosList()
    {
        List<ToDo> toDos = await _toDoContext.ToDos
            .AsNoTracking()
            .Include(x => x.Commentaries).ToListAsync();

        var result = toDos
        .Select(x => new ToDoInfoDTO
        {
            ToDo = x,
            Hash = _hashCalculatorService.CalculateMD5Hash(x.Header)
        })
        .ToList();
        return result;
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
        return await _toDoContext.ToDos
            .AsNoTracking()
            .Include(x => x.Commentaries).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task DeleteTodo(Guid id)
    {
        ToDo toDo = await _toDoContext.ToDos.Include(x=>x.Commentaries).FirstAsync(x => x.Id == id);

        toDo.Commentaries.ForEach(x => _toDoContext.Remove(x));
        _toDoContext.ToDos.Remove(toDo);
        await _toDoContext.SaveChangesAsync();
    }

    public async Task UpdateHeader(Guid id, string updatedHeader)
    {
        ToDo toDo = await _toDoContext.ToDos.FirstAsync(x => x.Id == id);

        toDo.Header = updatedHeader;
        await _toDoContext.SaveChangesAsync();
    }

    public async Task<List<Commentary>> GetTodoComments(Guid id)
    {
        ToDo? toDo = await _toDoContext.ToDos
            .AsNoTracking()
            .Include(x => x.Commentaries).FirstOrDefaultAsync(x => x.Id == id);

        return toDo?.Commentaries ?? new();
    }

    public async Task AddToDoComment(Guid id, string commentary)
    {
        ToDo toDo = await _toDoContext.ToDos
            .Include(x=>x.Commentaries).FirstAsync(x => x.Id == id);

        toDo.Commentaries.Add(new Commentary
        {
            Comment = commentary
        });

        await _toDoContext.SaveChangesAsync();
    }
}
