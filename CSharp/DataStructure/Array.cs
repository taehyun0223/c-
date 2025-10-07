using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.DataStructure
{
    class Player
    {

    }

    class Monster
    {

    }


    // 다차원 배열
    class Map
    {
        int[,] tiles =
        {
            { 1, 1, 1, 1, 1 },
            { 1, 0, 0, 0, 1 },
            { 1, 0, 0, 0, 1 },
            { 1, 0, 0, 0, 1 },
            { 1, 1, 1, 1, 1 }
        };

        public void Render()
        {
            var defaultColor = Console.ForegroundColor;

            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                for (int x = 0; x < tiles.GetLength(0); x++)
                {
                    if (tiles[x, y] == 1)
                        Console.ForegroundColor = ConsoleColor.Red;
                    else
                        Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write('\u25cf');
                }
                Console.WriteLine();
            }

            Console.ForegroundColor = defaultColor;
        }
    }



    class Array
    {
        Player player;
        Monster monster;

        Player player2;
        Monster monster2;

        static void Main(string[] args)
        {
            //배열
            int[] scores = new int[5];

            for(int i = 0; i < scores.Length; i++)
            {
                scores[i] = i*10;
            }
            // int[] scores = new int[5] { 0, 10, 20, 30, 40 };
            // int[] scores = new int[] { 0, 10, 20, 30, 40 };
            // int[] scores = { 0, 10, 20, 30, 40 };


            foreach (int i in scores)
            {
                Console.WriteLine(i);
            }

            // 배열 메소드 만들기
            int[] scores2 = new int[5] { 10, 30, 40, 20, 50 };
            Console.WriteLine(GetHighestScore(scores2));
            Console.WriteLine(GetAverageScore(scores2));
            int findIndex = GetIndexOf(scores2, 30);
            Console.WriteLine(findIndex);
            Sort(scores2);


            // 2차원 배열
            int[,] arr = new int[2, 3];
            int[,] arr2 = new int[2, 3] { {1, 2, 3}, { 4, 5, 6 } };
            int[,] arr3 = new int[,] { {1, 2, 3}, { 4, 5, 6 } };
            // 3x2 크기의 배열 선언

            // Map 렌더링
            Map map = new Map();
            map.Render();


            // 가변 배열
            int[][] a = new int[2][];
            a[0] = new int[2];
            a[1] = new int[6];
            a[2] = new int[3];


        }

        







        // 일반 배열

        static int GetHighestScore(int[] scores)
        {
            int max = scores[0];
            for (int i = 1; i < scores.Length; i++)
            {
                if (scores[i] > max)
                {
                    max = scores[i];
                }
            }
            return max;
        }

        static int GetAverageScore(int[] scores)
        {
            int sum = 0;
            foreach (int i in scores)
            {
                sum += i;
            }

            if (scores.Length == 0)
            {
                return 0;
            }

            return (sum / scores.Length);
        }

        static int GetIndexOf(int[] scores, int value)
        {
            for (int i = 0; i < scores.Length; i++)
            {
                if (scores[i] == value)
                {
                    return i;
                }
            }
            return -1;
        }

        static void Sort(int[] scores)
        {
            for(int i = 0;i < scores.Length-1;i++)
            {
                for(int j = i+1; j < scores.Length; j++)
                {
                    if (scores[i] > scores[j])
                    {
                        int temp = scores[i];
                        scores[i] = scores[j];
                        scores[j] = temp;
                    }
                }
            }
        }
    }
}
