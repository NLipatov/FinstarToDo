using FinstarToDo.DB;

namespace FinstarToDo.Controllers.Extensions;

public static class ToDoContextExtensions
{
    public static async Task<T> GetIfExistAsync<T>(this ToDoContext context, object key)
    {
        T? toDo = (T?) await context.FindAsync(typeof(T), key);
        if (toDo is null)
            throw new ArgumentException($"There is no {typeof(T)} with specified identifier: {key}");

        return toDo;
    }
}
