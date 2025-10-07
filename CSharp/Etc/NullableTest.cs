using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Etc
{
    class NullableTest
    {
        class Monster
        {
            public int Id { get; set; }
        }

        // nullable => null값을 받을수 있도록 함
        static void Main(string[] args)
        {
            // number는 null을 받을 수 있도록함 => null 인 상태로 출력하려고 하면 exception발생
            int? number = null;

            // ??를 사용하면 number가 null이 아닐 경우에는 int b = number가 되고, number가 null일 경우에는 int b = 0으로 되도록 함
            int b = number ?? 0;


            Monster monster = null;

            // 기존 체크 방법
            if (monster != null)
            {
                int monsterId = monster.Id;
            }

            // nullable을 사용한 방식
            int? id = monster?.Id;
            // int type이거나 null일수 있는 id 변수에 
            // monster가 null이 아니라면 Id값을 가져오고
            // 만약 null이라면 id에 null값 반환


            // 이와 똑같은 동작을 아주 간단하게 int? id = monster?.Id; 로 축약 가능
            //if (monster == null)
            //{
            //    id = null;
            //}
            //else
            //{
            //    id = monster.Id;
            //}
        }

    }
}
