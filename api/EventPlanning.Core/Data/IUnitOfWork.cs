using EventPlanning.Core.Data.Repositories;

namespace EventPlanning.Core.Data
{
    public interface IUnitOfWork
    {
        IEventRepository EventRepository { get; }
        Task SaveAsync();
    }
}
