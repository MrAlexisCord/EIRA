using EIRA.Application.Contracts.Persistence;
using EIRA.Domain.Common;

namespace EIRA.Infrastructure.Repositories
{
    public class JiraRepository<T> : IJiraRepository<T> where T : BaseEntity
    {
        public Task<List<T>> GetAll()
        {
            return null;
        }
    }
}
