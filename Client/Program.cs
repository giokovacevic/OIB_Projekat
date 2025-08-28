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
                Console.WriteLine(proxy.getUserInterface());
                
                // Dodavanje
                Console.WriteLine(" ENTER >> Dodavanje");
                Console.ReadLine();

                Koncert noviKoncert = new Koncert(10, "Jazz Group", new DateTime(2025, 9, 20), "Novi Sad", 1.0);
                proxy.DodajKoncert(noviKoncert); // normalno
                proxy.DodajKoncert(noviKoncert); // vec dodat koncert

                Koncert preraniKoncert = new Koncert(15, "DJ Early", new DateTime(2025, 6, 15), "Novi Sad", 0.5);
                proxy.DodajKoncert(preraniKoncert); // prerani

                Console.WriteLine(proxy.getUserInterface());

                // Izmene
                Console.WriteLine(" ENTER >> Izmene");
                Console.ReadLine();

                proxy.IzmeniKoncert(10, new Koncert(10, "Jazz Group", new DateTime(2025, 9, 10), "Novi Sad", 1.5)); // normalna
                proxy.IzmeniKoncert(7, new Koncert(7, "DJ Three", new DateTime(2025, 9, 13), "Novi Sad", 0.6)); // nepostojeci
                proxy.IzmeniKoncert(11, new Koncert(10, "DJ Johnson", new DateTime(2025, 10, 1), "Beograd", 0.8)); // razliciti id-ovi
                proxy.IzmeniKoncert(noviKoncert.Id, new Koncert(noviKoncert.Id, noviKoncert.Naziv, new DateTime(2025, 6, 15), noviKoncert.Lokacija, 0.9)); // prerani datum

                Console.WriteLine(proxy.getUserInterface());

                // Rezervacije
                Console.WriteLine(" ENTER >> Rezervacije");
                Console.ReadLine();

                proxy.NapraviRezervaciju(2, 10, 1); // normalna
                proxy.NapraviRezervaciju(3, 2, 3); // normalna
                proxy.NapraviRezervaciju(2, 10, 1); // vec postojeca rez
                proxy.NapraviRezervaciju(3, 7, 2); // za nepostojeci koncert

                Console.WriteLine(proxy.getUserInterface());

                // Plaćanje
                Console.WriteLine(" ENTER >> Plaćanje");
                Console.ReadLine();

                proxy.PlatiRezervaciju(2); // normalna 
                proxy.PlatiRezervaciju(2); // vec placena
                proxy.PlatiRezervaciju(1); // nedovljno sredstava ili nepostojeca
                proxy.PlatiRezervaciju(3); // nedovljno sredstava ili nepostojeca

                Console.WriteLine(proxy.getUserInterface());

                Console.WriteLine("Korisnik je završio sa radom.\n >> ENTER");
                Console.ReadLine();
            }

            Console.WriteLine(" Korisnik isključen.\n >> ENTER");
            Console.ReadLine();
        }
    }
}
