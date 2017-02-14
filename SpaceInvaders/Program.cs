using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SpaceInvaders
{
    class Program
    {
        private static SpaceInvaders game;

        static void UpdateThread()
        {
            while (true)
            {
                System.Threading.Thread.Sleep(1000);
                game.Update();
            }
        }

        static void Main(string[] args)
        {
            game = new SpaceInvaders();

            // create update thread
            Thread updatethread = new Thread(UpdateThread);

            // draw loop
            // todo, change true so it does not loop endlessly
            while (true)
            {
                
                // draw the game
                game.Draw();
            }

            // when the game is done.
            updatethread.Abort();
        }
    }
}
