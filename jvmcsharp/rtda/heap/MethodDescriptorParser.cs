namespace jvmcsharp.rtda.heap
{
    internal class MethodDescriptorParser
    {
        public string Raw { get; internal set; } = string.Empty;
        public int Offset { get; internal set; }
        public MethodDescriptor Parsed { get; internal set; } = new();

        public static MethodDescriptor ParseMethodDescriptor(string descriptor)
        {
            var parser = new MethodDescriptorParser();
            return parser.Parse(descriptor);
        }

        private MethodDescriptor Parse(string descriptor)
        {
            Raw = descriptor;
            Parsed = new();
            StartParams();
            ParseParamTypes();
            EndParams();
            ParseReturnType();
            Finish();
            return Parsed;
        }

        private void StartParams()
        {
            if (ReadUInt8() != '(')
            {
                CausePanic();
            }
        }

        private void ParseParamTypes()
        {
            while (true)
            {
                var t = ParseFieldType();
                if (t != string.Empty)
                {
                    Parsed.AddParameterType(t);
                }
                else
                {
                    break;
                }
            }
        }

        private void EndParams()
        {
            if (ReadUInt8() != ')')
            {
                CausePanic();
            }
        }

        private void ParseReturnType()
        {
            if (ReadUInt8() == 'V')
            {
                Parsed.ReturnType = "V";
                return;
            }
            UnreadUInt8();
            var t = ParseFieldType();
            if (t != string.Empty)
            {
                Parsed.ReturnType = t;
                return;
            }
            CausePanic();
        }

        private void Finish()
        {
            if (Offset != Raw.Length)
            {
                CausePanic();
            }
        }

        private void CausePanic() => throw new Exception($"BAD descriptor: {Raw}");

        public sbyte ReadUInt8() => (sbyte)Raw[Offset++];

        public void UnreadUInt8() => Offset--;

        private string ParseFieldType()
        {
            switch ((char)ReadUInt8())
            {
                case 'B':
                    return "B";
                case 'C':
                    return "C";
                case 'D':
                    return "D";
                case 'F':
                    return "F";
                case 'I':
                    return "I";
                case 'J':
                    return "J";
                case 'S':
                    return "S";
                case 'Z':
                    return "Z";
                case 'L':
                    return ParseObjectType();
                case '[':
                    return ParseArrayType();
                default:
                    UnreadUInt8();
                    return string.Empty;
            }
        }

        private string ParseObjectType()
        {
            var unread = Raw[Offset..^0];
            var semicolonIndex = unread.IndexOf(';');
            if (semicolonIndex == -1)
            {
                CausePanic();
                return string.Empty;
            }
            else
            {
                var objStart = Offset - 1;
                var objEnd = Offset + semicolonIndex + 1;
                Offset = objEnd;
                var descriptor = Raw[objStart..objEnd];
                return descriptor;
            }
        }

        private string ParseArrayType()
        {
            var arrString = Offset - 1;
            ParseFieldType();
            var arrEnd = Offset;
            var descriptor = Raw[arrString..arrEnd];
            return descriptor;
        }
    }
}
