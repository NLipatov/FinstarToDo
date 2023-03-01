using FinstarToDo.Services.HashCalculator;

namespace FinstarToDo.Services.HashCalculating.Extensions;

public static class HashServiceExtensions
{
    public static IServiceCollection UseHashService(this IServiceCollection services)
    {
        return services.AddTransient<IHashCalculatorService, HashCalculatorService>();
    }
}
