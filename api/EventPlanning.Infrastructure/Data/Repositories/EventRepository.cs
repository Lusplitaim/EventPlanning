using EventPlanning.Core.Data.Entities;
using EventPlanning.Core.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EventPlanning.Infrastructure.Data.Repositories
{
    internal class EventRepository : IEventRepository
    {
        private readonly DatabaseContext m_DbContext;
        public EventRepository(DatabaseContext dbContext)
        {
            m_DbContext = dbContext;
        }

        public async Task<EventEntity> CreateAsync(EventEntity entity)
        {
            var entry = await m_DbContext.Events.AddAsync(entity);
            return entry.Entity;
        }

        public async Task<List<EventEntity>> GetAsync()
        {
            return await m_DbContext.Events.Include(e => e.Members).ToListAsync();
        }

        public async Task<List<EventEntity>> GetByCreatorAsync(int creatorId)
        {
            return await m_DbContext.Events.Where(e => e.CreatorId == creatorId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<EventEntity?> GetByIdAsync(int eventId, bool forUpdate = false)
        {
            IQueryable<EventEntity> events = m_DbContext.Events;
            if (!forUpdate)
            {
                events = events.AsNoTracking();
            }

            return await events.Include(e => e.Members).SingleOrDefaultAsync(e => e.Id == eventId);
        }
    }
}
