namespace CSharp.Exercise;

public class Graph
{
    int[,] adj = new int[6, 6]
    {
        { 0, 1, 0, 1, 0, 0 },
        { 1, 0, 1, 1, 0, 0 },
        { 0, 1, 0, 0, 0, 0 },
        { 1, 1, 0, 0, 1, 0 },
        { 0, 0, 0, 1, 0, 1 },
        { 0, 0, 0, 0, 1, 0 },
    };

    List<int>[] adj2 = new List<int>[]
    {
        new List<int>() { 1, 3 },
        new List<int>() { 0, 2, 3 },
        new List<int>() { 1 },
        new List<int>() { 0, 1, 4 },
        new List<int>() { 3, 5 },
        new List<int>() { 4 },
    };
    
    // 끊어진 vertex가 있는 그래프
    
    int[,] adj3 = new int[6, 6]
    {
        { 0, 1, 0, 1, 0, 0 },
        { 1, 0, 1, 1, 0, 0 },
        { 0, 1, 0, 0, 0, 0 },
        { 1, 1, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 1 },
        { 0, 0, 0, 0, 1, 0 },
    };

    List<int>[] adj4 = new List<int>[]
    {
        new List<int>() { 1, 3 },
        new List<int>() { 0, 2, 3 },
        new List<int>() { 1 },
        new List<int>() { 0, 1 },
        new List<int>() { 5 },
        new List<int>() { 4 },
    };
    
    // weighted graph
    int[,] adj5 = new int[6, 6]
    {
        { -1, 15, -1, 35, -1, -1 },
        { 15, -1, 5, 10, -1, -1 },
        { -1, 5, -1, -1, -1, -1 },
        { 35, 10, -1, -1, 5, -1 },
        { -1, -1, -1, 5, -1, 5 },
        { -1, -1, -1, -1, 5, -1 },
    };
    
    // DFS

    public void PrintList()
    {
        foreach (var val in adj2)
        {
            foreach (var element in val)
            {
                Console.Write(element);
            }

            Console.WriteLine();
        }
    }

    private bool[] visited = new bool[6]; // 방문 여부 트래킹
    public void DFS(int now) // now: 시작지점
    {
        // 1. now부터 방문
        // 2. now와 연결된 vertex들을 하나씩 확인해서 "방문하지 않은 상태"라면 방문한다
        
        
        Console.WriteLine($"방문 vertex : {now}");
        visited[now] = true; // 1. now 방문처리

        for (int next = 0; next < adj.GetLength(0); next++)
        {
            if(adj[now, next] == 0) // 연결되어 있지 않으면 스킵
                continue;
            if (visited[next]) // 이미 방문했으면 스킵
                continue;
            DFS(next);            
        }
    }

    public void DFS2(int now)
    {
        // 1. now부터 방문
        // 2. now와 연결된 vertex들을 하나씩 확인해서 "방문하지 않은 상태"라면 방문한다
        
        Console.WriteLine($"방문 vertex : {now}");
        visited[now] = true; // 1. now 방문처리

        foreach (int next in adj2[now]) // 연결된 애들만 꺼냄, foreach
        {
            if (visited[next]) // 만약 방문했으면 스킵
                continue;
            DFS2(next);
        }
    }

    public void SearchAll() // 모든 vertex는 연결되어 있다는 보장이 없음, 그러므로 "모든 vertex"를 적어도 한번은 방문하게 하기 위해서 해당 메소드를 통한 접근
    {
        visited = new bool[6];
        for (int now = 0; now < 6; now++)
        {
            if (visited[now] == false)
            {
                DFS(now);
            }
        }
    }
    
    // BFS

    public void BFS(int start)
    {
        bool[] found = new bool[6];
        int[] parent = new int[6];
        int[] distance = new int[6];
        
        Queue<int> q = new Queue<int>();
        q.Enqueue(start);
        found[start] = true;
        parent[start] = start;
        distance[start] = 0;

        while (q.Count > 0) // 큐에 요소가 남아 있는 동안은 반복
        {
            int now = q.Dequeue();
            Console.WriteLine($"꺼낸 요소 : {now}, 부모: {parent[now]}, 거리: {distance[now]}");

            for (int next = 0; next < 6; next++)
            {
                if (adj[now, next] == 0)
                    continue;
                if (found[next])
                    continue;
                q.Enqueue(next);
                found[next] = true;
                parent[next] = now;
                distance[next] = distance[now] + 1;
            }
        }
    }
    
