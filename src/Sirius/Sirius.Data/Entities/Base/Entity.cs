using System;
using Sirius.Shared.Entities;

namespace Sirius.Data.Entities.Base
{
    /// <summary>
    /// Base entity class
    /// </summary>
    public abstract class Entity : IEntity, ICreatedEntity
    {
        /// <summary>
        /// Identity.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Is entity deleted.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Creation datetime
        /// </summary>
        public DateTime Created { get; set; }
    }
}
