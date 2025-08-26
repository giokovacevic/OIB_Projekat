using Contracts;
using Manager;
using SecurityManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Client : ChannelFactory<IService>, IService, IDisposable
    {  
    
        IService factory;

        public Client(NetTcpBinding binding, EndpointAddress address) : base(binding, address)
        {
            string cltCertCN = Manager.Formatter.ParseName(WindowsIdentity.GetCurrent().Name);

            this.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.Custom;
            this.Credentials.ServiceCertificate.Authentication.CustomCertificateValidator = new ClientCertValidator();
            this.Credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

            this.Credentials.ClientCertificate.Certificate = CertificateManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, cltCertCN);
            
            factory = this.CreateChannel();
        }

        public void DodajKoncert()
        {
            factory.DodajKoncert();
        }

        public void IzmeniKoncert()
        {
            throw new NotImplementedException();
        }

        public void NapraviRezervaciju()
        {
            throw new NotImplementedException();
        }

        public void PlatiRezervaciju()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            if (this.factory != null)
            {
                this.factory = null;
            }
            this.Close();
        }
    }
}
