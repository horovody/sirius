using System;
using Sirius.Shared.Entities;

namespace Sirius.Data.Entities.Base
{
    /// <summary>
    /// Base entity class
    /// </summary>
    public abstract class Entity : IEntity, ICreatedEntity
    {
        private Guid? _entityUid;

        /// <summary>
        /// Identity.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Is entity deleted.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Unique record key
        /// </summary>
        public Guid EntityUid
        {
            get { return _entityUid ?? (_entityUid = Guid.NewGuid()).Value; }
            set { _entityUid = value; }
        }

        /// <summary>
        /// Creation datetime
        /// </summary>
        public DateTime Created { get; set; }
    }
}
