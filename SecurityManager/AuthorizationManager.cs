using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SecurityManager
{
    public class AuthorizationManager
    {
        public static bool isAdmin(System.Security.Principal.IIdentity identity) // TODO: isAdmin
        {
            string role = Manager.IdentityManager.extractRole(identity);
            if (role.Equals("Admin")) return true;
            return false;
        }

        public static bool isKorisnik(System.Security.Principal.IIdentity identity) // TODO: isKorisnik
        {
            string role = Manager.IdentityManager.extractRole(identity);
            if (role.Equals("Korisnik")) return true;
            return false;
        }
    }
}
