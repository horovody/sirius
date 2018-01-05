using System;
using System.Security.Claims;
using System.Threading;
using JetBrains.Annotations;

namespace Sirius.Shared.Auth
{
    public class ClaimsUserContext: IUserContext
    {
        private readonly ClaimsPrincipal _principal;

        private readonly Lazy<string> _userName;
        private readonly Lazy<string> _givenName;
        private readonly Lazy<string> _userId;
        private readonly Lazy<string> _email;

        /// <summary>
        /// Create user context
        /// </summary>
        /// <param name="principal">
        /// Standart user clincipal /w claims
        /// </param>
        public ClaimsUserContext([NotNull] ClaimsPrincipal principal)
        {
          _principal = principal ?? throw new ArgumentNullException(nameof(principal));

          _userName = new Lazy<string>(this.GetUserName, LazyThreadSafetyMode.None);
          _userId = new Lazy<string>(this.GetUserId, LazyThreadSafetyMode.None);
          _givenName = new Lazy<string>(this.GetGivenName, LazyThreadSafetyMode.None);
          _email = new Lazy<string>(this.GetEmail, LazyThreadSafetyMode.None);
        }


        #region Extract claims

        private string GetUserName()
        {
          var userNameClaim = _principal
            .FindFirst(p => p.Type == ClaimTypes.Name);
          return userNameClaim?.Value;
        }

        private string GetUserId()
        {
          var userIdClaim = _principal
            .FindFirst(p => p.Type == ClaimTypes.NameIdentifier);
          var idValue = userIdClaim?.Value;
          if (idValue == null)
            throw new SiriusException("Принципал не содержит идентификатора пользователя.");
          return idValue;
        }

        private string GetGivenName()
        {
          var givenName = _principal
            .FindFirst(p => p.Type == ClaimTypes.GivenName);
          return givenName?.Value;
        }

        private string GetEmail()
        {
          var email = _principal
            .FindFirst(p => p.Type == ClaimTypes.Email);
          return email?.Value;
        }

        #endregion

        #region Implementation of IUserContext

        /// <inheritdoc />
        public string UserName => _userName.Value;

        /// <inheritdoc />
        public string GivenName => _givenName.Value;

        /// <inheritdoc />
        public string UserId => _userId.Value;

        /// <inheritdoc />
        public string Email => _email.Value;

        #endregion
  }
}
