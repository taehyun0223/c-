namespace CSharp.section3;

public class Board
{
    const char CIRCLE = '\u25cf';
    
    public TileType[,] _tile;
    public int _size;

    public enum TileType
    {
        Empty,
        Wall,
    }

    public void Initialize(int size)
    {
        // 짝수 크기에서는 동작 안함
        if (size % 2 == 0)
            return;
        
        _tile = new TileType[size, size];
        _size = size;
        
        GenerateByBinaryTree();
    }

    void GenerateByBinaryTree()
    {
        
        // 모든 길을 일단 다 막는 작업
        for (int y = 0; y < _size; y++)
        {
            for (int x = 0; x < _size; x++)
            {
                if (x % 2 == 0 || y % 2 == 0)
                {
                    _tile[y, x] = TileType.Wall;
                }
                else
                {
                    _tile[y, x] = TileType.Empty;
                }
            }
        }
        
        // 막힌 길을 뚫는 작업
        // Binary tree algorithm
        Random rand = new Random();
        for (int y = 0; y < _size; y++)
        {
            for (int x = 0; x < _size; x++)
            {
                if (x % 2 == 0 || y % 2 == 0)
                {
                    continue;
                }
                
                if(x == _size -2 && y == _size -2)
                    continue;

                if (y == _size - 2)
                {
                    _tile[y, x + 1] = TileType.Empty;
                    continue;
                }

                if (x == _size - 2)
                {
                    _tile[y + 1, x] = TileType.Empty;
                    continue;
                }

                if (rand.Next(0, 2) == 0)
                {
                    _tile[y, x + 1] = TileType.Empty;
                }
                else
                {
                    _tile[y + 1, x] = TileType.Empty;
                }
            }
        }
    }
    
    public void Render()
    {
        ConsoleColor prevColor = Console.ForegroundColor; // 안에서 바꾸니깐 기본 색상 유지를 위해 미리 기억해둠 
        for(int y = 0; y < _size; y++)
        {
            for(int x = 0; x < _size; x++)
            {
                Console.ForegroundColor = GetTileColor(_tile[y, x]);
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
                return ConsoleColor.Green;
            case TileType.Wall:
                return ConsoleColor.Red;
            default:
                return ConsoleColor.Green;
        }
    }
}