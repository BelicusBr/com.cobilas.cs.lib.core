namespace System.Xml {
    public class XMLIRWProcessingInstruction : XMLIRW, IDisposable {
        private bool disposedValue;

        public override string Name { get; set; }
        public override XMLIRW Parent { get; set; }
        public XMLIRWValue Value { get; private set; }
        public override XmlNodeType Type { get; set; }

        public XMLIRWProcessingInstruction(XMLIRW parent, string name, XMLIRWValue value) : base(parent, name, XmlNodeType.ProcessingInstruction) {
            this.Value = value;
        }

        public XMLIRWProcessingInstruction(string name, XMLIRWValue value) : this(null, name, value) {}

        ~XMLIRWProcessingInstruction()
            => Dispose(disposing: false);

        protected virtual void Dispose(bool disposing) {
            if (!disposedValue) {
                if (disposing) {
                    Value = XMLIRWValue.Empty;
                    Name = string.Empty;
                    Parent = null;
                    Type = XmlNodeType.None;
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