namespace System.Reflection {
    public static class FieldInfo_CB_Extension {
        public static bool IsBackingField(this FieldInfo F)
            => F.Name.Contains(">k__BackingField");
    }
}
