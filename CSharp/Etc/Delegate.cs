using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Etc
{
    // delegate란 무엇이고 왜 쓰는지에 대해 먼저 이해해야 함
    // delegate는 "대리자" 라는 의미로 함수를 전달하여 특정 상황에서 해당 함수를 실행시킬 때 주로 사용함
    // 실생활에 빗대어 설명하면
    // A업체 사장에게 용건이 있어 만나려고하는데, 해당 A업체 사장이 자리를 비운 상태임
    // 이 사장의 비서가 있는데, 이 비서한테 "우리의 연락처와 용건"을 남기고
    // 사장이 "도착"하면 "거꾸로" 연락을 달라고 하는 것과 같음

    // 여기서 "연락처와 용건"은 전달하는 함수와 그 내용이고,
    // A업체 사장은 실행 주체이며,
    // A업체 사장의 비서가 delegate 함수가 됨
    // 그리고 사장이 도착하면 = 이벤트 발생시 / 그 내용을 대신 전달한다는 것은 해당 delegate함수가 호출될 때 인자로 전달된 함수를 대신 실행해달라는 것임

    // 요약하면 "delegate"는 함수를 전달하고 나중에 대신 실행시켜주는 대리자의 역할을 한다고 보면 됨 (인자로 함수를 전달함)
    class Delegate
    {
        static void ButtonPressed( OnClicked clickedFunction /* 함수 자체를 인자로 넘겨줌 */)
        {
            // 지정해둔 버튼이 클릭되는 이벤트가 발생하면 => 이 함수를 실행하여
            // 인자로 넘겨준 함수를 호출함
            clickedFunction();
        }

        // delegate => 함수 자체를 인자로 넘겨주는 형식
        // 반환 : int, 입력 : void
        // OnClicked라는 이름이 이 delegate의 형식 이름임 -> int type을 반환하면서 파라미터가 없는 함수
        // => 설계도의 역할을함
        delegate int OnClicked();

        // -> 위의 OnClicked와 같은 형식의 함수인 TestDelegate를 선언하고 이를 main함수에서 함수 자체를 인자로 전달함
        static int TestDelegate()
        {
            Console.WriteLine("delegate 테스트");
            return 0;
        }

        static int TestDelegate2()
        {
            Console.WriteLine("delegate 테스트2");
            return 0;
        }

        static void Main(string[] args)
        {
            // 이와 같이 직접적으로 delegate인스턴스를 만들고 거기에 직접적으로 함수를 연결할 수 있음
            OnClicked onclick = new OnClicked(TestDelegate);

            // 하나의 함수만 연결할 수 있는게 아니라, 여러개의 함수를 체이닝해서 연결할 수 있음
            onclick += TestDelegate2;

            // 함수 자체를 인자로 전달함 => delegate인 OnClicked의 signature를 가지는 TestDelegate를 메소드 인자로 전달
            ButtonPressed(TestDelegate);
        }
    }
}