    // Dijkstra

    public void Dijkstra(int start)
    {
        bool[] visited = new bool[6]; // 실제 각 vertex를 방문했는지 기록
        int[] distance = new int[6]; // 각 vertex까지의 거리(가중치)
        int[] parent = new int[6];
        Array.Fill(distance, Int32.MaxValue); // 모든 거리를 0으로 초기화하면 안됨 => 초기지점이 0임

        distance[start] = 0; // 시작지점
        parent[start] = start;

        while (true)
        {
            // 가장 가까이에 있는 vertex 찾기
            
            //초기값
            int closest = Int32.MaxValue;
            int now = -1;
            
            // 모든 vertex를 순회하면서
            for (int i = 0; i < 6; i++)
            {
                // 이미 방문한 vertex는 skip
                if (visited[i])
                {
                    continue;
                }
                
                // 아직 발견된적 없거나 기존 후보보다 멀리 있으면 skip
                if (distance[i] == Int32.MaxValue || distance[i] >= closest)
                {
                    continue;
                }
                
                // 위 두개에서 방문하지 않았고 발견된적 있으면서 동시에 기존 후보보다 작은경우
                // 새롭게 후보로 선정하여 정보 갱신
                closest = distance[i];
                now = i;
            }
            
            // 다음 후보가 하나도 없는 경우
            if (now == -1)
            {
                break;
            }
            
            // 제일 좋은 후보를 찾았으니 방문 시도
            visited[now] = true;
            
            // 방문한 vertex와 인접한 vertex들을 조사
            // -> 상황에 따라 발견한 최단거리를 갱신
            for (int next = 0; next < 6; next++)
            {
                // 연결되지 않았으면 스킵
                if (adj5[now, next] == -1)
                {
                    continue;
                }
                
                // 이미 방문한 vertex는 스킵
                if (visited[next])
                {
                    continue;
                }
                
                // 한번도 방문x 연결된 vertex의 최단 거리 갱신
                int nextDist = distance[now] + adj5[now, next];
                
                // 만약 기존에 발견된 최단거리가 새로 조사된 최단거리보다 크면 => 새롭게 발견된 루트가 더 최단거리면 정보 갱신
                if (nextDist < distance[next])
                {
                    distance[next] = nextDist;
                    parent[next] = now;
                }
            }
        }
    }
}

public class Program
{
    
    static void Main(string[] args)
    {
        Stack<int> stack = new Stack<int>();
        
        stack.Push(101);
        stack.Push(102);
        stack.Push(103);
        stack.Push(104);
        stack.Push(105);

        int data = stack.Pop(); // 마지막 요소 꺼내기 (stack 비워져 있을때 사용시 오륲)
        int data2 = stack.Peek(); // 마지막 요소 꺼내진 않고 확인만

        Queue<int> queue = new Queue<int>();
        
        queue.Enqueue(101);
        queue.Enqueue(102);
        queue.Enqueue(103);
        queue.Enqueue(104);
        queue.Enqueue(105);

        int data3 = queue.Dequeue(); // 맨 앞 요소 꺼내기 (맨 처음 들어간 요소)
        int data4 = queue.Peek();
        
        
        // DFS
        
        Graph graph = new Graph();
        //Console.WriteLine("일반 2차원 배열 사용");
        //graph.DFS(3);
        
        // Console.WriteLine("List 사용");
        // graph.DFS2(3);

        Console.WriteLine("모든 vertex 방문하는 DFS");
        graph.SearchAll();
        
        
        // BFS
        
        graph.BFS(0);
        
    }
    
    /*
     * 그래프 간략 설명
     *
     * 현실 사물이나 추상 개념간의 "연결 관계"를 표현할 때 사용
     * vertex와 edge로 구성됨
     * 
     */
}