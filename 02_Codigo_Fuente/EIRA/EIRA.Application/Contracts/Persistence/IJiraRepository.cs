using EIRA.Domain.Common;

namespace EIRA.Application.Contracts.Persistence
{
    public interface IJiraRepository<T> where T : BaseEntity
    {
        Task<List<T>> GetAll();
    }
}
