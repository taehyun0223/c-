namespace CSharp.section3;

public class PriorityQueue<T> where T : IComparable<T>
{
    private List<T> _heap = new List<T>();
    
    // O(log N) : 연산량의 증가가 tree의 depth에 의해 영향을 받음, 이때 tree는 binary tree로 depth n마다 2^n 만큼 수용할 수 있음,
    // 그러므로 특정 depth에 대하여 push 혹은 pop은 depth 길이만큼의 log2(N) 만큼 연산을 수행하므로 Order of N은 O(log N) 이 된다.
    public void Push(T data)
    {
        // 1. heap의 맨 끝에 새로운 데이터를 삽입한다
        _heap.Add(data);
        
        // 2. 맨 마지막에 넣은 새로운 데이터와 그 부모를 비교
        int now = _heap.Count - 1;
        while (now > 0)
        {
            int next = (now - 1) / 2;
            if (_heap[now].CompareTo(_heap[next]) < 0)
            {
                break;
            }
            /*if (_heap[now] < _heap[next])
                break;*/
            
            // 3. 더 클 경우에는 교체
            T temp = _heap[now];
            _heap[now] = _heap[next];
            _heap[next] = temp;
            //(_heap[now], _heap[next]) = (_heap[next], _heap[now]);
            
            // 4. 다음 비교를 위해 교체한 위치로 검사 커서 이동
            now = next;
        }
    }

    // O(log N)
    public T Pop()
    {
        // 제일 큰 값을 내보내야 함
        
        // 1. 반환할 데이터 저장
        T ret = _heap[0];
        
        // 2. 마지막 요소를 최상위로 옮김
        int lastIndex = _heap.Count - 1;
        _heap[0] = _heap[lastIndex];
        _heap.RemoveAt(lastIndex);
        lastIndex--;
        
        // 3. 교체한 맨 마지막요소에서 "역"으로 위에서 아래로 비교
        int now = 0;
        while (true)
        {
            // 왼쪽은 x2에 1 더한값, 오른쪽은 x2에 2더한값
            int left = 2 * now + 1;
            int right = 2 * now + 2;
            // 현제 위치인 now에서
            int next = now;
            // 왼쪽 자식과 비교하여 (단, 마지막 index보다 작거나 같아야함, 그리고 그 값이 현재 값보다 커야함)
            if (left <= lastIndex && /*_heap[next] < _heap[left]*/ _heap[next].CompareTo(_heap[left]) < 0)
            {
                next = left;
            }
            // 오른쪽 자식과 비교하여 (단, 마지막 index보다 작거나 같아야함, 그리고 그 값이 현재 값보다 커야함(반드시 왼쪽 자식보다 커야함))
            if (right <= lastIndex && /*_heap[next] < _heap[right]*/ _heap[next].CompareTo(_heap[right]) < 0)
            {
                next = right;
            }
            
            // 4. 왼쪽 오른쪽 모두 현재값보다 작으면 종료
            if (next == now)
            {
                break;
            }
            
            
            // 5. 더 클 경우에는 교체
            T temp = _heap[next];
            _heap[next] = _heap[now];
            _heap[now] = temp;
            
            // 6. 커서 이동
            now = next;
        }
        
        
        return ret;
    }

    public int Count
    {
        get { return _heap.Count; }
    }
}