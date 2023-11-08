namespace System.Xml {
    public abstract class XMLIRW : IDisposable {
        public abstract string Name { get; set; }
        public abstract XMLIRW Parent { get; set; }
        public abstract XmlNodeType Type { get; set; }

        protected XMLIRW(XMLIRW parent, string name, XmlNodeType type) {
            Name = name;
            Type = type;
            Parent = parent;
        }
        protected XMLIRW(XMLIRW parent, string name) : this(parent, name, XmlNodeType.None) {}
        protected XMLIRW(string name, XmlNodeType type) : this(null, name, type) {}
        protected XMLIRW(string name) : this(null, name, XmlNodeType.None) {}

        public abstract void Dispose();
    }
}