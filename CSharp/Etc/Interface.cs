using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Etc
{
    // abstract keyword로 추상클래스화 => 인스턴스 생성 불가하도록 하게 함, 그리고 반드시 abstract method를 상속받는 클래스에서 override하도록 강제함
    abstract class Monster
    {
        public abstract void Shout();
        
    }

    //abstract class Flyable
    //{
    //    public abstract void Fly();
    //}

    // 인터페이스 특징 답게 메소드를 반드시 구현하는 클래스에서 "반드시" 구현하도록 강제함
    interface IFlyable
    {
        void Fly();
    }

    class Orc : Monster
    {
        public override void Shout()
        {
            Console.WriteLine("Orccccc!");
        }
    }

    class FlyableOrc : Orc, IFlyable 
    {
        public void Fly()
        {
            Console.WriteLine("오크.. 날다");
        }
    }

    // -> 한번에 여러개의 클래스를 다중 상속할 수 없음 => diamond 문제 발생함, 같은 메소드에 대하여 어떤 클래스의 메소드를 사용해야 하는지에 대한 문제
    //class FlyableOrc : Orc, Flyable
    //{

    //}

    class Skeleton : Monster
    {
        public override void Shout()
        {
            Console.WriteLine("Skelllllll!");
        }
    }

    class Interface
    {
        static void DoFly(IFlyable flyable)
        {
            flyable.Fly();
        }

        static void Main(string[] args)
        {
            FlyableOrc orc = new FlyableOrc();
            DoFly(orc);
        }
    }
}
