using System;
using Actio.Common.Exceptions;

namespace Actio.Services.Activities.Domain.Models
{
    public class Activity
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public string Category { get; protected set; }
        public string Description { get; protected set; }
        public Guid UserId { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        protected Activity()
        {
        }

        public Activity(Guid Id, Category category, Guid userId, string name, string description, DateTime createdAt)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ActioException("empty_activity_name", $"Activity: name can not be empty.");
            }

            Id = Guid.NewGuid();
            Name = name.ToLowerInvariant();
            Category = category.Name;
            Description = description;
            UserId = userId;
            CreatedAt = createdAt;
        }
    }
}