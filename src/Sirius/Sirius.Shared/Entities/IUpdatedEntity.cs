using System;

namespace Sirius.Shared.Entities
{
    public interface IUpdatedEntity : IEntity
    {
        DateTime? Updated { get; set; }
    }
}
