using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PatientPortal.Web.Infrastructure
{
    public class SimpleDependencyResolver : IDependencyResolver
    {
        private readonly IDictionary<Type, object> _services;

        public SimpleDependencyResolver(IDictionary<Type, object> services)
        {
            _services = services;
        }

        public object GetService(Type serviceType)
        {
            if (_services.TryGetValue(serviceType, out var service))
            {
                return service;
            }

            return null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (_services.TryGetValue(serviceType, out var service))
            {
                return new[] { service };
            }

            return Array.Empty<object>();
        }
    }
}
