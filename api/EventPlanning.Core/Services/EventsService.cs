using EventPlanning.Core.Data;
using EventPlanning.Core.DTOs.Event;
using EventPlanning.Core.Storages;
using EventPlanning.Core.Utils;
using EventPlanning.Core.Exceptions;

namespace EventPlanning.Core.Services
{
    internal class EventsService : IEventsService
    {
        private readonly IEventStorage m_EventStorage;
        private readonly IUnitOfWork m_UnitOfWork;
        private readonly IAuthUtils m_AuthUtils;
        public EventsService(IEventStorage eventStorage, IUnitOfWork uow, IAuthUtils authUtils)
        {
            m_EventStorage = eventStorage;
            m_UnitOfWork = uow;
            m_AuthUtils = authUtils;
        }

        public Task<EventDto> CreateEventAsync(CreateEventDto model)
        {
            try
            {
                return m_EventStorage.CreateAsync(model, m_AuthUtils.GetAuthUserId());
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create event", ex);
            }
        }

        public async Task<IEnumerable<EventDto>> GetEventsAsync()
        {
            try
            {
                var result = await m_EventStorage.GetAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create event", ex);
            }
        }

        public async Task<bool> RegisterToEvent(int eventId)
        {
            try
            {
                var userEvent = await m_EventStorage.GetAsync(eventId, forUpdate: true);
                if (userEvent is not null)
                {
                    var currentUserId = m_AuthUtils.GetAuthUserId();
                    return await m_EventStorage.AddMember(eventId, currentUserId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to register to event", ex);
            }

            throw new NotFoundCoreException();
        }
    }
}
