namespace jvmcsharp.classfile
{
    internal class MarkerAttribute : AttributeInfo
    {
        public void ReadInfo(ClassReader reader)
        {
            // 标记属性，不存任何数据
        }
    }

    internal class DeprecatedAttribute : MarkerAttribute { }

    internal class SyntheticAttribute : MarkerAttribute { }
}
