using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.DataStructure
{
    internal class List
    {
        static void Main(string[] args)
        {
            // 하지만 "배열(array)"의 경우는 "고정된" 크기임
            // 크기에 맞춰서 사용할 수 없을까? => List 자료구조가 나옴
            // 동적 배열의 List

            List<int> list = new List<int>();

            for (int i = 0; i < 5; i++)
            {
                list.Add(i);
            }

            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(list[i]);
            }

            foreach (int i in list)
            {
                Console.WriteLine(i);
            }

            // 삽입 & 삭제
            list.Insert(2, 999); // 두번째 index에 999 를 삽입
            list.Remove(3); // 제일 "첫번째"로 만나는 3을 삭제, bool 값을 반환 => 제대로 삭제되었는지 true/false 반환
            list.RemoveAt(2); // index를 지정하여 특정 index의 값을 삭제
            list.Clear(); // 전체 삭제

            // 배열의 경우 특정 index에 삽입/삭제 하는 경우
            // -> 삽입할때 뒤에 있는 모든 요소들을 한칸씩 뒤로 밀어야함 => 비 효율적
            // -> 삭제할때 없애고 나서 뒤의 모든 요소들을 한칸씩 앞당겨야 함 => 비효율적
            // -> 삽입 혹은 삭제의 query가 많은 경우에는 다른 자료구조를 고려해보는 것이 좋을 수 있음
            // 유지보수 <-> 효율성 이 사이에서 적절하게 판단해야함
        }
    }
}
