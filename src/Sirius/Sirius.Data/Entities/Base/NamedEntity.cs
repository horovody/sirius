using System.ComponentModel.DataAnnotations;
using Sirius.Shared.Entities;

namespace Sirius.Data.Entities.Base
{
    public class NamedEntity : TrackedEntity, INamedEntity
    {
        [Required, MaxLength(SiriusModelRestrictions.Name)]
        public string Name { get; set; }
    }
}
