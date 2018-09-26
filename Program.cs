using System;
using System.Runtime.Serialization;
using System.Text;
using MessagePack;


[MessagePackObject]
public class MP
{
    [Key(0)]
    public int X;
    [Key(1)]
    public int Y;
}

[MessagePackObject]
public class MPKeyed
{
    [Key("X")]
    public int X;
    [Key("Y")]
    public int Y;
}

[DataContract]
public class DC
{
    [DataMember]
    public int X;
    [DataMember]
    public int Y;
}

[DataContract]
public class DCNamed
{
    [DataMember(Name = "X")]
    public int X;
    [DataMember(Name = "Y")]
    public int Y;
}

[DataContract]
public class DCOrdered
{
    [DataMember(Order = 0)]
    public int X;
    [DataMember(Order = 1)]
    public int Y;
}

[DataContract]
public class DCBoth
{
    [DataMember(Order = 0)]
    public int X;
    [DataMember(Order = 1)]
    public int Y;
}

[DataContract, MessagePackObject]
public class DCMP
{
    [DataMember, Key(0)]
    public int X;
    [DataMember, Key(1)]
    public int Y;
}

[DataContract, MessagePackObject]
public class DCMPOverlap
{
    [DataMember, IgnoreMember]
    public int X;
    [DataMember, Key(0)]
    public int Y;
    [Key(1)]
    public int Z;
}

class Program
{
    static void Main(string[] args)
    {
        var hello = Encoding.UTF8.GetBytes("Hello World!");
        Dump(hello);

        Log("MP(X=1, Y=2)");
        Dump(MessagePackSerializer.Serialize(new MP {X = 1, Y = 2}));

        Log("MPKeyed(X=1, Y=2)");
        Dump(MessagePackSerializer.Serialize(new MPKeyed {X = 1, Y = 2}));

        Log("DC(X=1, Y=2)");
        Dump(MessagePackSerializer.Serialize(new DC {X = 1, Y = 2}));

        Log("DCNamed(X=1, Y=2)");
        Dump(MessagePackSerializer.Serialize(new DCNamed {X = 1, Y = 2}));

        Log("DCOrdered(X=1, Y=2)");
        Dump(MessagePackSerializer.Serialize(new DCOrdered {X = 1, Y = 2}));

        Log("DCBoth(X=1, Y=2)");
        Dump(MessagePackSerializer.Serialize(new DCBoth {X = 1, Y = 2}));

        Log("DCMP(X=1, Y=2)");
        Dump(MessagePackSerializer.Serialize(new DCMP {X = 1, Y = 2}));

        Log("DCMPOverlap(X=1, Y=2, Z=3)");
        Dump(MessagePackSerializer.Serialize(new DCMPOverlap {X = 1, Y = 2, Z = 3}));
    }

    static void Log(string s) => Console.WriteLine(s);

    static void Dump(byte[] data) => Console.WriteLine(Hexdump(data));

    static string Hexdump(byte[] data)
    {
        var builder = new StringBuilder();

        int i = 0;
        var l = new byte[10];
        foreach (var b in data)
        {
            l[i++] = b;
            if (i >= l.Length)
            {
                i = 0;
                foreach (var c in l) builder.AppendFormat("{0:X2} ", c);
                builder.Append("   ");
                foreach (var c in l) builder.AppendFormat("{0:G1}", (char)c);
                builder.AppendLine();
            }
        }

        if (i > 0)
        {
            for (var j = 0; j < i; j++) builder.AppendFormat("{0:X2} ", l[j]);
            for (var j = l.Length+1; j > i; j--) builder.Append("   ");
            for (var j = 0; j < i; j++) builder.AppendFormat("{0:G1}", (char)l[j]);
            builder.AppendLine();
        }

        return builder.ToString();
    }
}
