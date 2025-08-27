using Contracts;
using Manager;
using SecurityManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal class Program
    {
        static void Main(string[] args)
        {
			string srvCertCN = Manager.Formatter.ParseName(WindowsIdentity.GetCurrent().Name);
            
            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;

            string address = "net.tcp://localhost:9999/Receiver";
            ServiceHost host = new ServiceHost(typeof(Service));
            host.AddServiceEndpoint(typeof(IService), binding, address);

            host.Credentials.ClientCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.Custom;
            host.Credentials.ClientCertificate.Authentication.CustomCertificateValidator = new ServiceCertValidator();

            host.Credentials.ClientCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

            host.Credentials.ServiceCertificate.Certificate = CertificateManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, srvCertCN);

            ServiceSecurityAuditBehavior audit = new ServiceSecurityAuditBehavior();
            audit.AuditLogLocation = AuditLogLocation.Application;
            audit.ServiceAuthorizationAuditLevel = AuditLevel.SuccessOrFailure;

            host.Description.Behaviors.Remove<ServiceSecurityAuditBehavior>();
            
            host.Description.Behaviors.Add(audit);

            if (host.Credentials.ServiceCertificate.Certificate == null)
            {
                Console.WriteLine(" Service started from Wrong Machine. (WCFService expected)\n >> ENTER");
                Console.ReadLine();
            }
            else
            {
                try
                {
                    host.Open();
                    Console.WriteLine("WCFService is started.\nPress <enter> to stop ...");
                    Console.ReadLine();
                }
                catch (Exception e)
                {
                    Console.WriteLine("[ERROR] {0}", e.Message);
                    Console.WriteLine("[StackTrace] {0}", e.StackTrace);
                }
                finally
                {
                    host.Close();
                }
            }

            Console.WriteLine(" Service Stopped.\n >> ENTER");
            Console.ReadLine();
        }
    }
}
