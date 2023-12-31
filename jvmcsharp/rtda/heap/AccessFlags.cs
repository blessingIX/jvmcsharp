﻿namespace jvmcsharp.rtda.heap
{
    internal static class AccessFlags
    {
        public const int ACC_PUBLIC = 0x0001;
        public const int ACC_PRIVATE = 0x0002;
        public const int ACC_PROTECTED = 0x0004;
        public const int ACC_STATIC = 0x0008;
        public const int ACC_FINAL = 0x0010;
        public const int ACC_SUPER = 0x0020;
        public const int ACC_SYNCHRONIZED = 0x0020;
        public const int ACC_VOLATILE = 0x0040;
        public const int ACC_BRIDGE = 0x0040;
        public const int ACC_TRANSIENT = 0x0080;
        public const int ACC_VARARGS = 0x0080;
        public const int ACC_NATIVE = 0x0100;
        public const int ACC_INTERFACE = 0x0200;
        public const int ACC_ABSTRACT = 0x0400;
        public const int ACC_STRICT = 0x0800;
        public const int ACC_SYNTHETIC = 0x1000;
        public const int ACC_ANNOTATION = 0x2000;
        public const int ACC_ENUM = 0x4000;
    }
}
