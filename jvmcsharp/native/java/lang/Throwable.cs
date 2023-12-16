using jvmcsharp.rtda;
using jvmcsharp.rtda.heap;

namespace jvmcsharp.native.java.lang
{
    internal class Throwable
    {
        static Throwable()
        {
            Registry.Register("java/lang/Throwable", "fillInStackTrace", "(I)Ljava/lang/Throwable;", FillInStackTrace);
        }

        private static void FillInStackTrace(Frame frame)
        {
            var @this = frame.LocalVars.GetThis();
            frame.OperandStack.Push(@this);
            var sets = StackTraceElement.CreateStackTraceElements(@this, frame.Thread);
            @this.Extra = sets;
        }
    }

    internal class StackTraceElement
    {
        public string FileName { get; internal set; } = string.Empty;
        public string ClassName { get; internal set; } = string.Empty;
        public string MethodName { get; internal set; } = string.Empty;
        public int LineNumber { get; internal set; }

        public StackTraceElement(Frame frame)
        {
            var method = frame.Method;
            var @class = method.Class;
            FileName = @class!.SourceFile;
            ClassName = @class.JavaName();
            MethodName = method.Name;
            LineNumber = method.GetLineNumber(frame.NextPc - 1);
        }

        public override string ToString() => $"{ClassName}.{MethodName}({FileName}:{LineNumber})";

        public static StackTraceElement[] CreateStackTraceElements(JavaObject tObj, rtda.Thread thread)
        {
            var skip = DistanceToObject(tObj.Class) + 2;
            var frames = thread.GetFrames()[skip..^0];
            var sets = frames
                .Select(frame => new StackTraceElement(frame))
                .ToArray();
            return sets;
        }

        private static int DistanceToObject(rtda.heap.Class @class)
        {
            var distance = 0;
            for (var c = @class.SuperClass; c != null; c = c.SuperClass)
            {
                distance++;
            }
            return distance;
        }
    }
}
