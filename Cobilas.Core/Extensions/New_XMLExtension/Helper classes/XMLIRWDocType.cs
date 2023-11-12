using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace System.Xml {
    public class XMLIRWDocType : XMLIRW {
        private bool disposedValue;
        private XMLIRWValue pudid, sysid, subset;

        public XMLIRWValue PudID => pudid;
        public XMLIRWValue SysID => sysid;
        public XMLIRWValue SubSet => subset;
        public override string Name { get; set; }
        public override XMLIRW Parent { get; set; }
        public override XmlNodeType Type { get; set; }

        public XMLIRWDocType(XMLIRWElement parent, string name, XMLIRWValue pudid, XMLIRWValue sysid, XMLIRWValue subset) : base(parent, name, XmlNodeType.DocumentType) {
            this.pudid = pudid;
            this.sysid = sysid;
            this.subset = subset;
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
                    pudid = default;
                    sysid = default;
                    subset = default;
                }
                disposedValue = true;
            }
        }
    }
}