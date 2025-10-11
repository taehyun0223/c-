namespace CSharp.section3;

public class Pos
{
    public int Y;
    public int X;
    
    public Pos(int y, int x)
    {
        Y = y;
        X = x;
    }
}

public class Player
{
    public int PosX { get; private set; }
    public int PosY { get; private set; }

    Board _board;
    private Random _random = new Random();

    enum Dir
    {
        Up = 0,
        Left = 1,
        Down = 2,
        Right = 3
    }

    int _dir = (int)Dir.Up;
    List<Pos> _points = new List<Pos>();

    public void Initialize(int posY, int posX, Board board)
    {
        PosY = posY;
        PosX = posX;

        _board = board;
        
        // RightHand();
        
        // BFS
        /*BFS();*/
        
        // Astar algo
        AStar();
    }

    struct PQNode : IComparable<PQNode>
    {
        public int F;
        public int G;
        public int Y;
        public int X;

        public int CompareTo(PQNode other)
        {
            if (F == other.F)
            {
                return 0;
            }

            return F < other.F ? 1 : -1;
        }
    }

    public void AStar()
    {
        // 점수 매기기
        // F = G + H
        // F : 최종 점수 (작을수록 좋음, 경로에 따라 달라짐)
        // G : 시작점에서 해당 좌표까지 이동하는데 드는 비용 (작을수록 좋음, 경로에 따라 달라짐)
        // H : 목적지에서 얼마나 가까운지 (작을수록 좋음, 고정적임 => 중간에 장애물 고려하지 않고 출발점에서 도착점까지의 거리)
        
        // U L D R UL DL DR UR
        // 대각선 이동 가능 버전
        /*int[] deltaY = new int[] { -1, 0, 1, 0, -1, 1, 1, -1};
        int[] deltaX = new int[] { 0, -1, 0, 1, -1, -1, 1, 1 };
        int[] cost = new int[] { 10, 10, 10, 10, 14, 14, 14, 14};*/
        int[] deltaY = new int[] { -1, 0, 1, 0};
        int[] deltaX = new int[] { 0, -1, 0, 1};
        int[] cost = new int[] { 10, 10, 10, 10};
        
        // (y,x) 지점에 방문했는지 여부를 체크 ( = closed)
        bool[,] closed = new bool[_board.Size, _board.Size];
        
        // (y,x) 가는 길을 발견한적 있는지 체크
        // 발견하지 못했다면 => MaxValue
        // 발견했다면 => F = G + H
        int[,] open = new int[_board.Size, _board.Size];
        for (int y = 0; y < _board.Size; y++)
        {
            for (int x = 0; x < _board.Size; x++)
            {
                open[y,x] = Int32.MaxValue;
            }
        }
        
        // parent 추적을 통해 어떤 길을 갔는지 추적
        Pos[,] parent = new Pos[_board.Size, _board.Size];
        
        // open list에 있는 정보들중(닫혀있지 않은 갈수있는 노드들 중) 가장 좋은 후보들을 바로 뽑기 위한 큐 => 총합값인 F가 가장 작은 노드를 즉시 뽑아오기 위함
        PriorityQueue<PQNode> pq = new PriorityQueue<PQNode>();
        
        // 시작점 발견 => 예약 진행
        // 시작점인 (PosY, PosX)의 F값을 계산해서 open[PosY, PosX]에 저장 : 이때 시작점이므로 G값은 0이기 때문에
        // F = G + H = 0 + H = H 이므로
        // 순수 H값, 즉 시작점에서 도착점까지의 예상 거리와 같음 => 휴리스틱 방법을 사용 (맨허튼 거리 함수) : |p1 - q1| + |p2 - q2|
        open[PosY, PosX] = 10 * (Math.Abs(_board.DestY - PosY) + Math.Abs(_board.DestX - PosX)); 
        
        // 시작점(PosY, PosX)의 정보를 Priority queue에 PQNode 구조체 정보의 형태로 넣음
        pq.Push(new PQNode(){F= 10 * (Math.Abs(_board.DestY - PosY) + Math.Abs(_board.DestX - PosX)), G=0, Y = PosY, X = PosX});
        parent[PosY, PosX] = new Pos(PosY, PosX);
        
        // 남은 노드가 있는 동안 반복
        while (pq.Count > 0)
        {
            // 가장 좋은 후보를 찾기
            
            // 먼저 최우선순위 후보를 하나 꺼냄
            PQNode node = pq.Pop();
            
            // 동일한 좌표를 여러 경로로 찾아서, 더 빠른 경로로 인해 이미 방문된(closed) 경우에는 스킵
            if (closed[node.Y, node.X])
            {
                continue;
            }
            
            // 방문되지 않은 노드의 경우 먼저 방문기록
            closed[node.Y, node.X] = true;
            
            // 만약 목적지인 경우에는 스탑하고 종료
            if (node.Y == _board.DestY && node.X == _board.DestX)
            {
                break;
            }
            
            // 상하좌우 등 모든 이동할 수 있는 좌표를 확인해서 예약함 => open에 추가
            for (int i = 0; i < deltaY.Length; i++)
            {
                int nextY = node.Y + deltaY[i];
                int nextX = node.X + deltaX[i];
                
                // 유효범위 아니면 스킵
                if (nextX < 0 || nextX >= _board.Size || nextY < 0 || nextY >= _board.Size)
                    continue;       
                // 벽이면 스킵 => 벽으로 막혀있음
                if (_board.Tile[nextY, nextX] == Board.TileType.Wall)
                    continue;
                // 방문했으면 스킵
                if (closed[nextY, nextX])
                    continue;
                
                // 비용 계산
                int g = node.G + cost[i];
                int h = 10 * (Math.Abs(_board.DestY - nextY) + Math.Abs(_board.DestX - nextX));
                
                // 만약 다른 경로에서 해당 좌표에 대해 더 빠른 길을 찾았다면 스킵
                if (open[nextY, nextX] < g + h)
                {
                    continue;
                }
                
                // 여기까지 진행됐으면 가장 좋은 케이스임 => open에 예약 진행
                open[nextY, nextX] = g + h;
                pq.Push(new PQNode(){F = g + h, G = g, Y = nextY, X = nextX});
                parent[nextY, nextX] = new Pos(node.Y, node.X);
            }
        }
        CalcPathFromParent(parent);
    }
    
