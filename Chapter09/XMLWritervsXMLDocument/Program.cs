using System;
using System.Xml;
using System.Text;

namespace XMLWritervsXMLDocument
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("XmlWriter vs XmlDocument");

            UseXmlWriter();

            UseXmlDocument();

            UseXmlDocumentForUpdate();

        }

        public static void UseXmlWriter()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            
            XmlWriter xmlWriter = XmlWriter.Create("test_xmlwriter.xml", settings);

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("users");

            xmlWriter.WriteStartElement("user");
            xmlWriter.WriteAttributeString("age", "42");
            xmlWriter.WriteString("John Doe");
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("user");
            xmlWriter.WriteAttributeString("age", "39");
            xmlWriter.WriteString("Jane Doe");
            xmlWriter.WriteEndElement();
            
            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
        }        
    
        public static void UseXmlDocument()
        {
            // more object oriented than the XmlWriter approach
            // have to write a bit more code for this (but less if doing an update to a document)
            // but uses more memory. 

            XmlDocument xmlDoc = new XmlDocument();
            XmlNode rootNode = xmlDoc.CreateElement("users");
            xmlDoc.AppendChild(rootNode);

            XmlNode userNode = xmlDoc.CreateElement("user");
            XmlAttribute attribute = xmlDoc.CreateAttribute("age");
            attribute.Value = "42";
            userNode.Attributes.Append(attribute);
            userNode.InnerText = "John Doe";
            rootNode.AppendChild(userNode);

            userNode = xmlDoc.CreateElement("user");
            attribute = xmlDoc.CreateAttribute("age");
            attribute.Value = "39";
            userNode.Attributes.Append(attribute);
            userNode.InnerText = "Jane Doe";
            rootNode.AppendChild(userNode);

            xmlDoc.Save("test_xmlDocument.xml");
        }

        public static void UseXmlDocumentForUpdate()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("test_xmlDocument.xml");
            XmlNodeList userNodes = xmlDoc.SelectNodes("//users/user");
            foreach(XmlNode userNode in userNodes)
            {
                int age = int.Parse(userNode.Attributes["age"].Value);
                userNode.Attributes["age"].Value = (age + 1).ToString();
            }
            xmlDoc.Save("test_xmlDocument.xml");       
        }
    }


}
