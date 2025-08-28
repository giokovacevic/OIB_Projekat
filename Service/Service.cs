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

            string username = Manager.IdentityManager.extractUsername(userIdentity);

            if (AuthorizationManager.isAdmin(userIdentity))
            {
                if(!Database.koncerti.ContainsKey(koncert.Id))
                {
                    if(!(koncert.VremePocetka <= DateTime.Now))
                    {
                        Database.koncerti.Add(koncert.Id, koncert);
                        Audit.uspesnoDodavanje(username, koncert.Id, koncert.Naziv);
                    }
                    else
                    {
                        Audit.neuspesnoDodavanje(username, koncert.Id, koncert.Naziv, "Vreme početka nije validno.");
                    }  
                }
                else
                {
                    Audit.neuspesnoDodavanje(username, koncert.Id, koncert.Naziv, "Koncert već postoji.");
                }   
            }
            else
            {
                Audit.neuspesnaAutorizacija(username, "Dodavanje koncerta.");
            }
        }

        public void IzmeniKoncert(int id, Koncert koncert) 
        {
            var userIdentity = ServiceSecurityContext.Current.PrimaryIdentity;

            if (userIdentity == null)
            {
                throw new Exception("current user was null"); 
            }

            string username = Manager.IdentityManager.extractUsername(userIdentity);

            if (AuthorizationManager.isAdmin(userIdentity))
            {
                if(koncert.Id == id)
                {
                    if (Database.koncerti.ContainsKey(id))
                    {
                        if (!(koncert.VremePocetka <= DateTime.Now))
                        {
                            Database.koncerti[id] = koncert;
                            Audit.uspesnaIzmena(username, koncert.Id, koncert.Naziv);
                        }
                        else
                        {
                            Audit.neuspesnaIzmena(username, koncert.Id, koncert.Naziv, "Vreme početka nije validno.");
                        } 
                    }
                    else
                    {
                        Audit.neuspesnaIzmena(username, koncert.Id, koncert.Naziv, "Nepostojeći koncert");
                    }
                }
                else
                {
                    Audit.neuspesnaIzmena(username, koncert.Id, koncert.Naziv, "Nedozvoljena izmena Id-a");
                }
            }
            else
            {
                Audit.neuspesnaAutorizacija(username, "Izmena koncerta.");
            }
        }

        public void NapraviRezervaciju(int id, int koncertId, int brojKarata)
        {
            var userIdentity = ServiceSecurityContext.Current.PrimaryIdentity;

            if (userIdentity == null)
            {
                throw new Exception("current user was null"); 
            }

            string username = Manager.IdentityManager.extractUsername(userIdentity);

            if(AuthorizationManager.isAdmin(userIdentity) || AuthorizationManager.isKorisnik(userIdentity))
            {

                if (Database.koncerti.ContainsKey(koncertId)) // nepostojeci koncert
                {
                    if (!Database.korisnici[username].Rezervacije.ContainsKey(id)) // vec postoji taj id
                    {
                        Database.korisnici[username].Rezervacije.Add(id, new Rezervacija(id, koncertId, DateTime.Now, brojKarata, StanjeRezervacije.POTREBNO_PLATITI));
                       
                        Audit.uspesnaRezervacija(username, brojKarata, Database.koncerti[koncertId].Naziv);
                    }
                    else
                    {
                        Audit.neuspesnaRezervacija(username, Database.koncerti[koncertId].Naziv, "Rezervacija tog Id-a već postoji.");
                    }
                }
                else
                {
                    Audit.neuspesnaRezervacija(username, "?", "Nepostojeći koncert.");
                }
                
            }
            else
            {
                Audit.neuspesnaAutorizacija(username, "Rezervacija za koncert");
            }
        }

        public void PlatiRezervaciju(int id) 
        {
            var userIdentity = ServiceSecurityContext.Current.PrimaryIdentity;

            if (userIdentity == null)
            {
                throw new Exception("current user was null");
            }

            string username = Manager.IdentityManager.extractUsername(userIdentity);

            if (AuthorizationManager.isAdmin(userIdentity) || AuthorizationManager.isKorisnik(userIdentity))
            {
                if (Database.korisnici[username].Rezervacije.ContainsKey(id))
                {
                    Rezervacija rezervacija = Database.korisnici[username].Rezervacije[id];
                    if (rezervacija.Stanje == StanjeRezervacije.POTREBNO_PLATITI)
                    {
                        double potrebanIznos = rezervacija.KolicinaKarata * Database.koncerti[rezervacija.IdKoncerta].CenaKarte;

                        Korisnik korisnik = Database.korisnici[username];
                        
                        if((korisnik.Racun - potrebanIznos) < 0)
                        {
                            Audit.neuspesnoPlacanje(username, Database.koncerti[rezervacija.IdKoncerta].Naziv, "Nedovoljno sredstava na računu.");
                        }
                        else
                        {
                            korisnik.Racun = korisnik.Racun - potrebanIznos;
                            rezervacija.Stanje = StanjeRezervacije.PLACENA;
                            
                            Audit.uspesnoPlacanje(username, Database.koncerti[rezervacija.IdKoncerta].Naziv, potrebanIznos);
                        }
                    }
                    else
                    {
                        Audit.neuspesnoPlacanje(username, Database.koncerti[rezervacija.IdKoncerta].Naziv, "Rezervacija je već plaćena.");
                    }
                }
                else
                {
                    Audit.neuspesnoPlacanje(username, "?", "Nepostojeća rezervacija.");
                }
            }
            else
            {
                Audit.neuspesnaAutorizacija(Manager.IdentityManager.extractUsername(userIdentity), "Plaćanje Rezervacije");
            }
        }

        public string getUserInterface()
        {
            string str = "";
            foreach(Koncert koncert in Database.koncerti.Values)
            {
                str = str + koncert.ToString();
            }

            str = str + "\n";
            foreach (Korisnik korisnik in Database.korisnici.Values)
            {
                str = str + korisnik.ToString();
            }

            return str;
        }
    }
}
