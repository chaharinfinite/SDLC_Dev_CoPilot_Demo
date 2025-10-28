using System;
using System.Collections.Generic;

namespace PatientPortal.Domain.Common
{
    public abstract class EntityBase
    {
        private readonly List<DomainEvent> _domainEvents = new List<DomainEvent>();

        protected EntityBase()
        {
            Id = Guid.NewGuid();
            CreatedOn = DateTimeOffset.UtcNow;
        }

        public Guid Id { get; protected set; }
        public DateTimeOffset CreatedOn { get; protected set; }
        public DateTimeOffset? UpdatedOn { get; protected set; }
        public string CreatedBy { get; protected set; }
        public string UpdatedBy { get; protected set; }

        public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        protected void AddDomainEvent(DomainEvent domainEvent)
        {
            if (domainEvent == null)
            {
                throw new ArgumentNullException(nameof(domainEvent));
            }

            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        public void StampAudit(string userId)
        {
            UpdatedOn = DateTimeOffset.UtcNow;
            UpdatedBy = userId;
        }
    }
}
