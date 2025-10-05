using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.TextRPG2
{
    public enum GameMode
    {
        None,
        Lobby,
        Town,
        Field
    }
    class Game
    {
        private GameMode mode = GameMode.Lobby;
        private Player player = null;
        private Monster monster = null;
        private Random random = new Random();

        public void Process()
        {
            switch (mode)
            {
                case GameMode.Lobby:
                    ProcessLobby();
                    break;
                case GameMode.Town:
                    ProcessTown();
                    break;
                case GameMode.Field:
                    ProcessField();
                    break;
            }
        }

        private void ProcessLobby()
        {
            Console.WriteLine("직업을 선택해주세요");
            Console.WriteLine("[1] 기사");
            Console.WriteLine("[2] 궁수");
            Console.WriteLine("[3] 법사");

            string userInput = Console.ReadLine();
            switch (userInput)
            {
                case "1":
                    player = new Knight();
                    mode = GameMode.Town;
                    break;
                case "2":
                    player = new Archer();
                    mode = GameMode.Town;
                    break;
                case "3":
                    player = new Mage();
                    mode = GameMode.Town;
                    break;
                default:
                    Console.WriteLine("정상적인 값을 입력해주세요");
                    break;
            }
        }
        
        private void ProcessTown()
        {
            Console.WriteLine();
            Console.WriteLine("마을에 입장했습니다");
            Console.WriteLine();
            Console.WriteLine("[1] 필드로 가기");
            Console.WriteLine("[2] 로비로 돌아가기");
            Console.WriteLine();

            string userInput = Console.ReadLine();
            switch (userInput)
            {
                case "1":
                    mode = GameMode.Field;
                    break;
                case "2":
                    mode = GameMode.Lobby;
                    break;
                default:
                    Console.WriteLine("정상적인 값을 입력해주세요");
                    break;
            }
        }

        private void ProcessField()
        {
            Console.WriteLine();
            Console.WriteLine("필드에 접속했습니다");
            Console.WriteLine();

            // [1] 전투모드 돌입
            // [2] 일정 확률로 마을로 도망
            Console.WriteLine("[1] 전투모드 돌입");
            Console.WriteLine("[2] 일정 확률로 마을로 도망");
            Console.WriteLine();

            CreateMonster();

            string userInput = Console.ReadLine();
            switch (userInput)
            {
                case "1":
                    ProcessFight();
                    break;
                case "2":
                    TryEscape();
                    break;
            }
        }

        private void TryEscape()
        {
            int randValue = random.Next(0, 101);
            if (randValue < 33)
            {
                Console.WriteLine();
                Console.WriteLine("탈출에 성공했습니다");
                Console.WriteLine("마을로 돌아갑니다");
                mode = GameMode.Town;
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("탈출에 실패했습니다");
                Console.WriteLine();
                ProcessFight();
            }
        }

        private void ProcessFight()
        {
            Console.WriteLine("전투에 돌입합니다");
            Console.WriteLine();
            while (true)
            {
                // 플레이어 선공
                int damage = player.GetAttack();
                monster.OnDamaged(damage);
                if (monster.IsDead())
                {
                    Console.WriteLine();
                    Console.WriteLine($"{monster.GetMonsterType()} 전투에서 승리하였습니다");
                    Console.WriteLine($"남은 체력 : {player.GetHp()}");
                    Console.WriteLine();
                    break;
                }

                // 몬스터 후공
                damage = monster.GetAttack();
                player.OnDamaged(damage);
                if (player.IsDead())
                {
                    Console.WriteLine();
                    Console.WriteLine($"{monster.GetMonsterType()} 전투에서 패배했습니다");
                    Console.WriteLine("게임 초기화를 위해 로비로 돌아갑니다");
                    mode = GameMode.Lobby;
                    Console.WriteLine();
                    break;
                }
            }
        }

        private void CreateMonster()
        {
            int randValue = random.Next(0, 3);

            switch (randValue)
            {
                case 0:
                    monster = new Slime();
                    Console.WriteLine("슬라임이 생성되었습니다");
                    break;
                case 1:
                    monster = new Orc();
                    Console.WriteLine("오크가 생성되었습니다");
                    break;
                case 2:
                    monster = new Skeleton();
                    Console.WriteLine("스켈레톤이 생성되었습니다");
                    break;
                    
            }
        }
    }
}
