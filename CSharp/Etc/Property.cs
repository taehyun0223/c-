using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Etc
{
    class Knight
    {
        protected int hp;
        protected int attack;
        /*
         * getter / setter를 통해서만 변수에 접근할 수 있게 제한하고, 이후 확장성 및 안정성을 챙길 수 있음

        public int GetHp()
        {
            return hp;
        }

        public void SetHp(int hp)
        {
            // 특정 조건도 붙일수 있음
            this.hp = hp;
        }
        */

        // C#에서는 이러한 getter/setter를 쉽게 접근하기 위한 property를 제공함
        public int Hp
        {
            get { return hp; }
            set { hp = value; }
        }

        public int Attack
        {
            get { return attack; }
            private set { attack = value; }
        }

        public void AccessTest()
        {
            Attack = 10;
        }


        // 아예 property를 아주 쉽게 만들수 있는 문법도 있음
        public int Mp { get; set; } = 1000; // 뒤에 = 1000 을 통해 바로 초기화도 가능함
        /*
         * 
         * 위의 property는 아래의 코드를 함축한 것임
         * private int _mp;
         * public int GetMp() { return _mp; }
         * public void SetMp(int value) { _mp = value; }
         * 
         */

    } 
    class Property
    {
        static void Main(string[] args)
        {
            Knight knight = new Knight();

            knight.Hp = 100; // get 호출
            int hp = knight.Hp; // set 호출

            //knight.Attack = 100; -> property의 set을 private으로 선언했기 때문에 set은 해당 클래스 내부에서만 사용할 수 있음, 반면 get은 가능
            knight.AccessTest();
            int attack = knight.Attack;

            knight.Mp = 100;
            int mp = knight.Mp;
        }
    }
}
