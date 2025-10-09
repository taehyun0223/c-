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
        BFS();

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


    const int MOVE_TICK = 1;
    int _sumTick = 0;
    int _lastIndex = 0;
    public void Update(int deltaTick)
    {
        if (_lastIndex >= _points.Count)
        {
            return;
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