using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Procedural
{
    class TextRPG
    {
        enum ClassType
        {
            None = 0,
            Knight = 1,
            Archer = 2,
            Mage = 3
        }

        enum MonsterType
        {
            None = 0,
            Slime = 1,
            Orc = 2,
            Skeleton = 3
        }

        struct Player
        {
            public int hp;
            public int attack;
        }

        struct Monster
        {
            public int hp;
            public int attack;
        }

        static ClassType choiceClass()
        {
            while (true)
            {
                Console.WriteLine("직업을 선택하세요!");
                Console.WriteLine("[1] 기사");
                Console.WriteLine("[2] 궁수");
                Console.WriteLine("[3] 법사");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        return ClassType.Knight;
                    case "2":
                        return ClassType.Archer;
                    case "3":
                        return ClassType.Mage;
                    default:
                        continue;
                }
            }
        }

        static void createPlayer(ClassType myClass, out Player player)
        {
            switch (myClass)
            {
                case ClassType.Knight:
                    player.hp = 100;
                    player.attack = 10;
                    break;
                case ClassType.Archer:
                    player.hp = 75;
                    player.attack = 12;
                    break;
                case ClassType.Mage:
                    player.hp = 50;
                    player.attack = 15;
                    break;
                default:
                    player.hp = 1;
                    player.attack = 0;
                    break;
            }
        }

        static void createRandomMonster(out Monster monster)
        {
            Random random = new Random();
            int randMonster = random.Next(1, 4); // 1~3 사이 랜덤

            switch (randMonster)
            {
                case (int)MonsterType.Slime:
                    Console.WriteLine("슬라임이 스폰되었습니다");
                    monster.hp = 20;
                    monster.attack = 2;
                    break;
                case (int)MonsterType.Orc:
                    Console.WriteLine("오크가 스폰되었습니다");
                    monster.hp = 40;
                    monster.attack = 4;
                    break;
                case (int)MonsterType.Skeleton:
                    Console.WriteLine("스켈레톤이 스폰되었습니다");
                    monster.hp = 30;
                    monster.attack = 3;
                    break;
                default:
                    monster.hp = 0;
                    monster.attack = 0;
                    break;
            }
        }

        static void fight(ref Player player, ref Monster monster)
        {
            while (true)
            {
                // 플레이어가 선공 -> 몬스터 공격
                monster.hp -= player.attack;
                if (monster.hp <= 0)
                {
                    Console.WriteLine("승리했습니다");
                    Console.WriteLine($"남은 체력: {player.hp}");
                    break;
                }

                // 몬스터 공격 턴
                player.hp -= monster.attack;
                if (player.hp <= 0)
                {
                    Console.WriteLine("패배했습니다");
                    break;
                }
            }
        }

        static void enterField(ref Player player)
        {
            while (true)
            {
                Console.WriteLine("필드에 접속했습니다");

                // 몬스터 생성 : 랜덤 1~3 몬스터 중 하나를 랜덤 리스폰
                Monster monster;
                createRandomMonster(out monster);

                // [1] 전투모드 돌입
                // [2] 일정 확률로 마을로 도망
                Console.WriteLine("[1] 전투모드 돌입");
                Console.WriteLine("[2] 일정 확률로 마을로 도망");

                string userInput = Console.ReadLine();
                if (userInput == "1")
                {
                    fight(ref player, ref monster);
                }
                else if (userInput == "2")
                {
                    // 33% 확률로 도망
                    Random rand = new Random();
                    int randValue = rand.Next(0, 101);
                    if (randValue <= 33)
                    {
                        Console.WriteLine("도망치는데 성공했습니다");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("도망치는데 실패했습니다. 전투에 돌입합니다");
                        fight(ref player, ref monster);
                    }
                }
            }
        }

        static void enterGame(ref Player player)
        {

            while (true)
            {
                Console.WriteLine("게임에 접속했습니다");
                Console.WriteLine("[1] 필드로 가기");
                Console.WriteLine("[2] 로비로 돌아가기");

                string userInput = Console.ReadLine();

                if (userInput == "1")
                    enterField(ref player);
                else if (userInput == "2")
                    break;
            }
        }

        static void Main(string[] args)
        {
            // 직업 선택
            ClassType myClass = choiceClass();
            //Console.WriteLine(myClass);

            // 캐릭터 생성
            Player player;
            createPlayer(myClass, out player);

            Console.WriteLine("캐릭터 생성 정보");
            Console.WriteLine($"직업 : {myClass}");
            Console.WriteLine($"체력: {player.hp}, 공격력: {player.attack}");

            // 게임진입
            enterGame(ref player);
        }
    }

}
