using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        void DodajKoncert(Koncert koncert);
        [OperationContract]
        void IzmeniKoncert(int id, Koncert koncert);
        [OperationContract]
        void NapraviRezervaciju(int koncertId, int brojKarata, DateTime vreme);
        [OperationContract]
        void PlatiRezervaciju();
    }
}
