using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Client : ChannelFactory<IService>, IService, IDisposable
    {  
    
        IService factory;

        public Client(NetTcpBinding binding, EndpointAddress address) : base(binding, address)
        {
            factory = this.CreateChannel();
        }

        public void DodajKoncert()
        {
            throw new NotImplementedException();
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

        public void Dispose()
        {
            if (this.factory != null)
            {
                this.factory = null;
            }
            this.Close();
        }
    }
}
