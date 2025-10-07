using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.TextRPG
{
    class Mage : Player
    {
        private int mp = 50;
        public Mage() {
            Console.WriteLine("Mage 생성");
        }

        public void setMp(int mp)
        {
            this.mp = mp;
        }
    }
}
