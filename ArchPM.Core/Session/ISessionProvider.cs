using ArchPM.Web.Core.Domain;

namespace ArchPM.Core.Session
{
    public interface ISessionProvider
    {
        AuthenticatedUserInfo AuthUser { get; }
    }
}
