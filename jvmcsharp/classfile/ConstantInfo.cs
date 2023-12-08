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
            return tag switch
            {
                (byte)Tag.ConstantInteger => new ConstantIntegerInfo(),
                (byte)Tag.ConstantFloat => new ConstantFloatInfo(),
                (byte)Tag.ConstantLong => new ConstantLongInfo(),
                (byte)Tag.ConsantDouble => new ConstantDoubleInfo(),
                (byte)Tag.ConstantUtf8 => new ConstantUtf8Info(),
                (byte)Tag.ConstantString => new ConstantStringInfo() { Cp = cp },
                (byte)Tag.ConstantClass => new ConstantClassInfo() { Cp = cp },
                (byte)Tag.ConstantFieldref => new ConstantFieldrefInfo() { Cp = cp },
                (byte)Tag.ConstantMethodref => new ConstantMethodrefInfo() { Cp = cp },
                (byte)Tag.ConstantInterfaceMethodref => new ConstantInterfaceMethodrefInfo { Cp = cp },
                (byte)Tag.ConstantNameAndType => new ConstantNameAndTypeInfo(),
                (byte)Tag.ConstantMethodType => new ConstantMethodTypeInfo(),
                (byte)Tag.ConstantMethodHandle => new ConstantMethodHandleInfo(),
                (byte)Tag.ConstantInvokeDynamic => new ConstantInvokeDynamicInfo(),
                _ => throw new Exception("java.lang.ClassFormatError: constant pool tag!"),
            };
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
