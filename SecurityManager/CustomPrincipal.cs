using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SecurityManager
{
    public class CustomPrincipal : IPrincipal
    {
        private readonly IIdentity identity;
        private readonly string username;
        private readonly string role;

        public CustomPrincipal(IIdentity identity, string username = null, string role = null)
        {
            this.identity = identity;
            this.username = username;

            if (username != null)
            {
                this.username = username;
            }
            else
            {
                this.username = "";
            }

            if (role != null) {
                this.role = role;
            }
            else
            {
                this.role = "";
            }
        }
        public IIdentity Identity
        {
            get { return this.identity;  }
        }

        public string Username
        {
            get { return this.username; }
        }

        public bool IsInRole(string permission)
        {
            string[] permissions;
            if(RolesConfig.getPermissions(this.role, out permissions))
            {
                foreach(string p in permissions)
                {
                    if(p.Equals(permission))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
