using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp
{
    class Flow
    {
        // enumeration
        enum Choice
        {
            Rock = 1,
            Scissors = 0,
            Paper = 2
        }
        static void Main(string[] args)
        {
            /* 1.if */
            int hp = -1;
            bool isDead = (hp < 0);
            if (isDead) 
            {
                Console.WriteLine("You Died");
            }
            else 
            {
                Console.WriteLine("You are alive");
            }

            /* 2. if-else */
            int choice = 0;

            if (choice == 0)
            {
                Console.WriteLine("가위");
            }
            else if (choice == 1)
            {
                Console.WriteLine("바위");
            }
            else if (choice == 2)
            {
                Console.WriteLine("보");
            }
            else
            {
                Console.WriteLine("0~2 를 입력해주세요");
            }

            /* 3. switch */
            int choice2 = 0;

            switch (choice)
            {
                case 0:
                    Console.WriteLine("가위");
                    break;
                case 1:
                    Console.WriteLine("바위");
                    break;
                case 2:
                    Console.WriteLine("보");
                    break;
                case 3:
                    Console.WriteLine("치트");
                    break;
                default:
                    Console.WriteLine("0~3 를 입력해주세요");
                    break;
            }

            /* 4. ternary operator */
            int num = 11;

            bool overTen = num > 10 ? true : false;

            /* 5. 가위바위보 게임 */
            Random rand = new Random();
            int randNum = rand.Next(0, 3); // random 0 ~ 2
            int userNum = Convert.ToInt32(Console.ReadLine());

            switch (userNum)
            {
                case 0:
                    Console.WriteLine("your choice is 가위");
                    break;
                case 1:
                    Console.WriteLine("your choice is 바위");
                    break;
                case 2:
                    Console.WriteLine("your choice is 보");
                    break;
                default:
                    Console.WriteLine("올바르지 않은 값을 입력함");
                    break;
            }

            switch (randNum)
            {
                case 0:
                    Console.WriteLine("computer choice is 가위");
                    break;
                case 1:
                    Console.WriteLine("computer choice is 바위");
                    break;
                case 2:
                    Console.WriteLine("computer choice is 보");
                    break;
                default:
                    Console.WriteLine("오류 발생");
                    break;
            }

            int result = -1;
            if (userNum >= 0 && userNum <= 2)
            {
                result = (userNum - randNum + 3) % 3;
            }
            
            if (result == 0)
            {
                Console.WriteLine("무승부");
            }
            else if (result == 1)
            {
                Console.WriteLine("유저 승리");
            }
            else if (result == 2)
            {
                Console.WriteLine("컴퓨터 승리");
            }
            else
            {
                Console.WriteLine("값이 잘못 입력됨");
            }

            /* 6. enumeration and constant */

            // constant
            const int SCISSORS = 0;
            const int ROCK = 1;
            const int PAPER = 2;

            // using enum type
            switch (userNum)
            {
                case (int)Choice.Scissors:
                    Console.WriteLine("your choice is 가위");
                    break;
                case (int)Choice.Rock:
                    Console.WriteLine("your choice is 바위");
                    break;
                case (int)Choice.Paper:
                    Console.WriteLine("your choice is 보");
                    break;
                default:
                    Console.WriteLine("올바르지 않은 값을 입력함");
                    break;
            }
        }
    }
}
