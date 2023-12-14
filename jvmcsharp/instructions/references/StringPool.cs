using jvmcsharp.rtda.heap;

namespace jvmcsharp.instructions.references
{
    internal static class StringPool
    {
        public static Dictionary<string, JavaObject> InternedStrings = [];

        public static JavaObject JavaString(ClassLoader loader, string csString)
        {
            if (InternedStrings.TryGetValue(csString, out var internedString))
            {
                return internedString;
            }
            var chars = csString.ToCharArray().Select(v => (ushort)v).ToArray();
            var jChars = new ArrayObject { Class = loader.LoadClass("[C"), Data = chars };
            var jString = loader.LoadClass("java/lang/String").NewObject();
            jString.SetRefVar("value", "[C", jChars);
            InternedStrings[csString] = jString;
            return jString;
        }

        public static string CsharpString(JavaObject javaString)
        {
            var charArr = (ArrayObject)javaString.GetRefVar("value", "[C");
            return new string(charArr.Chars.Select(v => (char)v).ToArray());
        }
    }
}
