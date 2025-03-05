namespace UsersApp.Abstractions.Operations
{
    public interface IOperation<TRequest, TResponse>
    {
        Task<TResponse> ExecuteAsync(TRequest request);
    }
}
