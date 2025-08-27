using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    public class CertificateManager
    {
        public static X509Certificate2 GetCertificateFromStorage(StoreName storeName, StoreLocation storeLocation, string subjectName)
        {
            X509Store store = new X509Store(storeName, storeLocation);
            store.Open(OpenFlags.ReadOnly);

            X509Certificate2Collection certCollection = store.Certificates.Find(X509FindType.FindBySubjectName, subjectName, true);

            foreach (X509Certificate2 c in certCollection)
            {
                if (c.SubjectName.Name.Contains(','))
                {
                    string[] nameArray = c.SubjectName.Name.Split(',');
                    if (nameArray[0].Equals(string.Format("CN={0}", subjectName)))
                    {
                        return c;
                    }
                }
                else
                {
                    if (c.SubjectName.Name.Equals(string.Format("CN={0}", subjectName)))
                    {
                        return c;
                    }
                }
                
            }

            return null;
        }

        public static string getUsernameFromCertificate(X509Certificate2 certificate)
        {
            String[] nameArray = certificate.SubjectName.Name.Split(',');
            return nameArray[0].Trim().Substring(3);
        }
    }
}
