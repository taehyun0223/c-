using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Etc
{
    class Program
    {
        static void OnInputTest()
        {
            Console.WriteLine("input 발생");
        }

        static void Main(string[] args)
        {
            InputManager inputManager = new InputManager();
            // input key event에 OnInputTest를 구독하여 해당 키 입력 발생시 OnInputTest가 발생하도록 함
            inputManager.InputKey += OnInputTest;

            while (true)
            {
                inputManager.Update();
            }
        }
    }
}
