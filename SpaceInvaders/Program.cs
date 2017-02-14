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

        static void InputThread()
        {
            while (true)
            {
                // Thread.Sleep(1000);
                game.OnConsoleInput(Console.ReadKey().Key);
                Console.Write("Dankmemes");
            }
        }

        static void Main(string[] args)
        {
            game = new SpaceInvaders();

            // create update thread
            Thread inputthread = new Thread(InputThread);

            // draw loop
            // todo, change true so it does not loop endlessly
            while (true)
            {
                Thread.Sleep(1000);

                // update the game
                game.Update();

                // draw the game
                game.Draw();
            }

            // Console.ReadKey();

            // when the game is done.
            inputthread.Abort();
        }
    }
}
