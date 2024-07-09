namespace EventPlanning.Core.Data
{
    public interface IUnitOfWork
    {
        Task SaveAsync();
    }
}
