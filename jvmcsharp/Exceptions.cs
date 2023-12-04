namespace jvmcsharp
{
    internal class ClassNotFoundException : Exception
    {
        public ClassNotFoundException(string? message) : base(message)
        {
        }
    }
}
