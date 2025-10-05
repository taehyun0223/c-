using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.TextRPG2
{
    class Entry
    {
        static void Main(string[] args)
        {
            //Player player = new Knight();
            //Player player2 = new Archer();
            //Monster monster = new Orc();

            //int damage = player.GetAttack();
            //monster.OnDamaged(damage);

            Game game = new Game();

            while (true)
            {
                game.Process();
            }
        }
    }
}
