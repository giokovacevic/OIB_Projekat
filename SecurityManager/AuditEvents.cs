using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityManager
{
    public class AuditEvents
    {
        internal const string USPESNA_AUTENTIFIKACIJA = "Korisnik {0} se uspešno autentifikovao.";
        internal const string NEUSPESNA_AUTENTIFIKACIJA = "Korisnik {0} nije prošao autentifikaciju.";
        
        internal const string NEUSPESNA_AUTORIZACIJA = "Korisnik {0} nije autorizovan za operaciju: {1}";

        internal const string USPESNO_DODAVANJE = "Korisnik {0} je uspešno dodao koncert za id: {1} Naziv: {2}";
        internal const string NEUSPESNO_DODAVANJE = "Korisnik {0} nije dodao koncert za id: {1} Naziv: {2} | Razlog: {3}";

        internal const string USPESNA_IZMENA = "Korisnik {0} je uspešno izmenio koncert za id: {1} Naziv: {2}";
        internal const string NEUSPESNA_IZMENA = "Korisnik {0} nije izmenio koncert za id: {1} Naziv: {2} | Razlog: {3}";

        internal const string USPESNA_REZERVACIJA = ""; // TODO: popuni
        internal const string NEUSPESNA_REZERVACIJA = ""; // TODO: popuni

        internal const string USPESNO_PLACANJE = ""; // TODO: popuni
        internal const string NEUSPESNO_PLACANJE = ""; // TODO: popuni
    }
}
