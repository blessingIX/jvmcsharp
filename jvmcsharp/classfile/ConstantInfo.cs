namespace jvmcsharp.classfile
{
    internal interface ConstantInfo
    {
        public static ConstantInfo ReadConstantInfo(ClassReader reader, ConstantPool cp)
        {
            var tag = reader.ReadUInt8();
            var c = Create(tag, cp);
            c.ReadInfo(reader);
            return c;
        }

        public static ConstantInfo Create(byte tag, ConstantPool cp)
        {
            switch (tag)
            {
                case (byte)Tag.ConstantInteger:
                    return new ConstantIntegerInfo();
                case (byte)Tag.ConstantFloat:
                    return new ConstantFloatInfo();
                case (byte)Tag.ConstantLong:
                    return new ConstantLongInfo();
                case (byte)Tag.ConsantDouble:
                    return new ConstantDoubleInfo();
                case (byte)Tag.ConstantUtf8:
                    return new ConstantUtf8Info();
                case (byte)Tag.ConstantString:
                    return new ConstantStringInfo() { Cp = cp };
                case (byte)Tag.ConstantClass:
                    return new ConstantClassInfo() { Cp = cp };
                case (byte)Tag.ConstantFieldref:
                    return new ConstantFieldrefInfo() { Cp = cp };
                case (byte)Tag.ConstantMethodref:
                    return new ConstantMethodrefInfo() { Cp = cp };
                case (byte)Tag.ConstantInterfaceMethodref:
                    return new ConstantInterfaceMethodrefInfo { Cp = cp };
                case (byte)Tag.ConstantNameAndType:
                    return new ConstantNameAndTypeInfo();
                case (byte)Tag.ConstantMethodType:
                    return new ConstantMethodTypeInfo();
                case (byte)Tag.ConstantMethodHandle:
                    return new ConstantMethodHandleInfo();
                case (byte)Tag.ConstantInvokeDynamic:
                    return new ConstantInvokeDynamicInfo();
                default:
                    throw new Exception("java.lang.ClassFormatError: constant pool tag!");
            }
        }

        public void ReadInfo(ClassReader reader);
    }

    internal enum Tag : byte
    {
        ConstantClass = 7,
        ConstantFieldref = 9,
        ConstantMethodref = 10,
        ConstantInterfaceMethodref = 11,
        ConstantString = 8,
        ConstantInteger = 3,
        ConstantFloat = 4,
        ConstantLong = 5,
        ConsantDouble = 6,
        ConstantNameAndType = 12,
        ConstantUtf8 = 1,
        ConstantMethodHandle = 15,
        ConstantMethodType = 16,
        ConstantInvokeDynamic = 18,
    }
}
