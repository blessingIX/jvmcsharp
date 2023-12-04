using System.Text;

namespace jvmcsharp.classfile
{
    internal class ConstantUtf8Info : ConstantInfo
    {
        public string Str { get; internal set; } = string.Empty;

        public void ReadInfo(ClassReader reader)
        {
            Str = GetMUTF8String(reader.ReadBytes(reader.ReadUInt16()));
        }

        public override string ToString() => Str;

        /// <summary>
        /// Java字符串在class文件中以MUTF-8编码存储
        /// MUTF-8与UTF-8大致相同但是不兼容
        /// Java的序列化机制也使用了MUTF-8编码 java.io.DataInputStream.readUTF(DataInput)
        /// 这里将readUTF方法翻译成C#代码
        /// </summary>
        /// <param name="bytearr">字节数组</param>
        /// <returns>MUTF8编码字符串</returns>
        public static string GetMUTF8String(byte[] bytearr)
        {
            int utflen = bytearr.Length;
            char[] chararr = new char[utflen];

            int c, char2, char3;
            int count = 0;
            int chararr_count = 0;

            while (count < utflen)
            {
                c = bytearr[count] & 0xff;
                if (c > 127) break;
                count++;
                chararr[chararr_count++] = (char)c;
            }

            while (count < utflen)
            {
                c = bytearr[count] & 0xff;
                switch (c >> 4)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                        /* 0xxxxxxx*/
                        count++;
                        chararr[chararr_count++] = (char)c;
                        break;
                    case 12:
                    case 13:
                        /* 110x xxxx   10xx xxxx*/
                        count += 2;
                        if (count > utflen)
                            throw new Exception("malformed input: partial character at end");
                        char2 = bytearr[count - 1];
                        if ((char2 & 0xC0) != 0x80)
                            throw new Exception("malformed input around byte " + count);
                        chararr[chararr_count++] = (char)(((c & 0x1F) << 6) |
                                                        (char2 & 0x3F));
                        break;
                    case 14:
                        /* 1110 xxxx  10xx xxxx  10xx xxxx */
                        count += 3;
                        if (count > utflen)
                            throw new Exception("malformed input: partial character at end");
                        char2 = bytearr[count - 2];
                        char3 = bytearr[count - 1];
                        if (((char2 & 0xC0) != 0x80) || ((char3 & 0xC0) != 0x80))
                            throw new Exception("malformed input around byte " + (count - 1));
                        chararr[chararr_count++] = (char)(((c & 0x0F) << 12) |
                                                        ((char2 & 0x3F) << 6) |
                                                        ((char3 & 0x3F) << 0));
                        break;
                    default:
                        /* 10xx xxxx,  1111 xxxx */
                        throw new Exception("malformed input around byte " + count);
                }
            }
            // The number of chars produced may be less than utflen
            chararr = chararr[0..chararr_count];
            var utf16 = Encoding.GetEncoding("UTF-16");
            return utf16.GetString(utf16.GetBytes(chararr));
        }
    }
}
