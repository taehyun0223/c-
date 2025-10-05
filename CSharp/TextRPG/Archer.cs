using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.TextRPG
{
    class Archer : Player
    {
        public Archer() : base(70, 5)
        {
            Console.WriteLine("궁수 생성");
        }
    }
}
