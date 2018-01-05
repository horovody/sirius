namespace Sirius.Shared.Auth
{
    /// <summary>
    /// Auth user context
    /// </summary>
    public interface IUserContext
    {
         /// <summary>
         /// User name
         /// </summary>
         string UserName { get; }

         /// <summary>
         /// Display Name
         /// </summary>
         string GivenName { get; }

         /// <summary>
         /// User Uid
         /// </summary>
         string UserId { get; }

         /// <summary>
         /// Email
         /// </summary>
         string Email { get; }
    } 
}
