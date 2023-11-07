namespace System.Xml {
    public struct XMLIRWValue : IDisposable, IEquatable<XMLIRWValue> {
        private object value;

        public XMLIRWValue(object value) {
            this.value = value;
        }

        public void Dispose() {
            value = null;
        }

        public bool Equals(XMLIRWValue other) {
            throw new NotImplementedException();
        }
    }
}