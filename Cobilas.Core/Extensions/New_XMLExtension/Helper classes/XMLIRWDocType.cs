namespace System.Xml {
    public class XMLIRWDocType : XMLIRW {
        private bool disposedValue;

        public override string Name { get; set; }
        public override XMLIRW Parent { get; set; }
        public XMLIRWValue PudID { get; private set;}
        public XMLIRWValue SysID { get; private set;}
        public XMLIRWValue SubSet { get; private set;}
        public override XmlNodeType Type { get; set; }

        public XMLIRWDocType(XMLIRWElement parent, string name, XMLIRWValue pudid, XMLIRWValue sysid, XMLIRWValue subset) : base(parent, name, XmlNodeType.DocumentType) {
            this.PudID = pudid;
            this.SysID = sysid;
            this.SubSet = subset;
        }
        public XMLIRWDocType(string name, XMLIRWValue pudid, XMLIRWValue sysid, XMLIRWValue subset) : this(null, name, pudid, sysid, subset) {}
        public XMLIRWDocType(XMLIRWElement parent, string name, object pudid, object sysid, object subset) :
            this(parent, name, new XMLIRWValue(pudid), new XMLIRWValue(sysid), new XMLIRWValue(subset)) {}
        public XMLIRWDocType(string name, object pudid, object sysid, object subset) :
            this(name, new XMLIRWValue(pudid), new XMLIRWValue(sysid), new XMLIRWValue(subset)) {}

        ~XMLIRWDocType() => Dispose(disposing: false);

        public override void Dispose() {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            if (!disposedValue) {
                if (disposing) {
                    Name = null;
                    Parent = null;
                    Type = default;
                    PudID = default;
                    SysID = default;
                    SubSet = default;
                }
                disposedValue = true;
            }
        }
    }
}