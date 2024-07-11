using EventPlanning.Core.Data.Entities;

namespace EventPlanning.Core.Data.Repositories
{
    public interface IEventRepository
    {
        Task<EventEntity?> GetByIdAsync(int eventId, bool forUpdate = false);
        Task<List<EventEntity>> GetAsync();
        Task<List<EventEntity>> GetByCreatorAsync(int creatorId);
        Task<EventEntity> CreateAsync(EventEntity entity);
    }
}
