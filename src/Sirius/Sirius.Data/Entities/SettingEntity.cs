using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sirius.Data.Entities.Base;

namespace Sirius.Data.Entities
{
    [Table("Settings")]
    public class SettingEntity: NamedEntity
    {
        [Required]
        [MaxLength(SiriusModelRestrictions.UniqueName)]
        public string Alias { get; set; }

        [MaxLength(SiriusModelRestrictions.Value)]
        public string Value { get; set; }
    }
}
