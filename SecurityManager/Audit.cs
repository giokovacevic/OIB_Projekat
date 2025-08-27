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
                string message = String.Format(AuditEvents.USPESNA_AUTENTIFIKACIJA, username);
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
                string message = String.Format(AuditEvents.NEUSPESNA_AUTENTIFIKACIJA, username);
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
                string message = String.Format(AuditEvents.NEUSPESNA_AUTORIZACIJA, username, operation);
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
                string message = String.Format(AuditEvents.USPESNO_DODAVANJE, username, id, name);
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
                string message = String.Format(AuditEvents.NEUSPESNO_DODAVANJE, username, id, name, reason);
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
                string message = String.Format(AuditEvents.USPESNA_IZMENA, username, id, name);
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
                string message = String.Format(AuditEvents.NEUSPESNA_IZMENA, username, id, name, reason);
                log.WriteEntry(message);
            }
            else
            {
                throw new ArgumentException("Error in neuspenaIzmena()");
            }
        }

        // TODO: jos 4 2*2


        public void Dispose()
        {
            if(log != null) {
                log.Dispose();
                log = null;
            }
        }
    }
}
