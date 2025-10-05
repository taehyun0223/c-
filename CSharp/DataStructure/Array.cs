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
        }
    }
}
