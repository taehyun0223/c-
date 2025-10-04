using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp
{
    class Debug
    {
        static void Print(int value)
        {
            Console.WriteLine(value);
        }

        static int AddAndPrint(int a, int b)
        {
            int ret = a + b;
            Print(ret);
            return ret;
        }
        static void Main(string[] args)
        {
            int ret = Debug.AddAndPrint(10, 20);
        }
    }
}
