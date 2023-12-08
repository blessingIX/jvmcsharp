package jvmcsharp;

public class MyObject {
    public static int staticVar;
    public int instanceVar;
    public static void main(String[] args) {
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
