using System.Text;
using static Util;

class Program
{
    static void Main(string[] args)
    {
        Dump(Encoding.UTF8.GetBytes("Hello World! This is a test of hexdump output."));

        Interop.Test();
        Graph.Test();
    }
}
