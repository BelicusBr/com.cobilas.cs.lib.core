using System;
using System.IO;
using System.Xml;
using Cobilas.Collections;

namespace com.cobilas.cs.lib.core.test {
    class Program {
        static string pt1 => @"C:\Users\Cobilas portable\Desktop\Projetos CSharp\Bibliotecas\com.cobilas.cs.lib.core\com.cobilas.cs.lib.core.test\tds.xml";
        static string pt2 => @"C:\Users\Cobilas portable\Desktop\Projetos CSharp\Bibliotecas\com.cobilas.cs.lib.core\com.cobilas.cs.lib.core.test\tds2.xml";
        static string pt3 => @"C:\Users\Cobilas portable\Desktop\Projetos CSharp\Bibliotecas\com.cobilas.cs.lib.core\com.cobilas.cs.lib.core.test\tds3.xml";
        
        static void Main(string[] args) {
            //Console.Clear();
            using (FileStream stream = File.OpenRead(pt2)) {
                Console.WriteLine("Iniciar leitura");
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.DtdProcessing = DtdProcessing.Parse;
                using (XmlReader reader = XmlReader.Create(stream, settings)) {
                    Console.WriteLine(xmlread(reader));
                }
            }
            _ = Console.ReadLine();
        }

        static void xmlwriter(XMLIRWElement element, XmlWriter writer) {
            writer.WriteStartDocument();
            foreach (var item in element)
                switch (item.NodeType) {
                    case XmlNodeType.Element:
                        break;
                    case XmlNodeType.XmlDeclaration:
                        break;
                    case XmlNodeType.DocumentType:
                        break;
                    case XmlNodeType.ProcessingInstruction:
                        break;
                    case XmlNodeType.CDATA:
                        break;
                }
            writer.WriteEndDocument();
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
