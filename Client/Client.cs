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

        public void DodajKoncert(Koncert koncert)
        {
            try
            {
                factory.DodajKoncert(koncert);
            }
            catch(Exception exc)
            {
                Console.WriteLine("DodajKoncert nije uspeo");
            }
        }

        public void IzmeniKoncert(int id, Koncert koncert)
        {
            try
            {
                factory.IzmeniKoncert(id, koncert);
            }
            catch (Exception exc)
            {
                Console.WriteLine("IzmeniKoncert nije uspeo: " + exc.Message);
            }
        }

        public void NapraviRezervaciju(int id, int koncertId, int brojKarata)
        {
            try
            {
                factory.NapraviRezervaciju(id, koncertId, brojKarata);
            }
            catch (Exception exc)
            {
                Console.WriteLine("NapraviRezervaciju nije uspeo: " + exc.Message);
            }
        }

        public void PlatiRezervaciju(int id) 
        {
            try
            {
                factory.PlatiRezervaciju(id);
            }
            catch (Exception exc)
            {
                Console.WriteLine("PlatiRezervaciju nije uspeo: " + exc.Message);
            }
        }

        public string getUserInterface()
        {
            try
            {
                return factory.getUserInterface();
            }
            catch (Exception exc)
            {
                Console.WriteLine("printDatabase nije uspeo: " + exc.Message);
            }

            return "";
        }

        public void Dispose()
        {
            try
            {
                if (this.factory != null)
                {
                    this.factory = null;
                }
                this.Close();
            }
            catch(CommunicationObjectFaultedException exc)
            {
            }
            
        }
    }
}
