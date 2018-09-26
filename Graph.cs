using System;
using MessagePack;
using static Util;

public static class Graph
{
    [MessagePackObject]
    public class A
    {
        [Key(1)]
        public B B;
        [Key(0)]
        public int X;
    }

    [MessagePackObject]
    public class B
    {
        [Key(1)]
        public A A;
        [Key(0)]
        public int X;
    }

    [MessagePackObject]
    public class Generic<T>
    {
        [Key(0)]
        public T X;
    }

    [MessagePackObject]
    public class Dynamic
    {
        [Key(0)]
        public object X;
    }

    public static void Test()
    {
        var nest1 = new A { B = new B {} };
        Dump(nest1);

        var nest2 = new A { B = new B { A = new A() } };
        Dump(nest2);

        // stack overflow, fair enough
        // var nestRec = new A { B = new B {} };
        // nestRec.B.A = nestRec;
        // Dump(MessagePackSerializer.Serialize(nestRec)); 

        Log("Generic<int>(1)");
        Dump(new Generic<int> { X = 1 });

        Log("Generic<A>(null)");
        Dump(new Generic<A> { X = null });

        Log("Generic<A>(X=1)");
        Dump(new Generic<A> { X = new A { X = 1 } });

        Log("Dynamic(1)");
        Dump(new Dynamic { X = 1 });

        Log("Dynamic(X=1)");
        Dump(new Dynamic { X = new A { X = 1 } });
        
        var dser = MessagePackSerializer.Serialize(new Dynamic { X = new A { X = 1 } });
        var drnd = MessagePackSerializer.Deserialize<Dynamic>(dser);
        Dump(drnd); // how??

        // fails, X is object[] containing a byte
        // var a = (A)(drnd.X);
        var x = (byte)((object[])drnd.X)[0];
        Console.WriteLine(x); // 1
    }
}