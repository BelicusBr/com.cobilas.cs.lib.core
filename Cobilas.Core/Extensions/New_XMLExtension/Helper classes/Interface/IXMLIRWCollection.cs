using System.Collections.Generic;

namespace System.Xml {
    public interface IXMLIRWCollection : IEnumerable<XMLIRW>, IDisposable {
        bool IsEmpty {get;}
        bool NoElements {get;}
        bool ValueIsEmpty {get;}
        bool NoAttributes {get;}
        int AttributeCount {get;}
        IEnumerable<XMLIRW> Attributes {get;}

        bool Add(XMLIRWElement element);
    }
}