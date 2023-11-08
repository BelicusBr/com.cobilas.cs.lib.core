using System;
using System.IO;
using System.Xml;
using System.Text;
using Cobilas.Collections;

namespace com.cobilas.cs.lib.core.test {
    class Program {
        static string pt1 => @"C:\Users\Cobilas portable\Desktop\Projetos CSharp\Bibliotecas\com.cobilas.cs.lib.core\com.cobilas.cs.lib.core.test\tds.xml";
        static string pt2 => @"C:\Users\Cobilas portable\Desktop\Projetos CSharp\Bibliotecas\com.cobilas.cs.lib.core\com.cobilas.cs.lib.core.test\tds2.xml";
        static string pt3 => @"C:\Users\Cobilas portable\Desktop\Projetos CSharp\Bibliotecas\com.cobilas.cs.lib.core\com.cobilas.cs.lib.core.test\tds3.xml";
        
        static void Main(string[] args) {
            StringBuilder stream = new();
            XmlWriterSettings settings = new();
            settings.Indent = true;
            settings.IndentChars = "\t";
            settings.NewLineChars = "\r\n";
            settings.OmitXmlDeclaration = false;
            
            using (XmlWriter writer = XmlWriter.Create(stream, settings)) {
                writer.WriteStartDocument();
                if (settings.Indent)
                    if (!settings.OmitXmlDeclaration)
                        writer.WriteWhitespace(settings.NewLineChars);
                XMLIRWElement element = new("Root",
                    new XMLIRWElement("id1", "valor1", XmlNodeType.Attribute),
                    new XMLIRWElement("id2", "valor2", XmlNodeType.Attribute),
                    new XMLIRWElement("ele1", "valor1", XmlNodeType.Element, 
                        new XMLIRWElement("id1", "valor1", XmlNodeType.Attribute),
                        new XMLIRWElement("id2", "valor2", XmlNodeType.Attribute),
                        new XMLIRWElement("ele2", "valor2", XmlNodeType.Element,
                            new XMLIRWElement("ele3", XmlNodeType.Element)
                        )
                    )
                );
                xmlwriter(element, writer, 0);
                writer.WriteEndDocument();
            }
            Console.WriteLine(stream);
            //Console.Clear();
            // using (FileStream stream = File.OpenRead(pt2)) {
            //     Console.WriteLine("Iniciar leitura");
            //     XmlReaderSettings settings = new XmlReaderSettings();
            //     settings.DtdProcessing = DtdProcessing.Parse;
            //     using (XmlReader reader = XmlReader.Create(stream, settings)) {
            //         Console.WriteLine(xmlread(reader));
            //     }
            // }
            _ = Console.ReadLine();
        }

        static void xmlwriter(XMLIRWElement element, XmlWriter writer, ulong layer) {
            XmlWriterSettings settings = writer.Settings;
            if (element.IsEmpty) return;
            foreach (var item in element) {
                switch (item.NodeType) {
                    case XmlNodeType.ProcessingInstruction:
                        if (settings.Indent)
                            writer.WriteWhitespace(GetWhitespace(settings.IndentChars, layer));
                        writer.WriteProcessingInstruction(item.Name, item.Value.ToString());
                        if (settings.Indent)
                            writer.WriteWhitespace(settings.NewLineChars);
                        break;
                    case XmlNodeType.CDATA:
                            if (settings.Indent)
                                writer.WriteWhitespace(GetWhitespace(settings.IndentChars, layer));
                            writer.WriteCData(item.Value.ToString());
                            if (settings.Indent)
                                writer.WriteWhitespace(settings.NewLineChars);
                        break;
                    case XmlNodeType.DocumentType:
                            if (settings.Indent)
                                writer.WriteWhitespace(GetWhitespace(settings.IndentChars, layer));
                            writer.WriteDocType(item.Name,)
                            if (settings.Indent)
                                writer.WriteWhitespace(settings.NewLineChars);
                        break;
                    case XmlNodeType.Comment:
                            if (settings.Indent)
                                writer.WriteWhitespace(GetWhitespace(settings.IndentChars, layer));
                            writer.WriteComment(item.Value.ToString());
                            if (settings.Indent)
                                writer.WriteWhitespace(settings.NewLineChars);
                        break;
                    case XmlNodeType.Element:
                        if (settings.Indent)
                            writer.WriteWhitespace(GetWhitespace(settings.IndentChars, layer));
                        writer.WriteStartElement(item.Name);
                        foreach (var attri in item.Attributes)
                            writer.WriteAttributeString(attri.Name, attri.Value.ToString());

                        if (!item.ValueIsEmpty) {
                            writer.WriteString(item.Value.ToString());
                            if (settings.Indent)
                                writer.WriteWhitespace(settings.NewLineChars);
                        }
                        xmlwriter(item, writer, layer + 1);
                        if (settings.Indent && !item.NoElements)
                            writer.WriteWhitespace(GetWhitespace(settings.IndentChars, layer));
                        writer.WriteEndElement();
                        if (settings.Indent)
                            writer.WriteWhitespace(settings.NewLineChars);
                        break;
                }
            }
        }

