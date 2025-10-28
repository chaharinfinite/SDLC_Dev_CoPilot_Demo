using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PatientPortal.Application.Interfaces;
using PatientPortal.Domain.Common;

namespace PatientPortal.Infrastructure.Persistence
{
    public class InMemoryRepository<T> : IRepository<T> where T : EntityBase
    {
        private readonly InMemoryDataStore _dataStore;

        public InMemoryRepository(InMemoryDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public Task<T> AddAsync(T entity)
        {
            var set = _dataStore.Set<T>();
            set[entity.Id] = entity;
            return Task.FromResult(entity);
        }

        public Task DeleteAsync(T entity)
        {
            var set = _dataStore.Set<T>();
            set.TryRemove(entity.Id, out _);
            return Task.CompletedTask;
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            var set = _dataStore.Set<T>();
            set.TryGetValue(id, out var entity);
            return Task.FromResult(entity);
        }

        public Task<IReadOnlyList<T>> ListAsync()
        {
            var set = _dataStore.Set<T>();
            IReadOnlyList<T> results = set.Values.ToList();
            return Task.FromResult(results);
        }

        public Task<IReadOnlyList<T>> SearchAsync(Func<T, bool> predicate)
        {
            var set = _dataStore.Set<T>();
            IReadOnlyList<T> results = set.Values.Where(predicate).ToList();
            return Task.FromResult(results);
        }

        public Task UpdateAsync(T entity)
        {
            var set = _dataStore.Set<T>();
            set[entity.Id] = entity;
            return Task.CompletedTask;
        }
    }
}
