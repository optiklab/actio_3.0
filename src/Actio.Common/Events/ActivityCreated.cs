using System;

namespace Actio.Common.Events
{
    public class ActivityCreated : IAuthenticatedEvent
    {
        protected ActivityCreated()
        {
            //CreatedAt = DateTime.UtcNow;
        }

        public ActivityCreated(Guid id, Guid userId, string category, string name, string description)
        {
            Id = id;
            UserId = userId;
            Category = category;
            Name = name;
            Description = description;
            CreatedAt = DateTime.UtcNow;
        }

        public Guid Id { get; }
        public Guid UserId { get; }
        public string Category { get; }
        public string Name { get; }
        public string Description { get; }
        public DateTime CreatedAt { get; }
    }
}