        static string GetWhitespace(string IndentChars, ulong layer) {
            StringBuilder builder = new();
            for (ulong I = 0; I < layer; I++)
                builder.Append(IndentChars);
            return builder.ToString();
        }

        static XMLIRWElement xmlread(XmlReader reader) {
            XMLIRWElement element = new("Root");
            ulong cdata = 0;
            XMLIRWElement[] attributes;
            while (reader.Read()) {
                switch (reader.NodeType) {
                    case XmlNodeType.ProcessingInstruction:
                        attributes = new XMLIRWElement[reader.AttributeCount];
                        if(reader.AttributeCount != 0) {
                            for (int I = 0; I < reader.AttributeCount; I++) {
                                reader.MoveToAttribute(I);
                                attributes[I] = new(reader.Name, reader.Value, XmlNodeType.Attribute, null);
                            }
                            reader.MoveToElement();
                        }
                        element.Add(new XMLIRWElement(reader.Name, XmlNodeType.ProcessingInstruction, attributes));
                        break;
                    case XmlNodeType.XmlDeclaration:
                        attributes = new XMLIRWElement[reader.AttributeCount];
                        if(reader.AttributeCount != 0) {
                            for (int I = 0; I < reader.AttributeCount; I++) {
                                reader.MoveToAttribute(I);
                                attributes[I] = new(reader.Name, reader.Value, XmlNodeType.Attribute, null);
                            }
                            reader.MoveToElement();
                        }
                        element.Add(new XMLIRWElement(reader.Name, XmlNodeType.XmlDeclaration, attributes));
                        break;
                    case XmlNodeType.CDATA:
                        element.Add(new XMLIRWElement($"CDATA:{cdata}", reader.Value, XmlNodeType.CDATA, null));
                        ++cdata;
                        break;
                    case XmlNodeType.DocumentType:
                        element.Add(new XMLIRWElement(reader.Name, reader.Value, XmlNodeType.DocumentType, null));
                        break;
                    case XmlNodeType.Element:
                        attributes = new XMLIRWElement[reader.AttributeCount];
                        if(reader.AttributeCount != 0) {
                            for (int I = 0; I < reader.AttributeCount; I++) {
                                reader.MoveToAttribute(I);
                                attributes[I] = new(reader.Name, reader.Value, XmlNodeType.Attribute, null);
                            }
                            reader.MoveToElement();
                        }
                        if (reader.IsEmptyElement)
                            element.Add(new XMLIRWElement(reader.Name, XmlNodeType.Element, attributes));
                        else element.Add(element = new XMLIRWElement(reader.Name, XmlNodeType.Element, attributes));
                        break;
                    case XmlNodeType.Text:
                        element.Value = new(reader.Value);
                        break;
                    case XmlNodeType.EndElement:
                            element = element.Parent;
                        break;
                }
            }
            return element;
        }
    }
}
