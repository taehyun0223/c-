using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.DataStructure
{
    class Dict
    {
        class Monster
        {
            public int id;

            public Monster(int num)
            {
                this.id = num;
            }
        }

        static void Main(string[] args)
        {
            Dictionary<int, Monster> dic = new Dictionary<int, Monster>();

            //dic.Add(1, new Monster(1));
            //dic[5] = new Monster(5);

            for (int i = 0; i < 10000; i++)
            {
                dic.Add(i, new Monster(i));
            }

            // dictionary를 직접적으로 값을 받아오는건 위험함 => exception으로 인한 crash
            // so, 제공해주는 함수 사용
            Monster mon;
            bool found = dic.TryGetValue(20000, out mon); // 20000번 몬스터는 없으므로 try get value로 mon변수에 값을 넣지는 않음, 그리고 반환으로 실패했으니 false반환

            bool found2 = dic.TryGetValue(7777, out mon); // 7777번 몬스터는 존재하므로 찾은 value를 mon에 집어넣고 반환값으로 true반환

            // 삭제
            dic.Remove(7777); // key값에 해당하는 내용 삭제
            dic.Clear(); // 전체 삭제


            // hash table을 사용하여 빠른 속도가 보장됨
            // 다만 메모리 공간 상에서 손해임 => 메모리 공간을 내주고 성능 취하기


            /*
             * Dictionary<TKey, TValue> 에 대한 자세한 설명
             * -> key(고유값)로 value에 O(1)에 가깝게 접근하는 "hash" 기반 컬렉션임
             * 
             * <사용하는 경우>
             * 1. 배열/리스트 처럼 index가 아니라 "key"로 바로 value를 찾고 싶은 경우
             * 2. 중복을 허용하지 않는 매핑 구조가 필요한 경우 ex) id값으로 특정 사용자 번호
             * 
             * <특징>
             * 1. 평균적으로 조회/추가/삭제 의 시간복잡도가 O(1)임.
             * -> 하지만 최악의 경우 충돌이 많으면 O(n) : 이 경우는 하나의 key에 모든 value가 달려있는 구조 마치 배열처럼
             * 
             * 2. key값은 중복되면 안되고 동시에 바뀌면 안됨
             * 
             * 3. key는 null값이 될 수 없으며 value는 가능함
             * 
             * 4. 여러 스레드에서 동시에 수정하려고 하면 안됨 => 별도의 ConcurrentDictionary 를 사용해야 함
             * 
             * 5. 반복문 안에서 수정하려고 하면 안됨
             * -> ex) foreach(var i in dict) { dict.Remove(i.key); } 이렇게 반복문 안에서 삭제하면 안됨
             * 
             * 6. value가 저장되는 bucket은 일정 크기 이상이 되면 자동으로 버킷 크기를 늘리고 알아서 다시 rehash함 => 미리 capacity를 정해주면 성능이 좀 좋아짐
             * 
             * 
             * <동작방법>
             * 1. key값을 넣으면 내부적으로 `GetHashCode()`를 호출해서 "hash값"을 얻음
             * 
             * 2. 얻은 해시 값으로 "bucket"이라는 칸에 저장함
             * -> 예를 들어, bucket이 10개라면 (hashCode % 10) 으로 어느 bucket에 들어갈지 정함
             * 
             * 3. 나중에 찾을때는 dict["apple"] 과 같이 들어오면 들어온 key값인 apple에 대해 GetHashCode()로 hash code값을 구한뒤,
             *  똑같이 (hashCode % 10) 을 통해 나머지를 구하여 저장된 bucket을 구하고, 그 bucket에 실제로 있는지 `Equals()` 를 통해 확인함
             *  
             * 4. 있으면 값을 꺼내는 방식으로 동작함
             * 
             * - 그래서 해시값 계산하고 그 버킷안에만 확인하면 되니깐 O(1)의 상수시간을 가지는 거임
             * But, 하지만 각 들어온 key값이 "같은 hash code"를 가질 수 있음
             * 이 때문에 최악의 경우 모두 같은 hash code를 가져 하나의 bucket에 모두 보관되는 경우에는 마치 배열과 같이 쭉 전부 보관되어 있기 때문에 O(n)이 걸린다는 거임
             */
        }
    }
}
