using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Etc
{
    // Reflection이란? 쉽게 말해 X-Ray를 찍는것과 같다고 볼 수 있음

    // Unity에서는 C#의 reflection기능을 통해 C# script의 내용을 unity 화면에서 쉽게 확인하고 조정할 수 있도록 함
    // public인지 아닌지에 따라 unity화면에 보이게 할지 안할지도 설정가능
    // unity에서는 Attribute를 통해 [SerializeField]를 입력하게 하면 private으로 되어있는 변수더라도 unity화면에서 조정가능하도록 attribute를 제공함

    class Important : System.Attribute
    {
        string message;

        public Important(string message)
        {
            this.message = message;
        }
    }
    class MonsterEx
    {
        // 주석 주석 주석 중요한 주석이에요
        [Important("매우 매우 매우 중요한 주석")]
        public int hp;
        protected int attack;
        private float speed;

        void Attack() { }
    }

    class Reflect
    {
        // reflection은 클래스의 정보를 "runtime"에서 모두 가져올 수 있음

        static void Main(string[] args)
        {
            MonsterEx monster = new MonsterEx();
            Type type = monster.GetType();

            var fields = type.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Instance);

            foreach ( FieldInfo field in fields )
            {
                string access = "protected";
                if (field.IsPublic)
                    access = "public";
                else if (field.IsPrivate)
                    access = "private";

                var attributes = field.GetCustomAttributes();

                Console.WriteLine($"{access} {field.FieldType.Name} {field.Name}");
            }
        }
    }
}
