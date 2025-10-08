namespace CSharp;

public class Board
{
    // 게임 정보를 담을 자료구조는 아래와 같이 여러개가 선정될 수 있음
    // 각각의 자료구조에 대한 특성을 이해하기 상황에 맞게 어떻게 설정해야 할지 선택해야 함
    public int[] _data = new int[25]; // 배열
    public MyList<int> _data2 = new MyList<int>(); // 리스트
    public MyLinkedList<int> _data3 = new MyLinkedList<int>(); // 연결리스트
    
    /*
     * 선형 자료구조 vs 비선형 자료구조
     *
     * -> 선형 : 자료를 순차적으로 나열한 형태 : 배열, Linked list, stack, queue
     * -> 비선형 : 하나의 자료 뒤에 다수의 자료가 올 수 있는 형태 : tree, graph
     *
     *
     * 배열 vs 동적 배열(리스트) vs 연결리스트
     *
     * - 배열
     * => 크기가 고정적, 변경불가
     * => 연속적인 공간에 할당됨
     *
     * - 리스트 (동적배열)
     * => 크기를 유동적으로 조절할 수 있음
     * => 연속적인 공간에 할당됨
     * => 기본 크기가 있고, 그 크기를 오버하는 경우에 새로운 연속적인 공간에 더 큰 공간을 할당 (일반적으로 2배씩)
     * => 이때 확장시 data를 옮기는(복사) 것에 대한 비용문제
     *
     * - 연결리스트
     * => 연속되지 않은 공간에 할당
     * => 추가/삭제에 용이함
     * => 하지만 특정 index의 data에 바로 접근할 수 없음 (random access 불가)
     *
     *| 연산         | 배열(Array) | 동적 배열(List<T>)           | 연결 리스트(LinkedList<T>)   |
      | ---------- | --------- | ------------------------ | ----------------------- |
      | 인덱스로 접근    | **O(1)**  | **O(1)**                 | ❌ **O(n)**              |
      | 맨 끝 삽입     | ❌ (고정)    | 평균 **O(1)**, 최악 **O(n)** | **O(1)**                |
      | 중간 삽입      | **O(n)**  | **O(n)**                 | **O(1)** (노드 위치 알고 있다면) |
      | 삭제         | **O(n)**  | **O(n)**                 | **O(1)** (노드 위치 알고 있다면) |
      | 탐색(Search) | **O(n)**  | **O(n)**                 | **O(n)**                |
     *
     * 메모리 측면 차이

        - 배열 / 동적 배열

            1. 메모리 효율 좋음 (데이터만 연속적으로 존재)

            2. 확장 시 새 메모리 블록 할당 후 전체 복사 필요

         - 연결리스트

            1. 각 노드마다 data + next pointer (+ prev pointer)를 저장

            2. 따라서 메모리 사용량 더 많음

            3. CPU 캐시 효율도 나쁨 → 실제 실행 속도 느림

         <즉, “이론상 빠르지만 실제로는 캐시 때문에 느린 경우 많음” → 실무에서도 자주 묻는 포인트>
     *
     *
     * [ ?? 왜 CPU 캐시 때문에 오히려 더 성능이 안좋아지는 걸까? ]
     * 
     * 배열/리스트: 메모리 연속 → 캐시 히트율 높음 → 빠름
     * 연결리스트: 메모리 흩어짐 → 캐시 미스 자주 발생 → 실제 성능 느림
     *
     *
     * [ 그러면 언제 어느걸 사용해야 하는가? ]
     
     | 상황             | 추천 구조           | 이유         |
     | -------------- | --------------- | ---------- |
     | 요소 개수 예측 가능    | `Array`         | 빠르고 단순     |
     | 자주 추가/삭제 (끝부분) | `List<T>`       | 평균 O(1)    |
     | 중간 삽입/삭제 많음    | `LinkedList<T>` | 구조상 유리     |
     | 탐색 많이 함        | `List<T>`       | 랜덤 접근 O(1) |
     | 캐시 성능 중요       | `List<T>`       | 연속 메모리라 빠름 |

     *
     *
     * 기억 해두면 좋은 내용
     *
     * 1. Resizable policy: List<T>는 용량이 꽉 차면 2배로 확장 (Capacity *= 2)
     * 2. TrimExcess(): 남는 버퍼 메모리 줄이는 List<T> 메서드
     * 3. LinkedList<T>의 노드 탐색은 항상 O(n) → Find() 비용 주의
     * 4. 배열 복사 최적화: Array.Copy() vs Buffer.BlockCopy() (후자가 더 빠름)
     * => 단, 일반적인 배열복사는 .Copy() 사용하는 것이 낫고, 고성능 대량 primitive type 데이터 복사의 경우에만 BlockCopy()
     * => 주로 image buffer, 오디오 샘플, binary file 처리와 같은 순수 data 배열 복사할때 사용
     * 5. Span<T> / Memory<T> (in C#): 배열처럼 연속 메모리 다루지만 할당 안 함 → 고성능 코드에서 대체재로 사용됨
     * => 즉, 할당 없이 배열을 다룰 수 있게 해줌
     * => 우리가 보통 특정 배열을 "부분적"으로 다루려면 새 배열을 만들던가 특수 문법을 사용해야 했음
     * => Span<T>는 “이미 존재하는 메모리 조각”을 복사 없이 슬라이스해서 다룰 수 있게 해줌
     * => GC(가비지 컬렉션) 부담 X → 메모리 할당이 일어나지 않음
     * => 대규모 배열, 버퍼 처리, 문자열 파싱, 네트워크 패킷 처리 등에서 큰 차이
     * => Span<T>는 스택 기반이므로 메서드 리턴 불가 -> 리턴이 필요할 땐 Memory<T>를 대신 사용
     */
    
