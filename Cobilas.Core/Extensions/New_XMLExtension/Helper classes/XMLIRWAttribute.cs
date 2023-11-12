namespace System.Xml {
    public class XMLIRWAttribute : XMLIRW {
        private bool disposedValue;

        public XMLIRWValue Value {get; set;}
        public override string Name { get; set; }
        public override XMLIRW Parent { get; set; }
        public override XmlNodeType Type { get; set; }

        public XMLIRWAttribute(XMLIRWElement parent, string name, XMLIRWValue value) : base(parent, name, XmlNodeType.Attribute) {
            Value = value;
        }
        public XMLIRWAttribute(string name, XMLIRWValue value) : this(null, name, value) {}
        public XMLIRWAttribute(XMLIRWElement parent, string name, object value) : this(parent, name, new XMLIRWValue(value)) {}
        public XMLIRWAttribute(string name, object value) : this(null, name, value) {}

        ~XMLIRWAttribute() => Dispose(disposing: false);

        protected virtual void Dispose(bool disposing) {
            if (!disposedValue) {
                if (disposing) {
                    Name = null;
                    Parent = null;
                    Type = default;
                    Value = default;
                }
                disposedValue = true;
            }
        }

        public override void Dispose() {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}