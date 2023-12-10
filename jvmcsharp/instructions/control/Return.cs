using jvmcsharp.instructions.basis;
using jvmcsharp.rtda;
using jvmcsharp.rtda.heap;

namespace jvmcsharp.instructions.control
{
    /// <summary>
    /// return void
    /// </summary>
    internal class RETURN : NoOperandsInstruction
    {
        public override void Execute(Frame frame) => frame.Thread.PopFrame();
    }

    /// <summary>
    /// return reference
    /// </summary>
    internal class ARETURN : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var thread = frame.Thread;
            var currentFrame = thread.PopFrame();
            var invokerFrame = thread.TopFrame();
            var val = currentFrame.OperandStack.Pop<JavaObject>();
            invokerFrame.OperandStack.Push(val);
        }
    }

    /// <summary>
    /// return double
    /// </summary>
    internal class DRETURN : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var thread = frame.Thread;
            var currentFrame = thread.PopFrame();
            var invokerFrame = thread.TopFrame();
            var val = currentFrame.OperandStack.Pop<double>();
            invokerFrame.OperandStack.Push(val);
        }
    }

    /// <summary>
    /// return float
    /// </summary>
    internal class FRETURN : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var thread = frame.Thread;
            var currentFrame = thread.PopFrame();
            var invokerFrame = thread.TopFrame();
            var val = currentFrame.OperandStack.Pop<float>();
            invokerFrame.OperandStack.Push(val);
        }
    }

    /// <summary>
    /// return int 
    /// </summary>
    internal class IRETURN : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var thread = frame.Thread;
            var currentFrame = thread.PopFrame();
            var invokerFrame = thread.TopFrame();
            var val = currentFrame.OperandStack.Pop<int>();
            invokerFrame.OperandStack.Push(val);
        }
    }

    /// <summary>
    /// return long
    /// </summary>
    internal class LRETURN : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            var thread = frame.Thread;
            var currentFrame = thread.PopFrame();
            var invokerFrame = thread.TopFrame();
            var val = currentFrame.OperandStack.Pop<long>();
            invokerFrame.OperandStack.Push(val);
        }
    }
}
