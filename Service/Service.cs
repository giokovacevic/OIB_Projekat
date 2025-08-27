using Contracts;
using SecurityManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class Service : IService
    {
        public void DodajKoncert(Koncert koncert) 
        {
            var userIdentity = ServiceSecurityContext.Current.PrimaryIdentity;
            
            if(userIdentity == null)
            {
                throw new Exception("current user null"); //TODO: Log za currentUser: null
            }

            if(AuthorizationManager.isAdmin(userIdentity))
            {
                if(!Database.koncerti.ContainsKey(koncert.Id))
                {
                    Database.koncerti.Add(koncert.Id, koncert);
                    Console.WriteLine("Koncert je dodat. (DodajKoncert)"); //TODO: Log
                }
                else
                {
                    Console.WriteLine("Koncert sa tim id-om vec postoji. (DodajKoncert)"); //TODO: Log
                }   
            }
            else
            {
                Console.WriteLine("Nije autorizovan za DodajKoncert()"); //TODO: Log
            }
        }

        public void IzmeniKoncert(int id, Koncert koncert) 
        {
            var userIdentity = ServiceSecurityContext.Current.PrimaryIdentity;

            if (userIdentity == null)
            {
                throw new Exception("current user null"); //TODO: Log za currentUser: null
            }

            if (AuthorizationManager.isAdmin(userIdentity))
            {
                if(koncert.Id == id)
                {
                    if (Database.koncerti.ContainsKey(id))
                    {
                        Database.koncerti[id] = koncert;
                        Console.WriteLine("Koncert sa je uspesno izmenjen. (IzmeniKoncert)"); //TODO: Log
                    }
                    else
                    {
                        Console.WriteLine("Koncert sa tim id-om ne postoji. (IzmeniKoncert)"); //TODO: Log
                    }
                }
                else
                {
                    Console.WriteLine("Ne moze se menjati koncert sa razlicitim id-om (IzmeniKoncert)"); //TODO: Log
                }
            }
            else
            {
                Console.WriteLine("Nije autorizovan za IzmeniKoncert(). Mora biti Admin"); //TODO: Log
            }
        }

        public void NapraviRezervaciju() // TODO: NapraviRezervaciju
        {
            throw new NotImplementedException();
        }

        public void PlatiRezervaciju() // TODO: PlatiRezervaciju
        {
            throw new NotImplementedException();
        }
    }
}
