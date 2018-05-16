using System.Security.Principal;

namespace Monicais.DelayedStartup
{

    internal static class Privilege
    {

        public static bool IsAdminstrator()
        {
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }
    }
}
