using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class Database 
    {
        internal static Dictionary<string, Korisnik> korisnici = new Dictionary<string, Korisnik>();
        internal static Dictionary<int, Koncert> koncerti = new Dictionary<int, Koncert>();

        static Database() {
            Koncert k1 = new Koncert(1, "Oasis", new DateTime(2025, 9, 2), "Liverpul", 5.0);
            Koncert k2 = new Koncert(2, "Halid Beslic", new DateTime(2025, 9, 12), "Sarajevo", 2.0);
            Koncert k3 = new Koncert(3, "Daft Punk", new DateTime(2025, 9, 21), "Pariz", 3.0);
            Koncert k4 = new Koncert(4, "ABBA", new DateTime(2025, 9, 6), "Berlin", 4.0);

            koncerti.Add(k1.Id, k1);
            koncerti.Add(k2.Id, k2);
            koncerti.Add(k3.Id, k3);
            koncerti.Add(k4.Id, k4);

            Korisnik adminko = new Korisnik("Adminko", 25.0);
            Korisnik ogi = new Korisnik("Ogi", 6.0);
            
            korisnici.Add(adminko.Id, adminko);
            korisnici.Add(ogi.Id, ogi);

            Rezervacija r1 = new Rezervacija(1, 1, new DateTime(2025, 8, 25), 1, StanjeRezervacije.POTREBNO_PLATITI);
            korisnici["Ogi"].Rezervacije.Add(r1.Id, r1);
        }
    }
}
