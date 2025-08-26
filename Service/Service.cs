using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class Service : IService
    {
        // users/
        // concerts 
        // reservations
        public void DodajKoncert() // TODO: dodajKoncert
        {
            Console.WriteLine("KONCERT USPESNO DODAT!!!");
        }

        public void IzmeniKoncert()
        {
            throw new NotImplementedException();
        }

        public void NapraviRezervaciju()
        {
            throw new NotImplementedException();
        }

        public void PlatiRezervaciju()
        {
            throw new NotImplementedException();
        }
    }
}
