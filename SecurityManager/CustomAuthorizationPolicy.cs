using System;
using System.Collections.Generic;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SecurityManager
{
    public class CustomAuthorizationPolicy : IAuthorizationPolicy
    {
        public CustomAuthorizationPolicy()
        {
            Id = Guid.NewGuid().ToString();
        }

        public ClaimSet Issuer
        {
            get { return ClaimSet.System; }
        }
        public string Id
        {
            get;
        }

        public bool Evaluate(EvaluationContext evaluationContext, ref object state)
        {
            if (!evaluationContext.Properties.TryGetValue("Identities", out object list)) return false;

            var identities = list as IList<IIdentity>;
            if (identities == null || identities.Count == 0)
                return false;

            IIdentity identity = identities[0];

            string role = IdentityManager.extractRole(identity);
            string username = IdentityManager.extractUsername(identity);

            CustomPrincipal principal = new CustomPrincipal(identity, username, role);
            evaluationContext.Properties["Principal"] = principal;
            Thread.CurrentPrincipal = principal;

            return true;
        }
    }
}
