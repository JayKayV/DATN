using System;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Schema;

namespace SharedLibrary.Ultility
{
    public static class XMLHelper
    {
        public static XmlSchema GetSchema(string path)
        { 
            if (path == null)
                throw new ArgumentNullException("[Warning]: path is null");
            XmlTextReader reader = new XmlTextReader(path);
            return XmlSchema.Read(reader, SchemaValidationErrorCallback);
        }
        public static XmlDocument LoadScene(string path)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            settings.IgnoreComments = true;

            settings.ValidationFlags = XmlSchemaValidationFlags.ReportValidationWarnings;
            settings.ValidationType = ValidationType.Schema;

            XmlReader xmlReader = XmlReader.Create(path, settings);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Schemas.Add(@"http://testgame.com", "C:\\Users\\PC\\source\\repos\\DATN\\SharedLibrary\\Scene\\scene_schema.xsd");
            xmlDoc.Load(xmlReader);
            try
            {
                xmlDoc.Validate(null);
            } catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            } finally {
                xmlReader.Close(); 
            }

            return xmlDoc;
        }

        private static void SchemaValidationErrorCallback(object sender, ValidationEventArgs args)
        {
            if (args.Severity == XmlSeverityType.Warning)
                Console.WriteLine("\t[Warning]: Matching schema not found.  No validation occurred." + args.Message);
            else
                Console.WriteLine("\t[Validation error]: " + args.Message);
        }

        public static string? GetAttribute(XmlAttributeCollection collection, string name, string defaultValue = null, bool required = true)
        {
            if (collection.GetNamedItem(name) == null)
            {
                if (required)
                    throw new ArgumentException("Require attribute named: " + name);
                return defaultValue;
            }
            return collection.GetNamedItem(name).Value;
        }
    }
}
