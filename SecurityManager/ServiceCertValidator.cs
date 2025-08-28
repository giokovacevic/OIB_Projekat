using Manager;
using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SecurityManager
{
    public class ServiceCertValidator : X509CertificateValidator
    {
        public override void Validate(X509Certificate2 certificate)
        {
            X509Certificate2 srvCert = CertificateManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, Manager.Formatter.ParseName(WindowsIdentity.GetCurrent().Name));

            if(!srvCert.Issuer.Equals(certificate.Issuer))
            {
                Audit.neuspesnaAutentifikacija(Manager.CertificateManager.getUsernameFromCertificate((certificate)));
                throw new Exception("Failed to authenticate"); 
            }

            Audit.uspesnaAutentifikacija(Manager.CertificateManager.getUsernameFromCertificate((certificate)));
        }
    }
}
