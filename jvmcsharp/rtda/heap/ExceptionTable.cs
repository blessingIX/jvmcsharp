using jvmcsharp.classfile;

namespace jvmcsharp.rtda.heap
{
    internal class ExceptionTable
    {
        public ExceptionHandler[] ExceptionHandlers { get; internal set; }

        public ExceptionTable(ExceptionTableEntry[] entries, ConstantPool cp)
        {
            ExceptionHandlers = entries.Select(v
                => new ExceptionHandler(v.StartPc, v.EndPc, v.HandlePc, GetCatchType(v.CathcType, cp)!)
                ).ToArray();
        }

        public static ClassRef GetCatchType(uint index, ConstantPool cp)
        {
            if (index == 0) return null!;
            return cp.Get<ClassRef>(index);
        }

        public ExceptionHandler FindExceptionHander(Class exClass, int pc)
        {
            foreach (var handler in ExceptionHandlers)
            {
                if (pc >= handler.StartPc && pc < handler.EndPc)
                {
                    if (handler.CatchType == null)
                    {
                        return handler;
                    }
                    var catchClass = handler.CatchType.ResolveClass();
                    if (catchClass == exClass || catchClass.IsSuperClassOf(exClass))
                    {
                        return handler;
                    }
                }
            }
            return null!;
        }
    }

    internal class ExceptionHandler(int startPc, int endPc, int handlerPc, ClassRef catchType)
    {
        public int StartPc { get; internal set; } = startPc;
        public int EndPc { get; internal set; } = endPc;
        public int HandlerPc { get; internal set; } = handlerPc;
        public ClassRef CatchType { get; internal set; } = catchType;
    }
}
