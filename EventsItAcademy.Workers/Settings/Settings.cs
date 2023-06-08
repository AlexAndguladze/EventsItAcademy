using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EventsItAcademy.Workers.Settings
{
    public static class Settings
    {
        public static int EventBookingPeriod { get; set; }

        public static void UpdateProperties()
        {
            XDocument xdoc = XDocument.Load(@"C:\Users\Bazo\source\repos\EventsItAcademy.API\EventsItAcademy.WEB\SettingsSetByAdmin.xml");
            if (xdoc == null)
            {
                EventBookingPeriod = 1;
                return;
            }
            XNamespace ns = xdoc.Root.GetDefaultNamespace();

            XElement eventBookingPeriod = xdoc.Descendants(ns + "EventBookingPeriod").FirstOrDefault();
            if(eventBookingPeriod != null)
            {
                EventBookingPeriod = Int32.Parse(eventBookingPeriod.Value);
            }
            
        }
    }
}
