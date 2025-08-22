using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    [DataContract]
    public enum StanjeRezervacije 
    {
        [EnumMember]
        POTREBNO_PLATITI,
        [EnumMember]
        PLACENA
    }

    [DataContract]
    public class Rezervacija
    {
        int id;
        int idKoncerta;
        DateTime vremeRezervacije;
        int kolicinaKarata;
        StanjeRezervacije stanje;

        public Rezervacija(int id, int idKoncerta, DateTime vremeRezervacije, int kolicinaKarata, StanjeRezervacije stanje) 
        {
            this.id = id;
            this.idKoncerta = idKoncerta;
            this.vremeRezervacije = vremeRezervacije;
            this.kolicinaKarata = kolicinaKarata;
            this.stanje = stanje;
        }

        [DataMember]
        public int Id { get => id; set => id = value; }
        [DataMember]
        public int IdKoncerta { get => idKoncerta; set => idKoncerta = value; }
        [DataMember]
        public DateTime VremeRezervacije { get => vremeRezervacije; set => vremeRezervacije = value; }
        [DataMember]
        public int KolicinaKarata { get => kolicinaKarata; set => kolicinaKarata = value; }
        [DataMember]
        public StanjeRezervacije Stanje { get => stanje; set => stanje = value; }
    }
}
