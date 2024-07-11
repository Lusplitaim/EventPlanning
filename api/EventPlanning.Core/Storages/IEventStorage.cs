using EventPlanning.Core.DTOs.Event;

namespace EventPlanning.Core.Storages
{
    public interface IEventStorage
    {
        Task<List<EventDto>> GetAsync();
        Task<EventDto?> GetAsync(int eventId, bool forUpdate);
        Task<EventDto> CreateAsync(CreateEventDto model, int creatorId);
        Task<bool> AddMember(int eventId, int currentUserId);
    }
}
