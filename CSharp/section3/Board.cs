namespace CSharp.section3;

public class Board
{
    const char CIRCLE = '\u25cf';
    
    public TileType[,] Tile { get; private set; }
    public int Size { get; private set; }
    
    public int DestY { get; private set; }
    public int DestX { get; private set; }

    Player _player;

    public enum TileType
    {
        Empty,
        Wall,
    }

    public void Initialize(int size, Player player)
    {
        // 짝수 크기에서는 동작 안함
        if (size % 2 == 0)
            return;

        _player = player;
        
        Tile = new TileType[size, size];
        Size = size;

        DestY = Size - 2;
        DestX = Size - 2;
        
        GenerateBySideWinder();
    }

    void GenerateBySideWinder()
    {
        // 모든 길을 일단 다 막는 작업
        for (int y = 0; y < Size; y++)
        {
            for (int x = 0; x < Size; x++)
            {
                if (x % 2 == 0 || y % 2 == 0)
                {
                    Tile[y, x] = TileType.Wall;
                }
                else
                {
                    Tile[y, x] = TileType.Empty;
                }
            }
        }
        
        // 막힌 길을 뚫는 작업
        Random rand = new Random();
        for (int y = 0; y < Size; y++)
        {
            int count = 0;
            for (int x = 0; x < Size; x++)
            {
                if (x % 2 == 0 || y % 2 == 0)
                {
                    continue;
                }
                
                if(x == Size -2 && y == Size -2)
                    continue;

                if (y == Size - 2)
                {
                    Tile[y, x + 1] = TileType.Empty;
                    continue;
                }

                if (x == Size - 2)
                {
                    Tile[y + 1, x] = TileType.Empty;
                    continue;
                }

                if (rand.Next(0, 2) == 0)
                {
                    Tile[y, x + 1] = TileType.Empty;
                    count++;
                }
                else
                {
                    int randomIndex = rand.Next(0, count);
                    Tile[y + 1, x - randomIndex * 2] = TileType.Empty;
                    count = 1;
                }
                
                
            }
        }
    }

    void GenerateByBinaryTree()
    {
        
        // 모든 길을 일단 다 막는 작업
        for (int y = 0; y < Size; y++)
        {
            for (int x = 0; x < Size; x++)
            {
                if (x % 2 == 0 || y % 2 == 0)
                {
                    Tile[y, x] = TileType.Wall;
                }
                else
                {
                    Tile[y, x] = TileType.Empty;
                }
            }
        }
        
        // 막힌 길을 뚫는 작업
        // Binary tree algorithm
        // 반반 확률로 한쪽 뚫게 함
        Random rand = new Random();
        for (int y = 0; y < Size; y++)
        {
            for (int x = 0; x < Size; x++)
            {
                if (x % 2 == 0 || y % 2 == 0)
                {
                    continue;
                }
                
                if(x == Size -2 && y == Size -2)
                    continue;

                if (y == Size - 2)
                {
                    Tile[y, x + 1] = TileType.Empty;
                    continue;
                }

                if (x == Size - 2)
                {
                    Tile[y + 1, x] = TileType.Empty;
                    continue;
                }

                if (rand.Next(0, 2) == 0)
                {
                    Tile[y, x + 1] = TileType.Empty;
                }
                else
                {
                    Tile[y + 1, x] = TileType.Empty;
                }
            }
        }
    }
    
    public void Render()
    {
        ConsoleColor prevColor = Console.ForegroundColor; // 안에서 바꾸니깐 기본 색상 유지를 위해 미리 기억해둠 
        for(int y = 0; y < Size; y++)
        {
            for(int x = 0; x < Size; x++)
            {
                // player 좌표를 가져와서 그 좌표랑 현재 y, x가 일치하면 player 전용 색상으로 표시
                if (y == _player.PosY && x == _player.PosX)
                {
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                }
                else if (y == DestY && x == DestX)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                }
                else
                {
                    Console.ForegroundColor = GetTileColor(Tile[y, x]);
                }
                Console.Write(CIRCLE);
            }
            Console.WriteLine();
        }

        Console.ForegroundColor = prevColor; // 이전 색상 복원
    }

    ConsoleColor GetTileColor(TileType type)
    {
        switch (type)
        {
            case TileType.Empty:
                return ConsoleColor.DarkGreen;
            case TileType.Wall:
                return ConsoleColor.DarkRed;
            default:
                return ConsoleColor.DarkGreen;
        }
    }
}