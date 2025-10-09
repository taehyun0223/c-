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