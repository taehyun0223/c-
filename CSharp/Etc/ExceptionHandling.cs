using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Etc
{
    class TestException : Exception
    {

    }

    class ExceptionHandling
    {
        static void TestFunc()
        {
            throw new TestException();
        }

        static void Main(string[] args)
        {
            // 오류를 잡아서 별도 처리

            try
            {
                // ex1. 0으로 나눌때
                // ex2. 잘못된 메모리 참조 => null 참조
                // ex3. 오버플로우

                //int a = 10;
                //int b = 0;
                //int result = a / b;


                //throw new TestException(); //인위적으로 예외 발생시킴


                TestFunc(); // 함수 안에서 발생하는 예외도 잡아서 catch됨
            }
            catch (DivideByZeroException e) // exception을 catch하는 순서에도 영향이 있음 => 위에 있는거 먼저 해당하면 아례 catch문 실행x
            {

            }
            catch (Exception e)
            {

            }
            finally
            {
                // 예외가 발생하더라도 반드시 실행해야 하는 구문 ex) db 혹은 파일 처리
            }
        }
    }
}
