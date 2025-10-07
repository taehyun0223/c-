namespace CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false; // console창에서 커서 보이게할지 선택

            const int WAIT_TICK = 1000 / 30;
            const char CIRCLE = '\u25cf';

            int laskTick = 0;

            /*
            // 대부분의 게임은 main함수라는 하나의 진입점을 통해 실행되며

            // 메인 무한루프 안에서
            */
            while (true)
            {
                #region 프레임관리
                // 대부분의 게임은 최근 적어도 60FPS를 해줘야 버벅거림없이 동작하는 것으로 보임
                int currentTick = System.Environment.TickCount; // 절대적인 시간x, 프로그램 시작하고 카운팅되는 시간

                // 만약에 경과한 시간이 1/30초보다 작다면 통과시킴
                if (currentTick - laskTick < WAIT_TICK)
                    continue;
                laskTick = currentTick;
                #endregion


                /*
                // 클라이언트는 3가지의 주요 행동을 함

                // 입력 (ex. 사용자가 키보드 입력 : 공격키)

                // 로직 (ex. 특정 공격하면 스킬이 나간다던지 몬스터가 데미지를 입는다던지의 설정 로직 실행)

                // 렌더링 (ex. 캐릭터가 실제로 공격하는 모션, 데미지가 들어가는 화면을 렌더링)
                */

                Console.SetCursorPosition(0, 0); // console의 0,0 좌표로 커서를 고정

                for(int i = 0; i < 25; i++)
                {
                    for(int j = 0; j < 25; j++)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(CIRCLE);
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
