using System;

namespace Sirius.Shared.Entities
{
    public interface ICreatedEntity : IEntity
    {
        DateTime Created { get; set; }
    }
}
