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

        int[] frontY = new int[] { -1, 0, 1, 0 };
        int[] frontX = new int[] { 0, -1, 0, 1 };
        int[] rightY = new int[] { 0, -1, 0, 1 };
        int[] rightX = new int[] { 1, 0, -1, 0 };
        
        _points.Add(new Pos(PosY, PosX));
        
        while (PosY != board.DestY || PosX != board.DestX) // 아직 도착 못한 상태라면
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