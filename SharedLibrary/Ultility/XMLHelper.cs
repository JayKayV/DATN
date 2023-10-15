using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;

namespace SharedLibrary.Ultility
{
    public static class XMLHelper
    {
        public static XmlSchema GetSchema(string path)
        { 
            return XmlSchema.Read(new XmlTextReader(new StringReader(path)), SchemaValidationErrorCallback);
        }
        public static XmlDocument LoadUiSceneFromXML(string path)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            settings.IgnoreComments = true;

            settings.Schemas.Add(GetSchema("test"));
            settings.ValidationFlags = XmlSchemaValidationFlags.ReportValidationWarnings;
            settings.ValidationType = ValidationType.Schema;

            XmlReader xmlReader = XmlReader.Create(path, settings);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlReader);
            xmlReader.Close();

            return xmlDoc;
        }

        private static void SchemaValidationErrorCallback(object sender, ValidationEventArgs args)
        {
            if (args.Severity == XmlSeverityType.Warning)
                Console.WriteLine("\t[Warning]: Matching schema not found.  No validation occurred." + args.Message);
            else
                Console.WriteLine("\t[Validation error]: " + args.Message);
        }
    }
}
