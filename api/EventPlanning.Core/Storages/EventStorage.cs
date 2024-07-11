using EventPlanning.Core.Data;
using EventPlanning.Core.Data.Entities;
using EventPlanning.Core.DTOs.Event;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace EventPlanning.Core.Storages
{
    internal class EventStorage : IEventStorage
    {
        private readonly IUnitOfWork m_UnitOfWork;
        private readonly UserManager<UserEntity> m_UserManager;
        public EventStorage(IUnitOfWork uow, UserManager<UserEntity> userManager)
        {
            m_UnitOfWork = uow;
            m_UserManager = userManager;
        }

        public async Task<bool> AddMember(int eventId, int currentUserId)
        {
            bool result = false;

            var eventEntity = await m_UnitOfWork.EventRepository.GetByIdAsync(eventId, forUpdate: true);
            var userEntity = await m_UserManager.FindByIdAsync(currentUserId.ToString());
            if (eventEntity is not null && userEntity is not null)
            {
                eventEntity.Members.Add(userEntity);
                await m_UnitOfWork.SaveAsync();
                result = true;
            }

            return result;
        }

        public async Task<EventDto> CreateAsync(CreateEventDto model, int creatorId)
        {
            EventEntity entity = new()
            {
                Name = model.Name,
                Description = model.Description,
                Venue = model.Venue,
                IsOnline = model.IsOnline,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                CreatorId = creatorId,
            };

            var createdEntity = await m_UnitOfWork.EventRepository.CreateAsync(entity);

            await m_UnitOfWork.SaveAsync();

            return EventDto.From(createdEntity);
        }

        public async Task<EventDto?> GetAsync(int eventId, bool forUpdate)
        {
            var entity = await m_UnitOfWork.EventRepository.GetByIdAsync(eventId, forUpdate);

            if (entity is null)
            {
                return null;
            }

            return EventDto.From(entity);
        }

        public async Task<List<EventDto>> GetAsync()
        {
            var events = await m_UnitOfWork.EventRepository.GetAsync();
            return events.Select(EventDto.From).ToList();
        }
    }
}
