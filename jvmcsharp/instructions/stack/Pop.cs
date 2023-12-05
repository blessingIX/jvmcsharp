using jvmcsharp.instructions.basis;
using jvmcsharp.rtda;

namespace jvmcsharp.instructions.stack
{
    internal class POP : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => frame.OperandStack.Pop<object>();
    }

    internal class POP2 : NoOperandsInstruction
    {
        // TODO 实际的jvm中double和long变量在操作数栈中占据两个位置需要使用pop2指令弹出
        // 目前已简化，统一用object存操作数，不存在double和long变量占据两个位置的情况
        public override void Execute(Frame frame) => frame.OperandStack.Pop<object>();
    }
}
