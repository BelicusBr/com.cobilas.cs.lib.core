using Cobilas.Collections;

namespace System {
    public static class Object_CB_Extension {
        public static bool CompareType(this object O, Type type)
            => (O is Type tres ? tres : O.GetType()) == type;

        public static bool CompareType(this object O, params Type[] types) {
            for (int I = 0; I < ArrayManipulation.ArrayLength(types); I++)
                if (CompareType(O, types[I])) 
                    return true;
            return false;
        }

        public static bool CompareType<T>(this object O)
            => CompareType(O, typeof(T));

        /// <summary>Compares class type and the class it inherits.</summary>
        public static bool CompareTypeAndSubType(this object O, Type type, bool IncludeInterface)
            => CompareType(O, type) || IsSubclassOf(O, type) ||
            (IncludeInterface && IsAssignableFrom(O, type));

        /// <summary>Compares class type and the class it inherits.</summary>
        public static bool CompareTypeAndSubType(this object O, Type type)
            => CompareTypeAndSubType(O, type, false);

        /// <summary>Compares class type and the class it inherits.</summary>
        public static bool CompareTypeAndSubType<T>(this object O, bool IncludeInterface)
            => CompareTypeAndSubType(O, typeof(T), IncludeInterface);

        /// <summary>Compares class type and the class it inherits.</summary>
        public static bool CompareTypeAndSubType<T>(this object O)
            => CompareTypeAndSubType<T>(O, false);

        private static bool IsSubclassOf(object O, Type type)
            => (O is Type tp ? tp : O.GetType()).IsSubclassOf(type);

        private static bool IsAssignableFrom(object O, Type type)
            => (O is Type tp ? tp : O.GetType()).IsAssignableFrom(type);
    }
}
