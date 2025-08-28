using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityManager
{
    class RolesConfig
    {
        public const string PRMS_DODAVANJE = "Dodavanje";
        public const string PRMS_IZMENA = "Izmena";
        public const string PRMS_REZERVACIJA = "Rezervacija";
        public const string PRMS_PLACANJE = "Placanje";

        public const string ROLE_ADMIN = "Admin";
        public const string ROLE_KORISNIK = "Korisnik";

        private static Dictionary<string, string[]> roles = new Dictionary<string, string[]>();

        static RolesConfig(){
            roles[ROLE_ADMIN] = new string[] { PRMS_DODAVANJE, PRMS_IZMENA, PRMS_REZERVACIJA, PRMS_PLACANJE };
            roles[ROLE_KORISNIK] = new string[] { PRMS_REZERVACIJA, PRMS_PLACANJE };
        }
       
        public static bool getPermissions(string rolename, out string[] permissions)
        {
            return roles.TryGetValue(rolename, out permissions);
        }
    }
}
