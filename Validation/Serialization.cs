using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml.Serialization;

namespace Validation
{
    public class Serialization
    {
        public static T DerializeXML<T>(System.IO.Stream xml)
        {
            return DerializeXML<T>(xml, string.Empty);
        }
        public static T DerializeXML<T>(System.IO.Stream xml, string defaultnamespace)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T), defaultnamespace);
            System.Xml.XmlReader reader = System.Xml.XmlReader.Create(xml);
            xml.Position = 0;
            object obj = xmlSerializer.Deserialize(xml);
            return (T)obj;
        }
    }
}
