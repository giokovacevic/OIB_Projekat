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
        void DodajKoncert();
        [OperationContract]
        void IzmeniKoncert();
        [OperationContract]
        void NapraviRezervaciju();
        [OperationContract]
        void PlatiRezervaciju();
    }
}
