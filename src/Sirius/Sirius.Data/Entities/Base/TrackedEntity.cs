using System;
using Sirius.Shared.Entities;

namespace Sirius.Data.Entities.Base
{
    public abstract class TrackedEntity : Entity, ICreatedEntity, IUpdatedEntity
    {
        public DateTime? Updated { get; set; }
    }
}
