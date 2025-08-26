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

            /// Check whether the subjectName of the certificate is exactly the same as the given "subjectName"
            foreach (X509Certificate2 c in certCollection)
            {
                Console.WriteLine(c.SubjectName.Name);
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
    }
}