    public void Initialize()
    {
        _data2.Add(101);
        _data2.Add(102);
        _data2.Add(103);
        _data2.Add(104);
        _data2.Add(105);

        int temp = _data2[2];
        
        _data2.RemoveAt(2); // index의 요소 삭제
        
        
        _data3.AddLast(101);
        _data3.AddLast(102);
        MyLinkedListNode<int> node = _data3.AddLast(103);
        _data3.AddLast(104);
        _data3.AddLast(105);

        _data3.Remove(node);
    }
}

public class MyLinkedListNode<T>
{
    public T Data;
    public MyLinkedListNode<T> Next; // 다음 노드 주소
    public MyLinkedListNode<T> Prev; // 이전 노드 주소
}


public class MyLinkedList<T>
{
    public int Count = 0;
    public MyLinkedListNode<T> Head = null; // 첫번째 노드
    public MyLinkedListNode<T> Tail = null; // 마지막 노드

    public MyLinkedListNode<T> AddLast(T data)
    {
        
        // 시간복잡도 : O(1)
        MyLinkedListNode<T> newRoom = new MyLinkedListNode<T>();
        newRoom.Data = data;
        
        // 기존에 만약 아무런 Room이 없었을 경우에는 그 Room을 Head로 설정
        if (Head == null)
        {
            Head = newRoom;
        }
        
        // 만약 기존에 데이터가 있는 경우, 그 마지막 노드의 다음값을 새로운 노드로 설정하고, 새로운 노드의 이전값을 기존 마지막 노드로 설정한다
        if (Tail != null)
        {
            Tail.Next = newRoom;
            newRoom.Prev = Tail;
        }
        
        // 새롭게 추가되는 노드를 마지막 노드로 설정한 후, 갯수 증가하고 반환
        Tail = newRoom;
        Count++;
        return newRoom;
    }
    
    // 시간복잡도 : O(1)
    public void Remove(MyLinkedListNode<T> room)
    {
        // 만약 제거 대상이 맨 앞 노드라면 다음 데이터를 Head로 하면 됨 (만약 다음 노드가 없더라도 자동적으로 null을 이어주기 때문에 문제x)
        if (Head == room)
            Head = Head.Next;
        
        // 맨 마지막 노드를 삭제하는 경우, 마지막 이전 노드를 마지막 노드로 이어주기만 하면됨
        if (room == Tail)
            Tail = Tail.Prev;

        if (room.Prev != null)
            room.Prev.Next = room.Next;

        if (room.Next != null)
            room.Next.Prev = room.Prev;

        Count--;
    }
}

public class MyList<T>
{
    private const int DEFAULT_SIZE = 1;
    T[] _data = new T[DEFAULT_SIZE];
    
    public int Count = 0; // 실제로 사용중인 데이터 갯수
    public int Capacity // 예약된 데이터 갯수
    {
        get { return _data.Length; }
    }

    
    // O(1) 시간복잡도 예외케이스
    // 아래에 for문이 N개 입력에 대하여 선형증가하기 때문에 O(N)으로 보이지만,
    // 애초에 if (Count >= Capacity) 조건에서만 실행되기 때문에 => O(1)로 봄
    public void Add(T item)
    {
        // 1. 먼저 공간이 충분히 남아있는지 확인
        if (Count >= Capacity)
        {
            // 공간 부족상태 => 공간 확보해야함
            T[] newArr = new T[Capacity * 2];
            for (int i = 0; i < Count; i++)
            {
                newArr[i] = _data[i]; // 그대로 마이그레이션
            }
            _data = newArr;
        }
        // 2. 공간이 확보되면 공간에 데이터를 넣어줌
        _data[Count] = item;
        Count++;
    }

    
    // class를 배열처럼 사용할 수 있게해주는 indexer임
    // 즉, `[]` 연산자를 class안에 직접 정의한 것임
    // 이렇게 하면 클래스에 대한 객체를 배열처럼 "myList[0]" 이런식으로 접근할 수 있게 해줌
    // 아래에서는 MyList<int> myList = new MyList<int>(); 이렇게 선언된 경우
    // myList[0] 이렇게 접근하면 자동으로 객체 안의 _data 배열의 0번째 인덱스에 접근할 수 있다는 거임
    public T this[int index]
    // O(1) 시간복잡도
    {
        get { return _data[index]; }
        set { _data[index] = value; }
    }
    
    // O(N) 시간복잡도
    public void RemoveAt(int index)
    {
        for (int i = index; i < Count - 1; i++)
        {
            _data[i] = _data[i + 1];
        }

        _data[Count - 1] = default(T); // Count-1 번째 인덱스의 타입에 맞춰 int type이면 0으로 클래스 타입이면 null로 맞춰서 설정
        Count--;
    }
}