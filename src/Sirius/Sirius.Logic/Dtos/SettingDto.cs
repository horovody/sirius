using System.ComponentModel.DataAnnotations;
using Sirius.Logic.Dtos.Base;

namespace Sirius.Logic.Dtos
{
    public class SettingDto: NamedDto
    {
        [Required]
        public string Alias { get; set; }

        [Required]
        public string Value { get; set; }
    }
}
