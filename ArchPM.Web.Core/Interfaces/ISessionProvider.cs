using ArchPM.Web.Core.Domain;

namespace ArchPM.Web.Core
{
    public interface ISessionProvider
    {
        AuthenticatedUserInfo AuthUser { get; }
    }
}
