using System;

namespace Sirius.Shared.Entities
{
    public interface IEntity
    {
        Guid Id { get; set; }

        bool IsDeleted { get; set; }
    }
}
