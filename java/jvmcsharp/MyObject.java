package jvmcsharp;

public class MyObject {
    public static int staticVar;
    public int instanceVar;
    public static void main(String[] args) {
        /*
        * -1~5 => iconst
        * -128~127 => bipush
        * -32768~32767 => sipush
        * -2147483648~2147483647 => ldc
        * */
        int a = 5;      // iconst
        int b = 127;    // bipush
        int c = 32767;  // sipush
        int x = 32768;  // ldc
        MyObject myObject = new MyObject(); // new
        MyObject.staticVar = x; // putstatic
        x = MyObject.staticVar; // getstatic
        myObject.instanceVar = x;   // putfield
        x = myObject.instanceVar;   // getfield
        Object object = myObject;
        if (object instanceof MyObject) {   // instanceof
            myObject = (MyObject) object;   // checkcast
            System.out.println(myObject.instanceVar);
        }
    }
}
