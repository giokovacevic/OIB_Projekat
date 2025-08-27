using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    public class IdentityManager
    {
        public static string extractUsername(System.Security.Principal.IIdentity identity)
        {
            string identityData = identity.Name.Split(';')[0];
            string[] identityDataParts = identityData.Split(',');
            if (identityDataParts[0].Trim().StartsWith("CN="))
            {
                return identityDataParts[0].Trim().Substring(3);
            }
            else
            {
                throw new InvalidOperationException("Invalid CN in SubjectName"); // TODO: Log?
            }
        }
        public static string extractRole(System.Security.Principal.IIdentity identity) 
        {
            string identityData = identity.Name.Split(';')[0];
            string[] identityDataParts = identityData.Split(',');
            if (identityDataParts[1].Trim().StartsWith("OU="))
            {
                return identityDataParts[1].Trim().Substring(3);
            }
            else
            {
                return "";
            }
        }
    }
}
