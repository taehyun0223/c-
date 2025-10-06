using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Etc
{
    class Generic
    {
        class MyList<T>
        {
            T[] arr = new T[10];

            public T GetItem(int i)
            {
                return arr[i];
            }

            public T[] GetList()
            {
                return arr;
            }
        }

        // generic을 사용하면 각 타입별로 전용 버전을 만들 필요없이 타입에 맞춰 생성가능함
        //class MyList1
        //{
        //    int[] arr = new int[10];
        //}

        //class MyList2
        //{
        //    float[] arr = new float[10];
        //}
        
        class Monster
        {

        }

        static void Test<T>(T input)
        {
            // 어떤 타입의 input이 들어와도 처리가능하도록 generic을 통한 일반화
        }

        class ConditionGeneric<T> where T : Monster { } // => 반드시 T타입 매개변수는 Monster 타입을 상속받아야 한다는 조건
        // class ConditionGeneric<T> where T : struct { } => 반드시 구조체여야 한다는 조건
        // class ConditionGeneric<T> where T : new() { } => 반드시 어떠한 매개변수도 없는 기본 생성자가 존재해야 한다는 조건
        // 위와 같이 generic의 타입 매개변수 클래스에 대한 "조건"을 걸수 있음

        static void Main(string[] args)
        {
            // var와 object

            // var : 뒤에 있는 실제 값을 보고 타입을 자동 추론해줌
            // object : 최상위 부모 클래스인 object에 모든 클래스를 담아서 표현할 수 있음

            MyList<int> myList1 = new MyList<int>();
            MyList<float> myList2 = new MyList<float>();
            MyList<Monster> myMonsterList = new MyList<Monster>();


            // generic을 통해 각 타입에 맞춰서 사용가능함
            int getInt = myList1.GetItem(0);
            Monster monster = myMonsterList.GetItem(0);
            


        }
    }
}
