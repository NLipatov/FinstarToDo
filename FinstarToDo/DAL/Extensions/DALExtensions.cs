namespace FinstarToDo.DAL.Extensions
{
    public static class DALExtensions
    {
        public static IServiceCollection UseDAL(this IServiceCollection services)
        {
            return services.AddScoped<IDataAccessLayer, DataAccessLayer>();
        }
    }
}
