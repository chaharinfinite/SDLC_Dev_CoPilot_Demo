using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PatientPortal.Domain.Common;

namespace PatientPortal.Application.Interfaces
{
    public interface IReadRepository<T> where T : EntityBase
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IReadOnlyList<T>> ListAsync();
        Task<IReadOnlyList<T>> SearchAsync(Func<T, bool> predicate);
    }

    public interface IRepository<T> : IReadRepository<T> where T : EntityBase
    {
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
