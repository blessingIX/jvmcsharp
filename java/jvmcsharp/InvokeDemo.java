package jvmcsharp;

public class InvokeDemo implements Runnable {
    public static void main(String[] args) {
        new InvokeDemo().test();
    }

    public void test() {
        InvokeDemo.staticMethod();              // invokestatic
        InvokeDemo demo = new InvokeDemo();     // invokespecial
        demo.instanceMethod();                  // invokespecial
        super.equals(null);                     // invokespecial
        this.run();                             // invokevirtual
        ((Runnable) demo).run();                // invokeinterface
    }

    public static void staticMethod() {
        System.out.println(101);
    }

    private void instanceMethod() {
        System.out.println(102);
    }

    @Override
    public void run() {
        System.out.println(103);
    }
}
