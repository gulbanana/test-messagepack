using System;
using System.Text;
using MessagePack;

static class Util
{
    public static void Log(string s) => Console.WriteLine(s);

    public static void Dump<T>(T data) => Dump(MessagePackSerializer.Serialize(data));

    public static void Dump(byte[] data) => Console.WriteLine(Hexdump(data));

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