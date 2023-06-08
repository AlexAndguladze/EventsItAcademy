using System.Xml.Linq;

namespace EventsItAcademy.WEB.infrastructure.StaticItems
{
    public static class SettingsSetByAdmin
    {
        public static int GetEventEditLimitDays()
        {
            XDocument xdoc = XDocument.Load("SettingsSetByAdmin.xml");
            if (xdoc == null)
            {
                return 1;
            }
            XNamespace ns = xdoc.Root.GetDefaultNamespace();

            XElement eventEditLimitDays = xdoc.Descendants(ns + "EventEditLimitDays").FirstOrDefault();
            return int.Parse(eventEditLimitDays.Value);
        }
    }
}