    public void BFS()
    {
        int[] deltaY = new int[] { -1, 0, 1, 0 };
        int[] deltaX = new int[] { 0, -1, 0, 1 };
        
        bool[,] found = new bool[_board.Size, _board.Size]; // 보드 크기에 맞춰 각 위치마다 이동했는지 나타내는 bool 2차원 배열
        Pos[,] parent = new Pos[_board.Size, _board.Size];

        Queue<Pos> q = new Queue<Pos>();
        q.Enqueue(new Pos(PosY, PosX));
        found[PosY, PosX] = true;
        parent[PosY, PosX] = new Pos(PosY, PosX);

        while (q.Count > 0)
        {
            Pos pos = q.Dequeue();
            int nowY = pos.Y;
            int nowX = pos.X;

            for (int i = 0; i < 4; i++)
            {
                int nextY = nowY + deltaY[i];
                int nextX = nowX + deltaX[i];

                if (nextX < 0 || nextX >= _board.Size || nextY < 0 || nextY >= _board.Size)
                    continue;                    
                if (_board.Tile[nextY, nextX] == Board.TileType.Wall)
                    continue;
                if (found[nextY, nextX])
                    continue;
                
                q.Enqueue(new Pos(nextY, nextX));
                found[nextY, nextX] = true;
                parent[nextY, nextX] = new Pos(nowY, nowX);
            }
        }

        CalcPathFromParent(parent);
    }

