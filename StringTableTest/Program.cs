using System;
using BuildXL.Utilities;

namespace StringTableTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var st = new StringTable();
            var sid1 = st.AddString("Hello World!");
            var sid2 = st.AddString("Hello" + " World!");
            Console.WriteLine(st.GetString(sid1));
            Console.WriteLine(st.GetString(sid2));
            Console.WriteLine(sid1 == sid2);
        }
    }
}
