namespace jvmcsharp.rtda.heap
{
    internal class MethodDescriptor
    {
        public List<string> ParameterTypes { get; internal set; } = [];
        public string ReturnType { get; internal set; } = string.Empty;

        public void AddParameterType(string t) => ParameterTypes.Add(t);
    }
}