    void CalcPathFromParent(Pos[,] parent)
    {
        // 이제 BFS를 완료했으니 완료지점에서부터 parent[]를 통해 거꾸로 타고 올라가면서 경로를 확인함
        int y = _board.DestY; // 목적지 y좌표
        int x = _board.DestX; // 목적지 x좌표
        
        // 정확하게 y,x 의 좌표가 아닌동안 반복함
        // 오직 시작점만이 parent[y,x]와 값이 동일하게 되어 있기 때문에 아래와 같은 조건식을 통해 시작점을 찾는 거임
        // parent[PosY, PosX] = new Pos(PosY, PosX); => 이렇게 설정해놨으니 들어가 있는 좌표가 동일한 애만 찾으면 됨
        while (parent[y, x].Y != y || parent[y,x].X != x)
        {
            // 부모 vertex를 _points에 집어넣고,
            _points.Add(new Pos(y, x));
            
            // 부모 vertex의 y값과 x값을 할당함 => 점점 이전 vertex로 이동해서 시작지점까지
            Pos pos = parent[y, x];
            y = pos.Y;
            x = pos.X;
        }
        
        // 마지막 vertex => destY와 destX의 좌표의 vertex도 추가
        _points.Add(new Pos(y, x));
        
        // 현재 마지막 -> 시작 지점 으로 거꾸로 올라왔기 때문에 뒤집어줘야 함
        _points.Reverse();
    }

    public void RightHand()
    {
        int[] frontY = new int[] { -1, 0, 1, 0 };
        int[] frontX = new int[] { 0, -1, 0, 1 };
        int[] rightY = new int[] { 0, -1, 0, 1 };
        int[] rightX = new int[] { 1, 0, -1, 0 };
        
        _points.Add(new Pos(PosY, PosX));
        
        while (PosY != _board.DestY || PosX != _board.DestX) // 아직 도착 못한 상태라면
        {
            // 목표 지점에 들어갈때까지
            
            // 1. 현재 바라보는 방향을 기준으로 오른쪽으로 갈 수 있는지 확인
            if (_board.Tile[PosY+rightY[_dir], PosX+rightX[_dir]] == Board.TileType.Empty)
            {
                // 오른쪽 방향으로 90도 회전
                _dir = (_dir - 1 + 4) % 4;
                // 앞으로 한칸 전진
                PosY = PosY + frontY[_dir];
                PosX = PosX + frontX[_dir];
                _points.Add(new Pos(PosY, PosX));
            }
            // 2. 현재 바라보는 방향 기준으로 전진할 수 있는지 확인
            else if (_board.Tile[PosY+frontY[_dir], PosX+frontX[_dir]] == Board.TileType.Empty)
            {
                // 앞으로 1보 전진
                PosY = PosY + frontY[_dir];
                PosX = PosX + frontX[_dir];
                _points.Add(new Pos(PosY, PosX));
            }
            else // 1,2번 둘다 안되는 경우 => 대부분 막혀있는 경우임
            {
                // 왼쪽 방향으로 90도 회전
                _dir = (_dir + 1 + 4) % 4;
            }
        }
    }


    const int MOVE_TICK = 100;
    int _sumTick = 0;
    int _lastIndex = 0;
    public void Update(int deltaTick)
    {
        if (_lastIndex >= _points.Count)
        {
            _lastIndex = 0;
            _points.Clear();
            _board.Initialize(_board.Size, this);
            Initialize(1,1,_board);
            // return; ==> 끝나면 멈추고 종료하도록
        }
        
        _sumTick += deltaTick;
        if (_sumTick >= MOVE_TICK)
        {
            _sumTick = 0;

            PosY = _points[_lastIndex].Y;
            PosX = _points[_lastIndex].X;
            _lastIndex++;

            /*
            // 움직이는 로직 (상하좌우중 하나 골라서 이동)
            int randValue = _random.Next(0, 5); // 상하좌우
            switch (randValue)
            { // 외각 체크가 없는 상황 : 지금은 Wall로 인해 막아져 있음
                case 0 : // 상
                    if (PosY - 1 >= 0 && _board.Tile[PosY - 1, PosX] == Board.TileType.Empty)
                        PosY -= 1;
                    break;
                case 1: // 하
                    if (PosY + 1 < _board.Size &&_board.Tile[PosY + 1, PosX] == Board.TileType.Empty)
                        PosY += 1;
                    break;
                case 2: // 좌
                    if (PosX - 1 >= 0 && _board.Tile[PosY, PosX - 1] == 0)
                        PosX -= 1;
                    break;
                case 3: //우
                    if (PosX + 1 < _board.Size && _board.Tile[PosY, PosX + 1] == Board.TileType.Empty)
                        PosX += 1;
                    break;

            }
            */

        }
        
    }
}