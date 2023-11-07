using System.Collections;
using Cobilas.Collections;
using System.Collections.Generic;

namespace System.Xml {
    /// <summary>XML improved reader and writer element.</summary>
    public class XMLIRWElement : IDisposable, IEnumerable<XMLIRWElement> {
        private XmlNodeType type;
        private bool disposedValue;
        private XMLIRWElement parent;
        private XMLIRWElement[] itens;

        /// <summary>Element name.</summary>
        public string Name { get; set; }
        public XmlNodeType NodeType => type;
        public XMLIRWValue Value { get; set; }
        public XMLIRWElement Parent => parent;

        public XMLIRWElement(XMLIRWElement parent, string name, XMLIRWValue value, XmlNodeType type, params XMLIRWElement[] itens) {
            Name = name;
            this.type = type;
            this.itens = itens;
            this.Value = value;
            this.parent = parent;
            foreach (var item in itens)
                item.parent = this;
        }

        public XMLIRWElement(XMLIRWElement parent, string name, object value, XmlNodeType type, params XMLIRWElement[] itens) :
            this(parent, name, new XMLIRWValue(value), type, itens) {}

        public XMLIRWElement(string name, XMLIRWValue value, XmlNodeType type, params XMLIRWElement[] itens) :
            this(null, name, value, type, itens) {}

        public XMLIRWElement(string name, object value, XmlNodeType type, params XMLIRWElement[] itens) :
            this(null, name, value, type, itens) {}

        public XMLIRWElement(string name, XmlNodeType type, params XMLIRWElement[] itens) :
            this(name, default, type, itens) {}

        public XMLIRWElement(string name, XmlNodeType type) : this(name, type, null) {}

        public XMLIRWElement(string name) : this(name, XmlNodeType.None) {}

        ~XMLIRWElement() => Dispose(disposing: false);

        public void Dispose() {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public bool Add(XMLIRWElement element) {
            if(element is null) return false;
            element.parent = this;
            ArrayManipulation.Add(element, ref itens);
            return true;
        }

        public IEnumerator<XMLIRWElement> GetEnumerator()
            => new ArrayToIEnumerator<XMLIRWElement>(itens);

        private void Dispose(bool disposing) {
            if (!disposedValue) {
                if (disposing)
                    InternalDispose();
                disposedValue = true;
            }
        }

        protected virtual void InternalDispose() {
            Name = null;
            type = default;
            Value = default;
            ArrayManipulation.ClearArraySafe(ref itens);
        }

        IEnumerator IEnumerable.GetEnumerator()
            => new ArrayToIEnumerator<XMLIRWElement>(itens);
    }
}