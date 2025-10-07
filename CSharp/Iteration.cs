using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp
{
    class Iteration
    {
        static void Main(string[] args)
        {
            // 1. while
            int count = 5;
            while (count > 0)
            {
                Console.WriteLine(count);
                count--;
            }

            // 2. do-while : 적어도 한번은 실행
            string answer;
            do
            {
                Console.WriteLine("y를 입력해주세요 : ");
                answer = Console.ReadLine();
            } while (answer != "y");

            // 3. for
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(i);
            }

            // 4. break && continue
            int userInt = int.Parse(Console.ReadLine());
            bool result = true;

            for (int i = 2; i < userInt; i++)
            {
                if (userInt % i == 0)
                {
                    result = false;
                    break;
                }
            }

            result = IsPrime(userInt);
            Console.WriteLine(result);

            int how = int.Parse(Console.ReadLine());
            for (int i = 0; i < how; i++)
            {
                if (!IsPrime(i))
                {
                    continue;
                }
                Console.WriteLine($"{i}는 소수입니다");
            }

        }

        private static bool IsPrime(int n)
        {
            if (n < 2)
            {
                return false;
            }
            else if (n == 2)
            {
                return true;
            }
            else if ((n % 2) == 0)
            {
                return false;
            }

            for (int i = 3; i * i <= n; i += 2)
            {
                if (n % i == 0)
                {
                    return false;
                }
            }
            return true;

        }
    }
}
