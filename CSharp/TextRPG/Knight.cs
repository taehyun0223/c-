using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.TextRPG
{
    class Knight : Player
    {
        protected int swordDamage = 5;

        static public void Test()
        {
            // static method는 class에 종속되는 것이므로 각 개별 인스턴스 정보에는 접근할 수 없음
            // 즉, static 변수 혹은 또다른 static 메소드에는 접근가능하지만 다른 변수들에는 접근 불가능 => static method는 compile time에서 정해지는데,
            // 인스턴스 변수는 runtime에서 객체가 생성되면서 할당되기 때문에 알 수 없음
            
            // this.id = counter++; -> 불가능
            counter++;
        }

        static public Knight CreateKnight()
        {
            // 하지만 이와 같이 static method 내부에서 직접 인스턴스를 만들고 접근하는 것은 가능함
            Knight knight = new Knight();
            knight.hp = 100;
            knight.attack = 1;
            return knight;
        }

        //public Knight()
        //{
        //    this.id = counter++;
        //    this.hp = 0;
        //    this.attack = 0;
        //}

        public Knight()
        {
            Console.WriteLine("knight 생성자 호출");
        }


        public Knight(int hp) : this()
        {
            this.hp = hp;
        }

        public Knight(int hp, int attack)
        {
            this.id = counter++;
            this.hp = hp;
            this.attack = attack;
        }

        
        public Knight Clone()
        {
            Knight knight = new Knight();
            knight.hp = hp;
            knight.attack = attack;
            return knight;
        }

        public void Move()
        {
            Console.WriteLine("knight move");
        }

        public void Attack()
        {
            Console.WriteLine("knight attack");
        }

        public void KillKnight()
        {
            hp = 0;
        }

        // java에서는 @Override 어노테이션을 달았지만, C#에서는 override라는 키워드를 접근제어자 뒤에 붙여서 오버라이딩됨을 알려야함
        // 공유하는 쪽에서는 반드시 virtual 키워드를 붙여야함
        public override void SayHello()
        {
            Console.WriteLine("반가워 난 기사야"); ;
        }
    }

    class SuperKnight : Knight
    {
        public void SwordSkill()
        {
            Console.WriteLine($"소드 데미지 4배 공격 : {swordDamage * 4}");
        }

        // sealed를 통해 더이상 override 하지 못하도록 막을 수 잇음
        // 마치 java에서의 final 키워드와 동일한 역할을 함
        public sealed override void SayHello()
        {
            base.SayHello();
            Console.WriteLine("나는 기사의 최상위클래스야");
        }

        //만약 부모의 메소드를 재정의하는 '것이 목적이 아니라,
        // 그냥 가리는 목적이라묜 new 키워드를 쓰면 됨
        // 이 경우에는 오버라이딩이 아니라서 만약 부모 키워드의 참조변수에서 해당 인스턴스를 가리키고 메소드를 사용할 경우
        // 부모에 정의된 Move가 사용됨 => 재정의 목적이 아니라 가리는 목적이기 때문에 부모 메소드가 살아 있음

        public new void Move()
        {
            Console.WriteLine("super knight move");
        }
    }
}
