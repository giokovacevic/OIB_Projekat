using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Manager;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string srvCertCN = "WCFService";

            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;

            X509Certificate2 srvCert = CertificateManager.GetCertificateFromStorage(StoreName.TrustedPeople, StoreLocation.LocalMachine, srvCertCN);
            EndpointAddress address = new EndpointAddress(new Uri("net.tcp://localhost:9999/Receiver"), new X509CertificateEndpointIdentity(srvCert));
            
            using (Client proxy = new Client(binding, address))
            {
                // TEST TEST TEST
                proxy.DodajKoncert();
                Console.WriteLine("DodajKoncert() finished. >> ENTER");
                
                Console.ReadLine();
            }

            Console.WriteLine(" Korisnik iskljucen: >> ENTER");
            Console.ReadLine();
        }
    }
}
