using EventPlanning.Core.Data;

namespace EventPlanning.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private DatabaseContext m_DbContext;
        public UnitOfWork(DatabaseContext dbContext)
        {
            m_DbContext = dbContext;
        }

        public async Task SaveAsync()
        {
            await m_DbContext.SaveChangesAsync();
        }
    }
}
