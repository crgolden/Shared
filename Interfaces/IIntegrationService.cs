namespace crgolden.Shared
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IIntegrationService
    {
        Task<TModel> Create<TModel, TRequest>(TRequest request, CancellationToken token)
            where TModel : class;

        Task<TModel> Read<TModel, TRequest>(TRequest request, CancellationToken token)
            where TModel : class;

        Task Update<TModel, TRequest>(TRequest request, CancellationToken token)
            where TModel : class;

        Task Delete<TModel, TRequest>(TRequest request, CancellationToken token)
            where TModel : class;

        Task<TModel[]> List<TModel, TRequest>(TRequest request, CancellationToken token)
            where TModel : class;
    }
}
