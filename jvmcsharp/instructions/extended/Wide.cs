using jvmcsharp.instructions.basis;
using jvmcsharp.instructions.loads;
using jvmcsharp.instructions.math;
using jvmcsharp.instructions.stores;
using jvmcsharp.rtda;

namespace jvmcsharp.instructions.extended
{
    internal class WIDE : Instruction
    {
        public Instruction? ModifiedInstruction { get; internal set; }

        public override void Execute(Frame frame) => ModifiedInstruction?.Execute(frame);

        public override void FetchOperands(BytecodeReader reader)
        {
            var opcode = reader.ReadUInt8();
            switch (opcode)
            {
                case 0x15:  // iload
                    ModifiedInstruction = new ILOAD
                    {
                        Index = reader.ReadUInt16(),
                    };
                    break;
                case 0x16:  // lload
                    ModifiedInstruction = new LLOAD
                    {
                        Index = reader.ReadUInt16(),
                    };
                    break;
                case 0x17:  // fload
                    ModifiedInstruction = new FLOAD
                    {
                        Index = reader.ReadUInt16(),
                    };
                    break;
                case 0x18:  // dload
                    ModifiedInstruction = new DLOAD
                    {
                        Index = reader.ReadUInt16(),
                    };
                    break;
                case 0x19:  // aload
                    ModifiedInstruction = new ALOAD
                    {
                        Index = reader.ReadUInt16(),
                    };
                    break;
                case 0x36:  // istore
                    ModifiedInstruction = new ISTORE
                    {
                        Index = reader.ReadUInt16(),
                    };
                    break;
                case 0x37:  // lstore
                    ModifiedInstruction = new LSTORE
                    {
                        Index = reader.ReadUInt16(),
                    };
                    break;
                case 0x38:  // fstore
                    ModifiedInstruction = new FSTORE
                    {
                        Index = reader.ReadUInt16(),
                    };
                    break;
                case 0x39:  // dstore
                    ModifiedInstruction = new DSTORE
                    {
                        Index = reader.ReadUInt16(),
                    };
                    break;
                case 0x3a:  // astore
                    ModifiedInstruction = new ASTORE
                    {
                        Index = reader.ReadUInt16(),
                    };
                    break;
                case 0x84:  // iinc
                    ModifiedInstruction = new IINC
                    {
                        Index = reader.ReadUInt16(),
                        Const = reader.ReadUInt16(),
                    };
                    break;
                case 0xa9:  // ret
                    // TODO 暂未实现ret指令
                    throw new Exception("Unsupport opcode: oxa9!");
            }
        }
    }
}
