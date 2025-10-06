using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Etc
{
    // Event에 대하여


    // 해당 클래스는 사용자의 키보드 입력을 가로채어 동작을 제어하는 클래스
    // Observer pattern 사용한 구조 => 언어 차원에서 지원하는 거임 by event keyword
    class InputManager
    {
        public delegate void OnInputKey();
        public event OnInputKey InputKey;
        public void Update()
        {
            // 사용자가 입력한게 없으면 false => return으로 종료
            if (Console.KeyAvailable == false)
                return;

            // 사용자가 입력한 키를 읽어옴
            ConsoleKeyInfo info = Console.ReadKey();
            // 만약 사용자가 입력한 키가 A라면
            if (info.Key == ConsoleKey.A)
            {
                // 다른 모두에게 알려주기
                InputKey();
            }
        }
    }
}

/*
 * 
 * Delegate와 event 정리
 * 
 * C#에서 delegate는 "함수를 전달받는 수단" 이었음
 * event는 그 위에서 "이벤트를 구독/발행"하는 "안전한" 구조
 * 
 * 기존에 delegate를 보면,
 * delegate int OnClicked(); 이와 같이 delegate 타입을 정의하여 일종의 설계도를 만들어두고
 * 이 delegate함수를 인자로 받는 함수를 두어 특정 상황에서 발생할 함수들을 지정할 수 있었음
 * 
 * static void ButtonPressed( OnClicked clickedFunction ) 
 * 이런식으로 delegate 타입을 인자로 받는 함수를 만들어 "특정 시점"에 실행할 함수를 지정할 수 있었음 + chaining도 가능
 * 
 * 하지만 이러한 delegate type에는 치명적인 단점이 있음
 * 바로, 외부에서 delegate를 통째로 바꿀 수 있다는 것임
 * 
 * 우리가 기본적으로 실행할 함수들을
 * Onclicked onclick = new OnClicked(전달함수1);
 * onclick += 전달함수2;
 * 
 * 위와 같은 형식으로 추가하는데, delegate만 사용하는 경우에는 이 onclick delegate에 직접 접근하여 정보를 마음대로 수정할 수 있음
 * onclick = null; // 기존 정보 다 날라감
 * 
 * 그래서 안정성을 보장하고 오직 구독/구독해제만 가능하게 observer pattern에 맞춘 event를 사용함
 * 
 * 이 event를 사용하면
 * public event OnInputKey InputKey; // 이렇게 선언한 event의 delegate타입 변수인 InputKey에 대하여 직접적인 접근을 외부에서 할 수 없음
 * 오직 += 과 같은 구독/ 구독해제만 가능해짐
 * 
 * 
 */
