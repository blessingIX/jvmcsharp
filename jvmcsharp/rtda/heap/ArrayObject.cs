namespace jvmcsharp.rtda.heap
{
    internal class ArrayObject : JavaObject
    {
        public sbyte[] Bytes => (sbyte[])Data;
        public short[] Shorts => (short[])Data;
        public int[] Ints => (int[])Data;
        public long[] Longs => (long[])Data;
        public ushort[] Chars => (ushort[])Data;
        public float[] Flotas => (float[])Data;
        public double[] Doubles => (double[])Data;
        public JavaObject[] Refs => (JavaObject[])Data;

        public int ArrayLength()
        {
            if (Data is sbyte[] bytes)
            {
                return bytes.Length;
            }
            else if (Data is short[] shorts)
            {
                return shorts.Length;
            }
            else if (Data is int[] ints)
            {
                return ints.Length;
            }
            else if (Data is long[] longs)
            {
                return longs.Length;
            }
            else if (Data is ushort[] chars)
            {
                return chars.Length;
            }
            else if (Data is float[] floats)
            {
                return floats.Length;
            }
            else if (Data is double[] doubles)
            {
                return doubles.Length;
            }
            else if (Data is JavaObject[] javaObjects)
            {
                return javaObjects.Length;
            }
            throw new Exception("Not array!");
        }
    }
}