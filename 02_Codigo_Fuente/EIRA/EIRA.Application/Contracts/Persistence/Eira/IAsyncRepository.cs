using EIRA.Application.Models.EntityModels;
using EIRA.Application.Specifications;
using EIRA.Domain.Common;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIRA.Application.Contracts.Persistence.Eira
{
    public interface IAsyncRepository<T> where T : BaseEntity
    {
        void SetIsValidate(bool validate);
        void SetIQueryable(Func<IQueryable<T>, IQueryable<T>> func);
        IQueryable<T> GetIQueryable();
        IQueryable<T> GetIQueryable(ISpecification<T> spec);
        Task<T> GetByIdAsync<Z>(Z id);
        Task<T> GetByIdAsync<Z>(params Enum[] @enum);
        Task<T> GetByIdAsync(params object[] id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListNotTrackingAsync(ISpecification<T> spec);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entity, bool masivo = false);
        Task UpdateRangeAsync(IEnumerable<T> entity, bool masivo = false);
        Task AttachAsync(T entity, Func<T, T> func);
        Task<List<RegistroModificado>> AttachCatchFieldAsync(T entity, Func<T, T> func, Func<T, string, T> func2 = null);
        Task AttachRangeAsync(IEnumerable<T> entity, Func<IEnumerable<T>, IEnumerable<T>> func);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task DeleteRangeAsync(IEnumerable<T> entity);
        Task<long> CountAsync(ISpecification<T> spec);
        Task<long> CountNoTrackingAsync(ISpecification<T> spec);
        Task<IDbContextTransaction> BeginTransaction();
        Task<IReadOnlyList<T>> GetExecuteAsync(Enum @enum, ISpecification<T> spec, bool executeOnDapper = true);
        bool TransactionIsClosed();
        bool TransactionIsOpen();
        Task<IReadOnlyList<T>> ListAllNoTrackingAsync();
    }
}
