using jvmcsharp.rtda;
using System.Reflection;

namespace jvmcsharp.native
{
    internal delegate void NativeMethod(Frame frame);

    internal class Registry
    {
        private static Dictionary<string, NativeMethod> NativeMethodDict { get; } = [];

        public static void RegisterNativeMethods()
        {
            NativeMethodDict.Clear();
            Assembly assembly = Assembly.GetExecutingAssembly();
            Type[] types = assembly.GetTypes();
            HashSet<string> namespaces =
            [
                "java.lang",
            ];
            var baseNamespace = typeof(Registry).Namespace;
            namespaces = namespaces.Select(v => $"{baseNamespace}.{v}").ToHashSet();

            foreach (Type type in types)
            {
                if (namespaces.Contains(type.Namespace!))
                {
                    ConstructorInfo? constructor = type.GetConstructor(Type.EmptyTypes);
                    constructor?.Invoke(null);
                }
            }
        }

        public static string GetMethodKey(string className, string methodName, string methodDescriptor)
            => string.Join('~', className, methodName, methodDescriptor);

        public static void Register(string className, string methodName, string methodDescriptor, NativeMethod method)
            => NativeMethodDict[GetMethodKey(className, methodName, methodDescriptor)] = method;

        public static NativeMethod FindNativeMethod(string className, string methodName, string methodDescriptor)
        {
            var key = GetMethodKey(className, methodName, methodDescriptor);
            if (NativeMethodDict.TryGetValue(key, out NativeMethod? method))
            {
                return method;
            }
            if (methodDescriptor == "()V" && methodName == "registerNatives")
            {
                return EmptyNativeMethod;
            }
            return null!;
        }

        private static void EmptyNativeMethod(Frame _)
        {
            // do nothing
        }
    }
}
