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
                throw new Exception("current user was null"); 
            }

            if(AuthorizationManager.isAdmin(userIdentity))
            {
                if(!Database.koncerti.ContainsKey(koncert.Id))
                {
                    Database.koncerti.Add(koncert.Id, koncert);
                    Audit.uspesnoDodavanje(Manager.IdentityManager.extractUsername(userIdentity), koncert.Id, koncert.Naziv);
                    
                    Console.WriteLine("Koncert je dodat. (DodajKoncert)"); //TODO: IZBRISI
                }
                else
                {
                    Audit.neuspesnoDodavanje(Manager.IdentityManager.extractUsername(userIdentity), koncert.Id, koncert.Naziv, "Koncert već postoji.");
                    
                    Console.WriteLine("Koncert sa tim id-om vec postoji. (DodajKoncert)"); //TODO: IZBRISI
                }   
            }
            else
            {
                Audit.neuspesnaAutorizacija(Manager.IdentityManager.extractUsername(userIdentity), "Dodavanje koncerta.");
                
                Console.WriteLine("Nije autorizovan za DodajKoncert()"); //TODO: IZBRISI
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
                        Audit.uspesnaIzmena(Manager.IdentityManager.extractUsername(userIdentity), koncert.Id, koncert.Naziv);

                        Console.WriteLine("Koncert sa je uspesno izmenjen. (IzmeniKoncert)"); //TODO: IZBRISI
                    }
                    else
                    {
                        Audit.neuspesnaIzmena(Manager.IdentityManager.extractUsername(userIdentity), koncert.Id, koncert.Naziv, "Nepostojeći koncert");

                        Console.WriteLine("Koncert sa tim id-om ne postoji. (IzmeniKoncert)"); //TODO: IZBRISI
                    }
                }
                else
                {
                    Audit.neuspesnaIzmena(Manager.IdentityManager.extractUsername(userIdentity), koncert.Id, koncert.Naziv, "Nedozvoljena izmena Id-a");
                    
                    Console.WriteLine("Ne moze se menjati koncert sa razlicitim id-om (IzmeniKoncert)"); //TODO: IZBRISI
                }
            }
            else
            {
                Audit.neuspesnaAutorizacija(Manager.IdentityManager.extractUsername(userIdentity), "Izmena koncerta.");

                Console.WriteLine("Nije autorizovan za IzmeniKoncert(). Mora biti Admin"); //TODO: IZBRISI
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
