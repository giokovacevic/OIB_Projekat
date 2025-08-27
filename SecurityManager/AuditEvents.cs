using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace SecurityManager
{
    public enum AuditEventTypes
    {
        UspesnoDodavanje = 0,
        UspesnaIzmena = 1,
        NeuspesnaAutorizacija = 2,
        NeuspesnoDodavanje = 3,
        NeuspesnaIzmena = 4,
        NeuspesnaAutentifikacija = 5,
        UspesnaAutentifikacija = 6,
        NeuspesnaRezervacija = 7,
        NeuspesnoPlacanje = 8,
        UspesnaRezervacija = 9,
        UspesnoPlacanje = 10
    }

    public class AuditEvents
    {

        private static ResourceManager resourceManager = null;
        private static object resourceLock = new object();

        private static ResourceManager ResourceMgr
        {
            get
            {
                lock (resourceLock)
                {
                    if (resourceManager == null)
                    {
                        resourceManager = new ResourceManager(typeof(AuditEventsFile).ToString(), Assembly.GetExecutingAssembly());
                    }

                    return resourceManager;
                }
            }
        }

        public static string UspesnaAutentifikacija
        {
            get { return ResourceMgr.GetString(AuditEventTypes.UspesnaAutentifikacija.ToString()); }
        }

        public static string NeuspesnaAutentifikacija
        {
            get { return ResourceMgr.GetString(AuditEventTypes.NeuspesnaAutentifikacija.ToString()); }
        }

        public static string NeuspesnaAutorizacija
        {
            get { return ResourceMgr.GetString(AuditEventTypes.NeuspesnaAutorizacija.ToString()); }
        }

        public static string UspesnoDodavanje
        {
            get { return ResourceMgr.GetString(AuditEventTypes.UspesnoDodavanje.ToString()); }
        }

        public static string NeuspesnoDodavanje
        {
            get { return ResourceMgr.GetString(AuditEventTypes.NeuspesnoDodavanje.ToString()); }
        }

        public static string UspesnaIzmena
        {
            get { return ResourceMgr.GetString(AuditEventTypes.UspesnaIzmena.ToString()); }
        }

        public static string NeuspesnaIzmena
        {
            get { return ResourceMgr.GetString(AuditEventTypes.NeuspesnaIzmena.ToString()); }
        }

        public static string UspesnaRezervacija
        {
            get { return ResourceMgr.GetString(AuditEventTypes.UspesnaRezervacija.ToString()); }
        }

        public static string NeuspesnaRezervacija
        {
            get { return ResourceMgr.GetString(AuditEventTypes.NeuspesnaRezervacija.ToString()); }
        }

        public static string UspesnoPlacanje
        {
            get { return ResourceMgr.GetString(AuditEventTypes.UspesnoPlacanje.ToString()); }
        }

        public static string NeuspesnoPlacanje
        {
            get { return ResourceMgr.GetString(AuditEventTypes.NeuspesnoPlacanje.ToString()); }
        }
    }
}
