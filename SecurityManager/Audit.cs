using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityManager
{
    public class Audit : IDisposable
    {
        private static EventLog log = null;

        const string source = "SecurityManager.Audit";
        const string name = "RezervacijeEventLogger";
        static Audit()
        {
            try
            {
                if(!EventLog.SourceExists(source))
                {
                    
                    EventLog.CreateEventSource(source, name);
                }

                log = new EventLog(name, Environment.MachineName, source);
            }
            catch (Exception exc)
            {
                log = null;
                Console.WriteLine("Error in creating a Log on your computer");
            }
        }

        public static void uspesnaAutentifikacija(string username)
        {
            if (log != null)
            {
                string message = String.Format(AuditEvents.UspesnaAutentifikacija, username);
                log.WriteEntry(message);
            }
            else
            {
                throw new ArgumentException("Error in uspesnaAutentifikacija()");
            }
        }

        public static void neuspesnaAutentifikacija(string username)
        {
            if (log != null)
            {
                string message = String.Format(AuditEvents.NeuspesnaAutentifikacija, username);
                log.WriteEntry(message);
            }
            else
            {
                throw new ArgumentException("Error in neuspesnaAutentifikacija()");
            }
        }

        public static void neuspesnaAutorizacija(string username, string operation)
        {
            if (log != null)
            {
                string message = String.Format(AuditEvents.NeuspesnaAutorizacija, username, operation);
                log.WriteEntry(message);
            }
            else
            {
                throw new ArgumentException("Error in neuspenaAutorizacija()");
            }
        }

        public static void uspesnoDodavanje(string username, int id, string name)
        {
            if (log != null)
            {
                string message = String.Format(AuditEvents.UspesnoDodavanje, username, id, name);
                log.WriteEntry(message);
            }
            else
            {
                throw new ArgumentException("Error in uspesnoDodavanje()");
            }
        }

        public static void neuspesnoDodavanje(string username, int id, string name, string reason)
        {
            if (log != null)
            {
                string message = String.Format(AuditEvents.NeuspesnoDodavanje, username, id, name, reason);
                log.WriteEntry(message);
            }
            else
            {
                throw new ArgumentException("Error in neuspesnoDodavanje()");
            }
        }

        public static void uspesnaIzmena(string username, int id, string name)
        {
            if (log != null)
            {
                string message = String.Format(AuditEvents.UspesnaIzmena, username, id, name);
                log.WriteEntry(message);
            }
            else
            {
                throw new ArgumentException("Error in uspesnaIzmena()");
            }
        }

        public static void neuspesnaIzmena(string username, int id, string name, string reason)
        {
            if (log != null)
            {
                string message = String.Format(AuditEvents.NeuspesnaIzmena, username, id, name, reason);
                log.WriteEntry(message);
            }
            else
            {
                throw new ArgumentException("Error in neuspenaIzmena()");
            }
        }

        public static void uspesnaRezervacija(string username, int brojKarata, string nazivKoncerta) 
        {
            if (log != null)
            {
                string message = String.Format(AuditEvents.UspesnaRezervacija, username, brojKarata, nazivKoncerta);
                log.WriteEntry(message);
            }
            else
            {
                throw new ArgumentException("Error in uspesnaRezervacija()");
            }
        }

        public static void neuspesnaRezervacija(string username, string nazivKoncerta, string reason) 
        {
            if (log != null)
            {
                string message = String.Format(AuditEvents.NeuspesnaRezervacija, username, nazivKoncerta, reason);
                log.WriteEntry(message);
            }
            else
            {
                throw new ArgumentException("Error in neuspenaRezervacija()");
            }
        }

        public static void uspesnoPlacanje(string username, int brojKarata, string nazivKoncerta, double iznos) 
        {
            if (log != null)
            {
                string message = String.Format(AuditEvents.UspesnoPlacanje, username, brojKarata, nazivKoncerta, iznos);
                log.WriteEntry(message);
            }
            else
            {
                throw new ArgumentException("Error in uspesnoPlacanje()");
            }
        }

        public static void neuspesnoPlacanje(string username, int brojKarata, string nazivKoncerta, string reason) 
        {
            if (log != null)
            {
                string message = String.Format(AuditEvents.NeuspesnoPlacanje, username, brojKarata, nazivKoncerta, reason);
                log.WriteEntry(message);
            }
            else
            {
                throw new ArgumentException("Error in neuspesnoPlacanje()");
            }
        }


        public void Dispose()
        {
            if(log != null) {
                log.Dispose();
                log = null;
            }
        }
    }
}
