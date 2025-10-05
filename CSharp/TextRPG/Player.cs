using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.TextRPG
{
    class Player
    {
        static public int counter = 1; // 클래스 안에 유일하게 한개만 존재

        public int id;
        public int hp;
        public int attack;

        public Player()
        {
            Console.WriteLine("player 생성자 호출");
        }

        public Player(int hp, int attack)
        {
            this.hp = hp;
            this.attack = attack;
        }

        // java에서는 일반적인 메소드를 자식 클래스에서 오버라이딩 하는 것이 허용됐음
        // 하지만 C#에서는 반드시 명시적으로 "virtual"키워드를 접근제어자 뒤에 붙여줘야만 함
        public virtual void SayHello()
        {
            Console.WriteLine("반가워 난 player야");
        }
    }
}
