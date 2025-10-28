using System;
using System.Collections.Concurrent;
using PatientPortal.Domain.Common;

namespace PatientPortal.Infrastructure.Persistence
{
    internal class InMemoryDataStore
    {
        private readonly ConcurrentDictionary<Type, object> _sets = new ConcurrentDictionary<Type, object>();

        public ConcurrentDictionary<Guid, T> Set<T>() where T : EntityBase
        {
            return (ConcurrentDictionary<Guid, T>)_sets.GetOrAdd(typeof(T), _ => new ConcurrentDictionary<Guid, T>());
        }
    }
}
