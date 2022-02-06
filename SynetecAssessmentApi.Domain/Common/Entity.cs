using System;

namespace SynetecAssessmentApi.Domain.Common
{
    public abstract class Entity
    {
        public int Id { get; set; }

        public Entity(int id)
        {
            Id = id;
        }
    }
}
