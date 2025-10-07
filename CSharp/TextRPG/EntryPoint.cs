using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.TextRPG
{
    struct MageStruct
    {
        public int hp;
        public int attack;
    }
    class EntryPoint
    {
        static void KillMage( MageStruct m)
        {
            m.hp = 0;
        }

        static void EnterGame(Player player)
        {
            //Mage mage = (Mage) player;
            //mage.setMp(100);
            // 위의 코드는 문제가 생길 수 있음, compile time에서는 Player타입으로 어떤게 넘어올지 알 수 없음
            // Mage가 넘어올수도 Knight가 넘어올 수도 혹은 또 다른 클래스 타입이 넘어올수도 있는데
            // 여기서 explicit conversion을 사용하여 강제 형변환 할 경우 잘못된 casting을 하면 프로그램이 crash날수 있음

            // player is Mage는 player는 player 변수가 가리키는 실제 인스턴스의 타입이 Mage가 맞는 경우 true, 아닌 경우 false를 반환
            if(player is Mage) // player as Mage 로 대체할수도 있음 => 이 경우에는 player가 Mage타입의 인스턴스 인 경우 "Mage 인스턴스" 자체를 반환함. Mage mage = (player as Mage); 이런 형식으로 받아야 함
            // 하지만 대부분의 경우 player as Mage 를 사용하는게 맞음
            {
                Mage mage = (Mage)player;
                mage.setMp(100);
            }
        }
        static void Main(string[] args)
        {
            Knight knight = new Knight();

            knight.hp = 100;
            knight.attack = 10;

            knight.Move();
            knight.Attack();

            knight.KillKnight();

            MageStruct m = new MageStruct();
            m.hp = 100;
            m.attack = 50;
            KillMage(m);


            // shallow copy
            Knight knight2 = knight;
            knight2.hp = 80;
            // knight2와 knight가 가리키는 Knight 인스턴스가 같음
            Console.WriteLine(knight.hp); // 100 -> 80 

            // deep copy : 내부의 Clone 메소드를 통해 새로운 객체
            Knight knight3 = knight.Clone();
            knight3.hp = 120;
            Console.WriteLine(knight3.hp); // 새로 할당한 120
            Console.WriteLine(knight.hp); // 서로 다른 객체이므로 기존의 hp인 80



            // 생성자
            Knight knight4 = new Knight(150, 50);
            Knight knight5 = new Knight(100);

            Console.WriteLine($"기사4 체력: {knight4.hp}, 공격력: {knight4.attack}");
            Console.WriteLine($"기사5 체력: {knight5.hp}, 공격력: {knight5.attack}");

            // static
            Knight knight6 = Knight.CreateKnight();

            // 상속
            Archer archer1 = new Archer();
            Console.WriteLine($"아처 체력: {archer1.hp}, 아처 공격력: {archer1.attack}");

            // 은닉성
            SuperKnight SuperKnight = new SuperKnight();
            SuperKnight.SwordSkill();
        }
    }
}
