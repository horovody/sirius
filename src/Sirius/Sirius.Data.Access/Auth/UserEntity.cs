using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Sirius.Data.Access.Auth
{
    public class UserEntity: IdentityUser
    {
        /// <summary>
        /// Display name
        /// </summary>
        [MaxLength(512)]
        public string GivenName { get; set; }
    }
}
