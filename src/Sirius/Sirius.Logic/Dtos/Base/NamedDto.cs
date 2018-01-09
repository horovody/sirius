using System.ComponentModel.DataAnnotations;

namespace Sirius.Logic.Dtos.Base
{
    public class NamedDto : DtoBase
    {
        [Required]
        public string Name { get; set; }
    }
}
