namespace ArchPM.Core.Session
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISessionProvider
    {
        /// <summary>
        /// Gets the authentication user.
        /// </summary>
        /// <value>
        /// The authentication user.
        /// </value>
        AuthenticatedUserInfo AuthUser { get; }
    }
}
