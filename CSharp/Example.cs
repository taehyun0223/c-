using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp
{
    class Example
    {
        static void Main(string[] args)
        {
            for (int i = 2; i < 10; i++)
            {
                for(int j = 1;j < 10; j++)
                {
                    Console.WriteLine($"{i} x {j} = {i*j}");
                }
            }

            for(int i=0; i<5; i++)
            {
                for(int j = 0; j<=i; j++)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }

            for(int i = 1;i <= 5; i++)
            {
                Console.WriteLine(new string('*', i));
            }

            int ret = Factorial(5);
            Console.WriteLine(ret);

            int ret2 = Factorial2(5);
            Console.WriteLine(ret2);


        }

        static int Factorial2(int n)
        {
            if (n <= 1)
                return 1;
            return n * Factorial2(n - 1);
        }

        static int Factorial(int n)
        {
            int result = 1;
            for(int i = n; i >= 1; i--)
            {
                result = result * i;
            }

            return result;
        }
    }
}
