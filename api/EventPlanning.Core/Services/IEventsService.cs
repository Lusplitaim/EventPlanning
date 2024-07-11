using EventPlanning.Core.DTOs.Event;

namespace EventPlanning.Core.Services
{
    public interface IEventsService
    {
        Task<IEnumerable<EventDto>> GetEventsAsync();
        Task<EventDto> CreateEventAsync(CreateEventDto model);
        Task<bool> RegisterToEvent(int eventId);
    }
}
