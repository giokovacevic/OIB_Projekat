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
        const string ADMIN = "Admin";
        const string KORISNIK = "Korisnik";
        
        public static bool isAdmin(System.Security.Principal.IIdentity identity)
        {
            string role = Manager.IdentityManager.extractRole(identity);
            if (role.Equals(ADMIN)) return true;
            return false;
        }

        public static bool isKorisnik(System.Security.Principal.IIdentity identity)
        {
            string role = Manager.IdentityManager.extractRole(identity);
            if (role.Equals(KORISNIK)) return true;
            return false;
        }
    }
}
