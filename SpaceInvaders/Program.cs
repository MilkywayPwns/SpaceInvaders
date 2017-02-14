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
                game.OnConsoleInput(Console.ReadKey().Key);                
            }
        }

        static void AlienThread()
        {
            while (true)
            {
                // Check if we need to add Aliens
                game.CheckAliens();

                // Sleep thread for 1 second
                Thread.Sleep(1000);

                // Update all aliens
                game.Mutex.WaitOne();
                foreach (Alien alien in game.Aliens)
                {
                    alien.Update(game);
                }
                game.Mutex.ReleaseMutex();
            }
        }

        static void Main(string[] args)
        {
            game = new SpaceInvaders();

            // create update thread
            Thread inputthread = new Thread(InputThread);
            inputthread.Start();

            // alien update thread
            Thread alienthread = new Thread(AlienThread);
            alienthread.Start();

            // game loop
            while (true)
            {
                Thread.Sleep(200);

                // update the game
                game.Update();

                // draw the game
                game.Draw();
            }

            // when the game is done.
            // inputthread.Abort();
        }
    }
}
