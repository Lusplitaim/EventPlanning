using EventPlanning.Core.Data;
using EventPlanning.Core.Data.Repositories;
using EventPlanning.Infrastructure.Data.Repositories;

namespace EventPlanning.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private DatabaseContext m_DbContext;
        public UnitOfWork(DatabaseContext dbContext)
        {
            m_DbContext = dbContext;
        }

        public IEventRepository EventRepository => new EventRepository(m_DbContext);

        public async Task SaveAsync()
        {
            await m_DbContext.SaveChangesAsync();
        }
    }
}
