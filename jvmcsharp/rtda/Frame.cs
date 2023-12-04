namespace jvmcsharp.rtda
{
    internal class Frame(uint maxLocals, uint maxStack)
    {
        public Frame? Lower { get; internal set; }
        public LocalVars LocalVars { get; internal set; } = new LocalVars(maxLocals);
        public OperandStack OperandStack { get; internal set; } = new OperandStack(maxStack);
    }
}
