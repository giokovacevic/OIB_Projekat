using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    [DataContract]
    public class Korisnik
    {
        readonly string id;
        double racun;
        Dictionary<int, Rezervacija> rezervacije = new Dictionary<int, Rezervacija>();

        public Korisnik(string id, double racun)
        {
            this.id = id;
            this.racun = racun;
        }

        [DataMember]
        public string Id { get => id; }
        [DataMember]
        public double Racun { get => racun; set => racun = value; }
        [DataMember]
        public Dictionary<int, Rezervacija> Rezervacije { get => rezervacije; set => rezervacije = value; }

        public override string ToString()
        {
            string str = "";
            str = str + id + "  račun: " + racun + "\n---------\n";
            foreach(var rezervacija in rezervacije.Values)
            {
                str = str + rezervacija.ToString();
            }
            return str;
        }
    }
}
