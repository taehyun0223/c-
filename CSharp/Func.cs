using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp
{
    class Func
    {
        static void writeHello()
        {
            Console.WriteLine("---1. 메소드 테스트---");
            Console.WriteLine("Hello World!");
        }

        static void addOne(ref int num)
        // ref keyword를 통해 참조값을 전달할 수 있음 => 원래 primitive type의 경우에는 전달하여도 값을 복사해서 대입하기 때문에,
        // 실제 데이터 값에는 변화가 없음
        // 하지만 ref를 통해 실제 참조값을 전달하고
        // 그 참조값을 통해 값을 조정하는 경우에는 실제 값이 변경됨
        {
            Console.WriteLine("---2. 참조값 전달 테스트---");
            num++;
        }

        static int addOne2(int num)
        {
            Console.WriteLine("---3. 값 복사 전달 후 결과 받아오기 테스트---");
            return num + 1;
        }

        static void swap(ref int num1, ref int num2) 
        {
            Console.WriteLine("---4. 두 변수의 참조값 전달하여 실제 변수값 swap---");
            int temp = num1;
            num1 = num2;
            num2 = temp;
        }

        // 만약 여러개의 값을 반환해야 하는 경우 "out" 키워드를 사용하여 반환되는 값을 지정할 수 있음
        static void divide(int a, int b, out int result1, out int result2)
        {
            Console.WriteLine("---5. out 키워드를 통해 여러개의 출력값 지정하기---");
            result1 = a / b;
            result2 = a % b;
        }

        static void Main(string[] args)
        {
            writeHello();
            // Func.writeHello();

            int num = 1;
            addOne(ref num); // 매개변수에 대한 인자값으로 전달할 경우에도 ref키워드를 통해 명시적으로 참조값을 전달해야 함
            Console.WriteLine(num);

            int result1 = addOne2(num);
            Console.WriteLine(result1);

            int num1 = 1;
            int num2 = 5;
            swap(ref num1, ref num2);
            Console.WriteLine(num1);
            Console.WriteLine(num2);


            int a = 10;
            int b = 3;

            int result2;
            int result3;
            divide(a, b, out result2, out result3);

            Console.WriteLine(result2);
            Console.WriteLine(result3);


        }
    }
}
