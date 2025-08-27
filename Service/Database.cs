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
            Koncert k1 = new Koncert(1, "Oasis", DateTime.Now, "Liverpool", 5.0);
            Koncert k2 = new Koncert(2, "Oasis", DateTime.Now, "Manchester", 4.0);
            Koncert k3 = new Koncert(3, "Halid Beslic", DateTime.Now, "Sarajevo", 3.0);

            koncerti.Add(k1.Id, k1);
            koncerti.Add(k2.Id, k2);
            koncerti.Add(k3.Id, k3);

            Korisnik adminko = new Korisnik("Adminko", 25.0);
            Korisnik ogi = new Korisnik("Ogi", 47.0);
            
            korisnici.Add(adminko.Id, adminko);
            korisnici.Add(ogi.Id, ogi);
        }
    }
}
